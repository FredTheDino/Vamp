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
		List<Attack> attacks;
        
        public VampGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "..\\res";
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            player = new Player(new Vector2(160, 160));
			room = new Room(1, 1, 7, 6);
			
			attacks = new List<Attack>();
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

			room.Overlap(player);
            player.Update(time, Keyboard.GetState(), attacks);
			foreach (Attack attack in attacks)
			{
				attack.Update(time);
				room.Overlap(attack);
			}

            base.Update(time);
        }

		float counter;
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);



			// TEMPORARY!
			counter += (float) time.ElapsedGameTime.TotalSeconds;
            spriteBatch.Begin();

            //spriteBatch.Draw(test, Vector2.Zero, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
			room.Draw(spriteBatch, pixel);

            spriteBatch.Draw(pixel, player.Position - player.Size(), null, 
					Color.Red, 
					0, Vector2.Zero, player.Size() * 2, SpriteEffects.None, 0);

			room.DebugDraw(spriteBatch);

			player.DrawCollider(spriteBatch);

			foreach (Attack attack in attacks)
			{
				spriteBatch.Draw(pixel, attack.Position - attack.Size(), null, Color.Blue, 0, Vector2.Zero, attack.Dimension * attack.Scale * 2, SpriteEffects.None, 0);
			}

            spriteBatch.End();

            base.Draw(time);
        }
    }
}

