using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Vamp
{
    public class Player : GameObject
    {
        // The players velocity
        private Vector2 velocity;

		// The players firerate and elapsedtime from last fire
		private float firerate, elapsedTime;

        // Main constructor
        public Player (Vector2 position) : base(position, new Vector2(32,32), new Vector2(1, 1), new Collider(true, Shape.Circle))
        {
            velocity = new Vector2();
			firerate = 2f;
			elapsedTime = firerate;
        }

        // Update the player every frame
        public void Update (GameTime gameTime, KeyboardState keyboardState, List<Attack> attacks)
        {
			// Add time to the elapsed time
			elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Read keyboard input
            if (keyboardState.IsKeyDown(Keys.W))
            {
                velocity += new Vector2(0,-1);
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                velocity += new Vector2(0, 1);
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
            }
			if (keyboardState.IsKeyDown(Keys.Up) && elapsedTime > 1 / firerate)
			{
				Attack attack = new Attack(Position, new Vector2(0,-200), new Vector2(32,32), new Vector2(1,1), new Collider(true, Shape.Circle), 5f);
				attacks.Add(attack);
				elapsedTime = 0;
			}
			if (keyboardState.IsKeyDown(Keys.Right) && elapsedTime > 1 / firerate)
			{
				Attack attack = new Attack(Position, new Vector2(200,0), new Vector2(32,32), new Vector2(1,1), new Collider(true, Shape.Circle), 5f);
				attacks.Add(attack);
				elapsedTime = 0;
			}
			if (keyboardState.IsKeyDown(Keys.Down) && elapsedTime > 1 / firerate)
			{
				Attack attack = new Attack(Position, new Vector2(0,200), new Vector2(32,32), new Vector2(1,1), new Collider(true, Shape.Circle), 5f);
				attacks.Add(attack);
				elapsedTime = 0;
			}
			if (keyboardState.IsKeyDown(Keys.Left) && elapsedTime > 1 / firerate)
			{
				Attack attack = new Attack(Position, new Vector2(-200,0), new Vector2(32,32), new Vector2(1,1), new Collider(true, Shape.Circle), 5f);
				attacks.Add(attack);
				elapsedTime = 0;
			}
            // Update position
            if (velocity.X != 0 && velocity.Y != 0) velocity.Normalize();

            Position += velocity * 400 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity = Vector2.Zero;
        }
    }
}
