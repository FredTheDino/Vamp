using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace Vamp
{
    public class Player : GameObject
    {
        // The players velocity
        private Vector2 velocity;

		// The players firerate, elapsedtime from last fire, speed and its attacks aliveTime
		private float firerate, elapsedTime, speed, attackAliveTime;

        // Main constructor
        public Player (Vector2 position) : base(position, new Vector2(32,32), new Vector2(1, 1), new Collider(true, Shape.Circle))
        {
            velocity = new Vector2();
			firerate = 2f;
			elapsedTime = firerate;
			speed = 0;
			attackAliveTime = 1f;
        }

        // Update the player every frame
        public void Update (GameTime gameTime, KeyboardState keyboardState, List<Attack> attacks)
        {
			// Add time to the elapsed time
			elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
			
			// Initialize acceleration
			Vector2 acceleration = new Vector2();

            // Read keyboard input
            if (keyboardState.IsKeyDown(Keys.W))
            {
                acceleration += new Vector2(0,-1);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                acceleration += new Vector2(1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                acceleration += new Vector2(0, 1);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                acceleration += new Vector2(-1, 0);
            }
			if (keyboardState.IsKeyDown(Keys.Up) && elapsedTime > 1 / firerate)
			{
				Shoot(new Vector2(0,-1), 3, 1000, attacks);
				elapsedTime = 0;
			}
			if (keyboardState.IsKeyDown(Keys.Right) && elapsedTime > 1 / firerate)
			{
				Shoot(new Vector2(1, 0), 3, 1000, attacks);
				elapsedTime = 0;
			}
			if (keyboardState.IsKeyDown(Keys.Down) && elapsedTime > 1 / firerate)
			{
				Shoot(new Vector2(0, 1), 3, 1000, attacks);
				elapsedTime = 0;
			}
			if (keyboardState.IsKeyDown(Keys.Left) && elapsedTime > 1 / firerate)
			{
				Shoot(new Vector2(-1,0), 3, 1000, attacks);
				elapsedTime = 0;
			}

            // Update position
            if (acceleration.LengthSquared() != 0) acceleration.Normalize();	

			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

			float accLength = 15000;
			float friction = 0.35f;
			float maxSpeed = 500;
			float minSpeed = 4000 * dt;

			velocity += acceleration * accLength * dt;
			velocity -= velocity * (float) Math.Pow(dt, friction);
			
			if (velocity.LengthSquared() > maxSpeed * maxSpeed)
			{ 
				velocity = Vector2.Normalize(velocity) * maxSpeed;
			}
			else if (acceleration.LengthSquared() == 0)
			{
				if (velocity.LengthSquared() < minSpeed * minSpeed)
				{
					velocity *= 0;
				}
			}

            Position += velocity * dt;
        }

		private void Shoot (Vector2 direction, int projectiles, int projectileSpeed, List<Attack> attacks)
		{
			double angle = Math.Atan2(direction.Y, direction.X), fireWidth = (projectiles-1.0)/ 10;
			if (fireWidth > 1) fireWidth = 1;
			angle -= fireWidth/2;
			for (int i = 0; i < projectiles; i++)
			{
				double shotAngle = angle + i * fireWidth / (projectiles - 0.99999);
				Vector2 velocity = new Vector2((float) Math.Cos(shotAngle), (float) Math.Sin(shotAngle)) * projectileSpeed;
				Attack attack = new Attack(Position,
						velocity,
						new Vector2(8,8),
						new Vector2(1,1),
						new Collider(true, Shape.Circle),
						attackAliveTime);
				attacks.Add(attack);
			}
		}
    }
}
