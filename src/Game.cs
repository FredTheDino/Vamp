using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Vamp
{
    public class VampGame : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D test, playersprite, pixel;
        Player player;

		Room room;
        
        public VampGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "..\\res";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player(new Vector2(50, 50));
			room = new Room(1, 1, 7, 7);
			
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = Content.Load<Texture2D>("test");
            playersprite = Content.Load<Texture2D>("player");
            pixel = Content.Load<Texture2D>("pixel");
			Debug.pixel = pixel;
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime time)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
				Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
                Exit();
			}

            player.Update(time, Keyboard.GetState());

            base.Update(time);
        }

		float counter;
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);



			// TEMPORARY!
			counter += (float) time.ElapsedGameTime.TotalSeconds;
			GameObject a = new GameObject(
					new Vector2(56.0f, 84.0f),
					new Vector2(32.0f, 32.0f + 16.0f * (float) Math.Sin(counter)),
					new Vector2(1, 1),
					new Collider(false, Shape.Box));
			CollisionSystem system = new CollisionSystem();
			Overlap overlap = system.Check(a, player);
			overlap.Solve();
			
            spriteBatch.Begin();

            //spriteBatch.Draw(test, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            spriteBatch.Draw(pixel, player.Position - player.Size(), null, 
					overlap ? Color.Red : Color.Green, 
					0, Vector2.Zero, player.Size() * 2, SpriteEffects.None, 0);

            spriteBatch.Draw(pixel, a.Position - a.Size(), null, Color.Black, 
					0, Vector2.Zero, a.Size() * 2, SpriteEffects.None, 0);

			room.Draw(spriteBatch, pixel);

			a.DrawCollider(spriteBatch);
			player.DrawCollider(spriteBatch);

            spriteBatch.End();

            base.Draw(time);
        }
    }
}

