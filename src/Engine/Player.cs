using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Vamp
{
    public class Player : GameObject
    {
        // The players velocity
        private Vector2 velocity;

        // Main constructor
        public Player (Vector2 position) : base(position, new Vector2(32,32), new Vector2(1, 1), new Collider(true, Shape.Circle))
        {
            velocity = new Vector2();
        }

        // Update the player every frame
        public void Update (GameTime gameTime, KeyboardState keyboardState, List<Attack> attacks)
        {
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
			if (keyboardState.IsKeyDown(Keys.Up))
			{
				Attack attack = new Attack(Position, new Vector2(0,-200), new Vector2(1,1), new Collider(true, Shape.Circle), 5f);
				attacks.add(attack);
			}
			if (keyboardState.IsKeyDown(Keys.Right))
			{
				Attack attack = new Attack(Position, new Vector2(200,0), new Vector2(1,1), new Collider(true, Shape.Circle), 5f);
				attacks.add(attack);
			}
			if (keyboardState.IsKeyDown(Keys.Down))
			{
				Attack attack = new Attack(Position, new Vector2(0,200), new Vector2(1,1), new Collider(true, Shape.Circle), 5f);
				attacks.add(attack);
			}
			if (keyboardState.IsKeyDown(Keys.Left))
			{
				Attack attack = new Attack(Position, new Vector2(-200,0), new Vector2(1,1), new Collider(true, Shape.Circle), 5f);
				attacks.add(attack);
			}
            // Update position
            if (velocity.X != 0 && velocity.Y != 0) velocity.Normalize();

            Position += velocity * 400 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity = Vector2.Zero;
        }
    }
}
