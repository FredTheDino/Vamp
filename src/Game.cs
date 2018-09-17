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
        Texture2D test;
        
        public VampGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "..\\res";
			
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

			
			GameObject a = new GameObject(
					new Vector2(0.0f, 0.0f),
					new Vector2(1.0f, 1.0f),
					new Collider());
			GameObject b = new GameObject(
					new Vector2(0.3f, 0.8f),
					new Vector2(1.0f, 1.0f),
					new Collider());

			PhysicsSystem system = new PhysicsSystem();
			system.Check(a, b);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            test = Content.Load<Texture2D>("test");
            // TODO: use this.Content to load your game content here
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

            base.Update(time);
        }

        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            spriteBatch.Draw(test, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);

            spriteBatch.End();

            base.Draw(time);
        }
    }
}

