using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Tubes2Stima
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont font;
        private Graph G;
        private int width, height;
        private int nNode;
        public Game1(ref Graph _G, int _w, int _h, int _n)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            G = _G;
            width = _w;
            height = _h;
            nNode = _n;
            graphics.PreferredBackBufferWidth = width;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = height;   // set this value to the desired height of your window
            graphics.ApplyChanges();
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
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 30.0f);
            G.ForceDirected(this.width, this.height, false);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("NodeID");
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

            // TODO: Add your update logic here
            G.updatePosFD(this.width, this.height);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            for (int i = 0; i < nNode; i++)
            {
                int x = (int)G.getNode(i).getX();
                int y = (int)G.getNode(i).getY();
                x = normKoorDraw(0, width, x, 60);
                y = normKoorDraw(0, height, y, 20);
                spriteBatch.DrawString(font, "Id : "+G.getNode(i).getID(), new Vector2(x, y), Color.Black);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private int normKoorDraw(int min, int max, int val, int delta)
        {
            return (min+delta) + (max-min - 2 * delta) * (val-min) /(max-min);
        }
    }
}
