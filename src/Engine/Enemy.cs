using System;
using Microsoft.Xna.Framework;

namespace Vamp
{
	public class Enemy : GameObject
	{


		private float hitPoints;

		public Enemy() : base(new Vector2(0, 0), new Vector2(32,32), new Vector2(1, 1), new Collider(true, Shape.Circle))
		{
			hitPoints = 35.0f;
		}

		public void Update(Player player, float delta)
		{
			float speed = (float) 300.0;
			Vector2 direction = Vector2.Normalize(player.Position - position);
			position += direction * speed * delta;
		}

		public bool IsAlive()
		{
			return hitPoints > 0;
		}
	}
}
