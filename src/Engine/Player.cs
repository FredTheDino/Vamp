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

		// Various player stats
		private float fireRate, attackAliveTime, shotSpeed, maxSpeed, damage, shotSpread, projectiles;

		// Initalize elapsed time
		private float elapsedTime;

        // Main constructor
        public Player (Vector2 position) : base(position, new Vector2(32,32), new Vector2(1, 1), new Collider(true, Shape.Circle))
        {
            velocity = new Vector2();
			fireRate = 4f;
			elapsedTime = fireRate;
			attackAliveTime = 2f;
			shotSpeed = 1000;
			maxSpeed = 500;
			damage = 10;
			shotSpread = 0.5f;
			projectiles = 3;
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
			if (keyboardState.IsKeyDown(Keys.Up) && elapsedTime > 1 / fireRate)
			{
				Shoot(new Vector2(0,-1), attacks);
				elapsedTime = 0;
			}
			if (keyboardState.IsKeyDown(Keys.Right) && elapsedTime > 1 / fireRate)
			{
				Shoot(new Vector2(1, 0), attacks);
				elapsedTime = 0;
			}
			if (keyboardState.IsKeyDown(Keys.Down) && elapsedTime > 1 / fireRate)
			{
				Shoot(new Vector2(0, 1), attacks);
				elapsedTime = 0;
			}
			if (keyboardState.IsKeyDown(Keys.Left) && elapsedTime > 1 / fireRate)
			{
				Shoot(new Vector2(-1,0), attacks);
				elapsedTime = 0;
			}

            // Update position
            if (acceleration.LengthSquared() != 0) acceleration.Normalize();	

			float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

			float accLength = 15000;
			float friction = 0.35f;
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
		
		// Function for shooting projectiles
		private void Shoot (Vector2 direction, List<Attack> attacks)
		{
			// Set up projectile spread
			double angle = Math.Atan2(direction.Y, direction.X);

			angle -= shotSpread * (projectiles - 1) / (2 * projectiles);	
			
			// Fire projectiles
			for (int i = 0; i < projectiles; i++)
			{
				double shotAngle = angle + i * shotSpread / projectiles;
				Vector2 velocity = new Vector2((float)Math.Cos(shotAngle), (float)Math.Sin(shotAngle)) * shotSpeed;
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
