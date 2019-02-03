using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;


//Sprite:
//  Position
//  Image
//  Tint
//  Hitbox
//  Draw()

//Bullet : Sprite
// Speed
// Update

//Ship : Sprite
//  Speed
//  List<Bullet>
//  Update
//  override Draw

//Alien : Sprite
//  Speed
//  List<Bullet>
//  Update
//  override Draw

namespace SpaceInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Ship ship;
        List<Alien> aliens;
        bool cool;
        Texture2D bulletImage;
        List<Bullet> shipbullets;
        KeyboardState ks;
        SpriteFont font;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 1800;
            graphics.PreferredBackBufferHeight = 980;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        public void Reset()
        {
            shipbullets = new List<Bullet>();
            aliens = new List<Alien>();
            int positiony = 0;
            int positionx = 0;

            for (int i = 0; i < 9; i++)
            {
                aliens.Add(new Alien(Content.Load<Texture2D>("Space Invaders"), new Vector2(positionx, positiony), new Vector2(2, 0), Color.Red));
                positionx += 130;
                positiony += 0;
            }
            bulletImage = Content.Load<Texture2D>("Bullet");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            ship = new Ship(Content.Load<Texture2D>("SpaceShip"), new Vector2(850, 850), new Vector2(5, 0), Color.Green);

            Reset();

            font = Content.Load<SpriteFont>("Text");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState lastKs = ks;
            ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Q) && lastKs.IsKeyUp(Keys.Q))
            {
                aliens.Add(new Alien(Content.Load<Texture2D>("Space Invaders"), new Vector2(0,0), new Vector2(2, 0), Color.Red));
                
            }
            if (ks.IsKeyDown(Keys.Space) && lastKs.IsKeyUp(Keys.Space))
            {
                shipbullets.Add(new Bullet(bulletImage, ship.position + new Vector2(80, -55), new Vector2(0, -10), Color.White));
            }

            if (ks.IsKeyDown(Keys.W))
            {
                shipbullets.Add(new Bullet(bulletImage, ship.position + new Vector2(80, -55), new Vector2(0, -10), Color.White));
            }

            ship.Update(GraphicsDevice.Viewport);

            for (int i = 0; i < aliens.Count; i++)
            {
                aliens[i].Update(GraphicsDevice.Viewport);
            }

            for (int i = 0; i < shipbullets.Count; i++)
            {
                shipbullets[i].Update();
            }

            for (int i = 0; i < shipbullets.Count; i++)
            {
                for (int x = 0; x < aliens.Count; x++)
                {
                    if (shipbullets[i].Hitbox.Intersects(aliens[x].Hitbox))
                    {
                        aliens.Remove(aliens[x]);
                    }
                }
            }
            if(ks.IsKeyDown(Keys.R))
            {
                Reset();
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            for (int i = 0; i < shipbullets.Count; i++)
            {
                shipbullets[i].Draw(spriteBatch);
            }
            ship.Draw(spriteBatch);
            for (int i = 0; i < aliens.Count; i++)
            {
                aliens[i].Draw(spriteBatch);
            }
            if (aliens.Count == 0)
            {
                spriteBatch.DrawString(font, "You Win! Press R to Restart", new Vector2(GraphicsDevice.Viewport.Width / 2 - 200, GraphicsDevice.Viewport.Height / 2 - 100), Color.Turquoise);
            }
            // TODO: Add your drawing code here

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
