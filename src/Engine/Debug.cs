using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vamp
{
	// A namespace for things that should only be used during debug.
	public class Debug
	{
		public static Texture2D pixel;

		public static void DrawLine(SpriteBatch batch, 
				Vector2 start, Vector2 end)
		{
			DrawLine(batch, start, end, 1);
		}

		public static void DrawLine(SpriteBatch batch, 
				Vector2 start, Vector2 end, float size)
		{
			DrawLine(batch, start, end, size, new Color(165, 28, 132));
		}

		public static void DrawLine(SpriteBatch batch, 
				Vector2 start, Vector2 end,
				float size,
				Color color)
		{
			Vector2 edge = end - start;
			float angle = (float) Math.Atan2(edge.Y, edge.X);
			batch.Draw(
					pixel, start,
					null, color, 
					angle, Vector2.Zero,
					new Vector2(edge.Length(), size),
					SpriteEffects.None, 0);

		}
				
	}

}
