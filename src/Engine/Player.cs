using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Vamp
{
    public class Player : GameObject
    {
        // The players velocity
        private Vector2 velocity;

        // Main constructor
        public Player (Vector2 position) : base(position, new Vector2(32,32), new Collider(true, Shape.Circle))
        {
            velocity = new Vector2();
        }

        // Update the player every frame
        public void Update (GameTime gameTime, KeyboardState keyboardState)
        {
            // Read keyboard input
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                velocity += new Vector2(0,-1);
            }
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                velocity += new Vector2(1, 0);
            }
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                velocity += new Vector2(0, 1);
            }
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                velocity += new Vector2(-1, 0);
            }

            // Update position
            if (velocity.X != 0 && velocity.Y != 0) velocity.Normalize();

            Position += velocity * 400 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity = Vector2.Zero;
        }
    }
}
