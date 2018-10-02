using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Vamp
{
	public class Enemy : GameObject
	{
		private float hitPoints, speed;

		public Enemy(Vector2 position) : base(position, new Vector2(32,32), new Vector2(1, 1), new Collider(true, Shape.Circle))
		{
			hitPoints = 35.0f;
			speed = 300;
		}

		public void Update(Player player, float delta, List<Attack> attacks)
		{

			Vector2 direction = Vector2.Normalize(player.Position - position);
			position += direction * speed * delta;

			foreach (Attack attack in attacks)
			{
				Overlap overlap = Collider.Check(this, attack);
				if (overlap)
				{
					hitPoints -= 10;
				}
			}
		}

		public bool IsAlive()
		{
			return hitPoints > 0;
		}
	}
}
