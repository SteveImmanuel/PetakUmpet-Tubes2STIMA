using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
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
        private bool animateInitGraph;
        Texture2D t;
        private List<Body> listBody = new List<Body>();
        private QuadNode quadTree;
        private List<Quad> quads = new List<Quad>();

        public Game1(ref Graph _G, int _w, int _h, int _n)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            G = _G;
            width = _w;
            height = _h;
            nNode = _n;
            graphics.PreferredBackBufferWidth = width+600;  // set this value to the desired width of your window
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
            animateInitGraph = true;
            G.ForceDirected(this.width, this.height, false);
            for (int i = 0; i < G.getSize(); i++)
            {
                listBody.Add(new Body(G.getNode(i)));
            }
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
            t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData<Color>(
                new Color[] { Color.White });
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
            Koordinat2D center = new Koordinat2D(width / 2, height / 2);
            quadTree = new QuadNode(1, center, height);
            foreach (Body bod in listBody)
            {
                quadTree.addBody(bod);
            }
            foreach(Body body in listBody)
            {
                quadTree.interact(body, 0.5f);
                body.update();
            }
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
                Node temp = G.getNode(i);
                int x = (int)temp.getX();
                int y = (int)temp.getY();
                x = normKoorDraw(0, width, x, 60);
                y = normKoorDraw(0, height, y, 20);
                spriteBatch.DrawString(font, "Id : " + G.getNode(i).getID(), new Vector2(x, y), Color.Black);
                //Gambar garis ke tetangga
                for (int j = 0; j < temp.neighborSize(); j++)
                {
                    Node tetanggaTemp = temp.getNeighbor(j);
                    int xT = (int)tetanggaTemp.getX();
                    int yT = (int)tetanggaTemp.getY();
                    xT = normKoorDraw(0, width, xT, 60);
                    yT = normKoorDraw(0, height, yT, 20);
                    DrawLine(spriteBatch, //draw line
                        new Vector2(x, y), //start of line
                        new Vector2(xT, yT) //end of line
                    );
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawLine(SpriteBatch sb, Vector2 start, Vector2 end)
        {
            Vector2 edge = end - start;
            // calculate angle to rotate line
            float angle =
                (float)Math.Atan2(edge.Y, edge.X);


            sb.Draw(t,
                new Rectangle(// rectangle defines shape of line and position of start of line
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(), //sb will strech the texture to fill this rectangle
                    1), //width of line, change this to make thicker line
                null,
                Color.Red, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }

        public int normKoorDraw(int min, int max, int val, int delta)
        {
            return (min+delta) + (max-min - 2 * delta) * (val-min) /(max-min);
        }
    }
}
