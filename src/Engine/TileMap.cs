using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vamp
{
	enum TileType
	{
		Wall,
		Floor,
		Door,
		Hazard
	}

	class Tile : GameObject
	{
		private TileType type;
		public static float tileSize = 32;

		public Tile(int x, int y, TileType type) : 
			base(new Vector2(x * Tile.tileSize * 2, y * Tile.tileSize * 2), new Vector2(Tile.tileSize, Tile.tileSize), new Vector2(1, 1))
		{
			this.type = type;
			if (type == TileType.Floor)
			{
				collider = null;
			}
			else
			{
				collider = new Collider();
			}
			collider = null;
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
				if (a.x + a.w < b.x || b.x + b.w < a.x)
					continue;
				if (a.y + a.h < b.y || b.y + b.h < a.y)
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
			Room room = new Room(-5, -5, 10, 10);
			rooms.Add(room);

			for (int i = 0; i < length; i++)
			{
				// Offset one so the first loop works
				int direction = RNG.RandomIntInRange(-1, 1);
				do
				{
					// TODO: Break if we're in an infinet loop.
					// Check all posibilities.
					direction = (direction + 1) % 3;

					int startX;
					int startY;
					int offset = RNG.RandomIntInRange(-2, 2);
					int w = RNG.RandomIntInRange(12, 19);
					int h = RNG.RandomIntInRange(10, 15);
					if (direction == 0)
					{
						// Up
						startX = room.x + room.w / 2;
						startY = room.y - 1;
						startX += offset;
						startY -= h;
					}
					else if (direction == 1)
					{
						// Down
						startX = room.x + room.w / 2;
						startY = room.y + room.h + 1;
						startX += offset;
						startY += 0;
					}
					else // if (direction == 2)
					{
						// Right
						startX = room.x + room.w + 1;
						startY = room.y + room.h / 2;
						startX += 0;
						startY += offset;
					}
					room = new Room(startX, startY, w, h);
				} while (!CanBePlaced(room));
				rooms.Add(room);
			}
		}

		public void Update(GameObject go)
		{
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
		List<Tile> tiles;
		public readonly int x, y, w, h;

		public Room(int x, int y, int w, int h)
		{
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
			tiles = new List<Tile>();
			for (int tile_x = x; tile_x <= x + w; tile_x++)
			{
				for (int tile_y = y; tile_y <= y + h; tile_y++)
				{
					TileType type;
					if (tile_x == x || tile_x == x + w)
						type = TileType.Wall;
					else if (tile_y == y || tile_y == y + h)
						type = TileType.Wall;
					else
						type = TileType.Floor;
							
					tiles.Add(new Tile(tile_x, tile_y, type));
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
				batch.Draw(texture, tile.Position - tile.Size(), null, 
					tile.Type == TileType.Wall ? Color.White : Color.Green, 
					0, Vector2.Zero, tile.Size() * 2, SpriteEffects.None, 0);
			}
		}
	}
}

