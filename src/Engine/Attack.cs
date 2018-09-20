using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Graphics;

namespace Vamp
{
	public class Attack : GameObject
	{
		private Vector2 velocity;
		private float aliveTime, rotation;

		public Attack (Vector2 origin, Vector2 velocity, Vector2 scale, Collider collider, float aliveTime) : base(origin, scale, collider)
		{
			this.velocity = velocity;
			this.aliveTime = aliveTime;
			this.rotation = velocity.Normalize();
		}

		public void Update (GameTime gameTime)
		{
			Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
			alivetime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
		}
	}
}
