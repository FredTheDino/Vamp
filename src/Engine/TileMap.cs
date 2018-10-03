using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vamp
{
	public class MapRegion
	{
		private int x, y, w, h;

		public int X { get { return x; } }
		public int Y { get { return y; } }
		public int W { get { return w; } }
		public int H { get { return h; } }

		public MapRegion(int x, int y, int w, int h) 
		{
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
		}
		
		public MapRegion Intersection(MapRegion that)
		{
			int x = Math.Max(this.x, that.x) + 1;
			int y = Math.Max(this.y, that.y) + 1;
			int w = Math.Min(this.x + this.w, that.x + that.w) - x;
			int h = Math.Min(this.y + this.h, that.y + that.h) - y;
			return new MapRegion(x, y, Math.Max(w, 0), Math.Max(h, 0));
		}

		// Grows twice the ammount, since it scales in both the negative
		// and posetive direction.
		public void Grow(int x, int y)
		{
			this.x -= x * 2;
			this.w += x * 2;

			this.y -= y * 2;
			this.h += y * 2;
		}

		public void LoopOver(Func<int, int, int> func)
		{
			for (int x = this.x; x < (this.x + this.w); x++)
				for (int y = this.y; y < (this.y + this.h); y++)
					func(x, y);
		}
	}

	public enum TileType
	{
		Wall,
		Floor,
		Door,
		Hazard
	}

	public class Tile : GameObject
	{
		private TileType type;
		public static float tileSize = 32;

		public Tile(int x, int y, TileType type) : 
			base(new Vector2(x * Tile.tileSize * 2, y * Tile.tileSize * 2), new Vector2(Tile.tileSize, Tile.tileSize), new Vector2(1, 1))
		{
			this.type = type;
			if (type == TileType.Wall)
			{
				collider = new Collider();
			}
			else
			{
				collider = null;
			}
			//collider = null;
		}

		public TileType Type { get { return type; } set { type = value; } }
		public static float TileSize { get { return tileSize; } set { tileSize = value; } }
	}

	public class Floor
	{
		// A list of all the rooms.
		List<Room> rooms;

		private bool CanBePlaced(Room a)
		{
			foreach (Room b in rooms)
			{
				if (a.X + a.W < b.X || b.X + b.W < a.X)
					continue;
				if (a.Y + a.H < b.Y || b.Y + b.H < a.Y)
					continue;
				return false;
			}
			return true;
		}

		public Floor(int length, int offshoots)
		{
			// TODO: Add charcteristics, like the floor could have large rooms,
			// or a lot of enemies.

			// Moving left in the drunken walk is illigal!
			
			// Initalize
			this.rooms = new List<Room>();

			// First room is allways set.
			Room room = new Room(-7, -7, 14, 14);
			rooms.Add(room);

			for (int i = 0; i < length; i++)
			{
				// Offset one so the first loop works
				int direction = RNG.RandomIntInRange(-1, 1);
				int doorX;
				int doorY;
				do
				{
					// TODO: Break if we're in an infinet loop.
					// Check all posibilities.
					direction = (direction + 1) % 3;

					int startX, startY;
					int offset = RNG.RandomIntInRange(-2, 2);
					int w = RNG.RandomIntInRange(15, 21);
					int h = RNG.RandomIntInRange(11, 17);
					if (direction == 0)
					{
						// Up
						doorX = room.X + room.W / 2;
						doorY = room.Y - 1;
						startX = doorX + offset;
						startY = doorY - h;
					}
					else if (direction == 1)
					{
						// Down
						doorX = room.X + room.W / 2;
						doorY = room.Y + room.H + 1;
						startX = doorX + offset;
						startY = doorY;
					}
					else // if (direction == 2)
					{
						// Right
						doorX = room.X + room.W + 1;
						doorY = room.Y + room.H / 2;
						startX = doorX;
						startY = doorY + offset;
					}
					room = new Room(startX, startY, w, h);
				} while (!CanBePlaced(room));
				rooms.Add(room);
				Room to = room;
				Room from = rooms[rooms.Count - 2];

				MapRegion fromRegion = from.ToRegion();
				MapRegion toRegion   = to.ToRegion();
				MapRegion intersect = fromRegion.Intersection(toRegion);

				if (direction != 2)
				{
					// Vertical direction, so clamp the x-axis
					intersect.Grow(0, 1);
				}
				else 
				{
					// Horizontal direction, so clamp the y-axis
					intersect.Grow(1, 0);
				}

				intersect.LoopOver((x, y) =>
					{
						to.SetTileAt(x, y, new Tile(x, y, TileType.Door));
						from.SetTileAt(x, y, new Tile(x, y, TileType.Door));
						// TODO: Figure out why this doesn't work.
						return 0;
					});
			}
		}

		public void Update(GameObject go)
		{
			// TODO: Optimize this.
			foreach (Room room in rooms)
			{
				room.Update(go);
			}
		}

		public void Draw(SpriteBatch batch, Texture2D texture)
		{
			foreach (Room room in rooms)
			{
				room.Draw(batch, texture);
			}
		}
	}

	public class Room
	{
		private List<Tile> tiles;
		private readonly int x, y, w, h;

		public int X { get { return x; } }
		public int Y { get { return y; } }
		public int W { get { return w; } }
		public int H { get { return h; } }


		public Room(int x, int y, int w, int h)
		{
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
			tiles = new List<Tile>();
			for (int tileX = x; tileX <= x + w; tileX++)
			{
				for (int tileY = y; tileY <= y + h; tileY++)
				{
					TileType type;
					if (tileX == x || tileX == x + w)
						type = TileType.Wall;
					else if (tileY == y || tileY == y + h)
						type = TileType.Wall;
					else
						type = TileType.Floor;
							
					tiles.Add(new Tile(tileX, tileY, type));
				}
			}
		}

		bool ContainsWorldPoint(Vector2 point)
		{
			if (point.X < (x * Tile.TileSize))
				return false;
			if (point.X > ((x + w) * Tile.TileSize))
				return false;
			if (point.Y < (y * Tile.TileSize))
				return false;
			if (point.Y > ((y + h) * Tile.TileSize))
				return false;
			return true;
		}

		public MapRegion ToRegion()
		{
			return new MapRegion(x, y, w, h);
		}
	
		public Tile GetTileAt(int x, int y)
		{
			foreach (Tile t in tiles)
			{
				if (t.Position.X == x && t.Position.Y == y)
					return t;
			}
			return null;
		}

		public void SetTileAt(int x, int y, Tile tile)
		{
			for (int i = 0; i < tiles.Count; i++)
			{
				Tile t = tiles[i];
				if (t.Position.X == x * Tile.tileSize * 2 && t.Position.Y == y * Tile.tileSize * 2)
				{
					tiles[i] = tile;
					return;
				}
			}
		}

		public void Update(GameObject go)
		{
			Overlap overlap; 
			foreach (Tile tile in tiles)
			{
				if (tile.Collider == null)
					continue;
				overlap = Collider.Check(tile, go);
				overlap.Solve();
			}
		}

		public void DebugDraw(SpriteBatch batch)
		{
			foreach (Tile tile in tiles)
			{
				tile.DrawCollider(batch);
			}
		}

		public void Draw(SpriteBatch batch, Texture2D texture)
		{
			foreach (Tile tile in tiles)
			{
				Color c;
				if (tile.Type == TileType.Wall)
					c = Color.White;
				else if (tile.Type == TileType.Floor)
					c = Color.Green;
				else if (tile.Type == TileType.Door)
					c = Color.Red;
				else
					c = Color.Black;

				batch.Draw(texture, tile.Position - tile.Size(), null, 
					c, 0, Vector2.Zero, tile.Size() * 2, 
					SpriteEffects.None, 0);
			}
		}
	}
}

