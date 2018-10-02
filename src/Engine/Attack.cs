using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Vamp
{
	public class Attack : GameObject
	{
		private Vector2 velocity;
		private float aliveTime, rotation;

		public Attack (Vector2 origin, Vector2 velocity, Vector2 dimension, Vector2 scale, Collider collider, float aliveTime) : base(origin, dimension, scale, collider)
		{
			this.velocity = velocity;
			this.aliveTime = aliveTime;
			this.rotation = (float) Math.Atan2(velocity.X, -velocity.Y);
		}

		public void Update (GameTime gameTime)
		{
			Position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
			aliveTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
		}

		public bool IsAlive ()
		{
			return aliveTime > 0;
		}

		public float Rotation
		{ get { return rotation; } }
	}
}
