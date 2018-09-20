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
		Hazard
	}

	class Tile : GameObject
	{
		private TileType type;
		public static float tileSize = 32;

		public Tile(int x, int y, TileType type) : 
			base(new Vector2(x * Tile.tileSize * 2, y * Tile.tileSize * 2), new Vector2(Tile.tileSize, Tile.tileSize), new Vector2(1, 1))
		{
			if (type == TileType.Floor)
			{
				collider = null;
			}
			else
			{
				collider = new Collider();
			}
		}

		public TileType Type { get { return type; } set { type = value; } }
		public static float TileSize { get { return tileSize; } set { tileSize = value; } }
	}

	public class Room
	{
		List<Tile> tiles;

		public Room(int x, int y, int w, int h)
		{
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


		public void Overlap(GameObject go)
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
					tile.Type == TileType.Wall ? Color.White : Color.Red, 
					0, Vector2.Zero, tile.Size() * 2, SpriteEffects.None, 0);
			}
		}
	}
}

