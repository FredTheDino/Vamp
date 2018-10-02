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
        Texture2D test, playersprite, pixel, arrow;
        Player player;
		Camera camera;
		List<Attack> attacks;
        List<Enemy> enemies;
		GameManager gameManager;

		Floor floor;
        
        public VampGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "..\\res";

			RNG.Seed(34563377);
        }

		// Initialize variables
        protected override void Initialize()
        {
            player = new Player(new Vector2(160, 160));
			attacks = new List<Attack>();
            enemies = new List<Enemy>();
            
            for (int i = 0; i < 20; i++)
            {
                int x = 160 + i * 30;
                enemies.Add(new Enemy(new Vector2(x, 200)));
            }

			gameManager = new GameManager(attacks, enemies);
			camera = new Camera(player, GraphicsDevice);
			floor = new Floor(5, 2);
            
			base.Initialize();
        }

		// Load Textures
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

			// Load sprites that are in the game
            test = Content.Load<Texture2D>("test");
            playersprite = Content.Load<Texture2D>("player");
            pixel = Content.Load<Texture2D>("pixel");
			arrow = Content.Load<Texture2D>("vamp_arrow");
			Debug.pixel = pixel;
        }


        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

		// Update the game (called every frame)
        protected override void Update(GameTime time)
        {
			// Exit if escape is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || 
				Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
                Exit();
			}

			// Update player
            player.Update(time, Keyboard.GetState(), attacks);
			
			floor.Update(player);

            foreach (Enemy enemy in enemies)
            {
                enemy.Update(player, (float) time.ElapsedGameTime.TotalSeconds, attacks);
                if(!enemy.IsAlive()) gameManager.MarkForRemoval(enemy);
            }

			// Update attacks
			foreach (Attack attack in attacks)
			{
				attack.Update(time);
				// Update against the room
				if (!attack.IsAlive()) gameManager.MarkForRemoval(attack);
			}
			
			// Remove objects we don't want
			gameManager.RemoveObjects();

			// Test camera shake, TEMPORARY
			if (Keyboard.GetState().IsKeyDown(Keys.R))
				camera.Shake((float) time.ElapsedGameTime.TotalSeconds * 2);
			camera.Update(time);

            base.Update(time);
        }

		// Draw the game (called every frame)
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(
					SpriteSortMode.Deferred,
					null, null, null, null, null, 
					camera.ViewMatrix);

            spriteBatch.Draw(pixel, player.Position, null, 
					Color.Blue, 
					0, Vector2.Zero, player.Dimension * player.Scale * 2, SpriteEffects.None, 0);

			floor.Draw(spriteBatch, pixel);

            spriteBatch.Draw(pixel, player.Position - player.Size(), null, 
					Color.Red, 
					0, Vector2.Zero, player.Size() * 2, SpriteEffects.None, 0);

			player.DrawCollider(spriteBatch);

            foreach (Enemy enemy in enemies)
            {
                spriteBatch.Draw(pixel, enemy.Position - enemy.Size(), null, 
                    Color.Gray, 
                    0, Vector2.Zero, enemy.Size() * 3, SpriteEffects.None, 0);
            }

			// Draw attacks
			foreach (Attack attack in attacks)
			{
				spriteBatch.Draw(arrow,
					   	attack.Position - attack.Size(),
					   	null,
						Color.White,
					   	attack.Rotation,
					   	new Vector2(arrow.Width, arrow.Height) / 2,
					   	attack.Dimension * attack.Scale * 2,
					   	SpriteEffects.None,
						0);
			}

            spriteBatch.End();

            base.Draw(time);
        }
    }
}

