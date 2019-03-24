using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        private Random random = new Random();
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Ship ship;
        private int lives = 3;
        private int shieldhealth = 250;
        private List<Alien> aliens;
        private Texture2D bulletImage;
        private List<Bullet> shipbullets;
        private List<Bullet> alienbullets;
        private List<Shield> shields;
        private KeyboardState ks;
        private SpriteFont font;

        //List<Shield>

        private TimeSpan spawnEnemyBulletTime = TimeSpan.FromMilliseconds(200);
        private TimeSpan elapsedSpawnEnemyBullet;

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
            alienbullets = new List<Bullet>();
            shields = new List<Shield>();
            int alienpositiony = 0;
            int alienpositionx = 0;
            int shieldpositiony = GraphicsDevice.Viewport.Height / 2;
            int shieldpositionx = 400;
            lives = 3;

            for (int i = 0; i < 13; i++)
            {
                aliens.Add(new Alien(Content.Load<Texture2D>("Space Invaders"), new Vector2(alienpositionx, alienpositiony), new Vector2(2, 0), Color.Blue));
                alienpositionx += 130;
                alienpositiony += 0;
            }


            for (int i = 0; i < 3; i++)
            {
                shields.Add(new Shield(new Vector2(shieldpositionx, shieldpositiony), Content.Load<Texture2D>("shield"), Color.White, font, shieldhealth));
                shieldpositionx += 450;
                shieldpositiony += 0;
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

            font = Content.Load<SpriteFont>("Text");

            Reset();



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
            {
                Exit();
            }

            KeyboardState lastKs = ks;
            ks = Keyboard.GetState();

            elapsedSpawnEnemyBullet += gameTime.ElapsedGameTime;

            if (elapsedSpawnEnemyBullet >= spawnEnemyBulletTime)
            {
                elapsedSpawnEnemyBullet = TimeSpan.Zero;
                if (aliens.Count != 0)
                {
                    Bullet newBullet = new Bullet(bulletImage, aliens[random.Next(aliens.Count)].position + new Vector2(55, 65), new Vector2(0, 10), Color.White)
                    {
                        spriteEffects = SpriteEffects.FlipVertically
                    };
                    alienbullets.Add(newBullet);
                }

            }

            if (ks.IsKeyDown(Keys.Q) && lastKs.IsKeyUp(Keys.Q))
            {
                aliens.Add(new Alien(Content.Load<Texture2D>("Space Invaders"), new Vector2(0, 0), new Vector2(2, 0), Color.Red));

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

            for (int i = 0; i < alienbullets.Count; i++)
            {
                alienbullets[i].Update();
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
                        //remove the bullet
                        shipbullets.RemoveAt(i);
                    }
                }
            }

            for (int i = 0; i < alienbullets.Count; i++)
            {
                if (alienbullets[i].Hitbox.Intersects(ship.Hitbox))
                {
                    lives--;
                    alienbullets.Remove(alienbullets[i]);
                }

            }

            for (int i = 0; i < alienbullets.Count; i++)
            {
                bool shouldbreak = false;
                for (int x = 0; x < shields.Count; x++)
                {
                    if (alienbullets[i].Hitbox.Intersects(shields[x].Hitbox))
                    {
                        shields[x].ReduceHealth();
                        //shieldhealth--;
                        alienbullets.Remove(alienbullets[i]);
                        shouldbreak = true;
                        break;
                    }

                    
                }

                if(shouldbreak)
                {
                    break;
                }
            }
            for (int i = 0; i < shipbullets.Count; i++)
            {
                for (int x = 0; x < shields.Count; x++)
                {
                    if (shipbullets[i].Hitbox.Intersects(shields[x].Hitbox))
                    {
                        shields[x].ReduceHealth();
                        shipbullets.Remove(shipbullets[i]);
                        break;
                     
                    }
                }
            }

            for (int i = 0; i < shields.Count; i++)
            {
                if (shields[i].Health <= 0)
                {
                    shields.RemoveAt(i);
                    i--;
                }
            }

            if (lives == 0)
            {
                shipbullets.Clear();
                alienbullets.Clear();
                for (int i = 0; i < aliens.Count; i++)
                {
                    aliens[i].position = Vector2.Zero;
                }
            }
            

            if (ks.IsKeyDown(Keys.R))
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
            for (int i = 0; i < shields.Count; i++)
            {
                shields[i].Draw(spriteBatch);
            }

            for (int i = 0; i < shipbullets.Count; i++)
            {
                shipbullets[i].Draw(spriteBatch);
            }

            for (int i = 0; i < alienbullets.Count; i++)
            {
                alienbullets[i].Draw(spriteBatch);
            }

            ship.Draw(spriteBatch);
            for (int i = 0; i < aliens.Count; i++)
            {
                aliens[i].Draw(spriteBatch);
            }

            if (aliens.Count == 0)
            {
                spriteBatch.DrawString(font, "You Win! Press R to Restart", new Vector2(GraphicsDevice.Viewport.Width / 2 - 200, GraphicsDevice.Viewport.Height / 2 - 100), Color.Goldenrod);

            }

            if (lives == 0 && aliens.Count > 0)
            {
                spriteBatch.DrawString(font, "You Lose! Press R to Restart", new Vector2(GraphicsDevice.Viewport.Width / 2 - 200, GraphicsDevice.Viewport.Height / 2 - 100), Color.Red);

            }
            spriteBatch.DrawString(font, $"Lives: {lives}", new Vector2(0, 0), Color.Turquoise);
            // TODO: Add your drawing code here

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
