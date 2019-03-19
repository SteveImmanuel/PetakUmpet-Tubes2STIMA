using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
namespace Tubes2Stima
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class VisualizeGraph : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private SpriteFont font;
        private Graph G;
        private int width, height;
        private int nNode;
        private bool animateInitGraph;
        Texture2D t, rec;
        MouseState mouseState;
        KeyboardState keyboardState;
        private int previousScrollValue;
        private int previousXValue;
        private int previousYValue;

        public VisualizeGraph(ref Graph _G, int _w, int _h, int _n)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            G = _G;
            width = _w;
            height = _h;
            nNode = _n;
            this.IsMouseVisible = true;
            previousScrollValue = mouseState.ScrollWheelValue;
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
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 100.0);
            this.IsFixedTimeStep = false;
            G.generateWeight(G.getNode(0), 0);
            G.unvisitAll();
            G.GeneratePosition(this.width, this.height);
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
            t.SetData<Color>(new Color[] { Color.White });
            rec = new Texture2D(GraphicsDevice, 1, 1);
            rec.SetData(new[] { Color.White });
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            base.UnloadContent();
            spriteBatch.Dispose();
            rec.Dispose();
            t.Dispose();
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
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            int curScrollVal = mouseState.ScrollWheelValue;
            if (curScrollVal > previousScrollValue)
            {
                width += 20;
                height += 20;
                previousScrollValue = curScrollVal;
                G.GeneratePosition(width, height);
            }
            else if (curScrollVal < previousScrollValue)
            {
                width -= 20;
                height -= 20;
                previousScrollValue = curScrollVal;
                G.GeneratePosition(width, height);
            }
            else if (mouseState.RightButton == ButtonState.Pressed)
            {
                G.PanPosition(mouseState.X - previousXValue, mouseState.Y - previousYValue);
                previousXValue = mouseState.X;
                previousYValue = mouseState.Y;
            }
            else if (mouseState.RightButton == ButtonState.Released)
            {
                previousXValue = mouseState.X;
                previousYValue = mouseState.Y;
            }

            if (keyboardState.IsKeyDown(Keys.S))
            {
                Algorithm al = new Algorithm();
                Boolean found = al.SearchPath(0, G.getNode(0), G.getNode(2), G);
                Console.WriteLine(found);
            }
            else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            BarnesHut B = new BarnesHut(0,new Koordinat2D(width/2, height/2), width, height);
            //for (int i = 0; i < G.getNodeSize(); i++)
            //{
            //    B.addNodeSimulation(ref G.getNode(i));
            //}
            foreach(Node N in G.allNode)
            {
                Node NTemp = N;
                B.addNodeSimulation(ref NTemp);
            }
            foreach (Node N in G.allNode)
            {
                Node NTemp = N;
                Koordinat2D force = B.getForce(ref NTemp, 0.5);
                N.updatePos(force, 1 / 20.0);
            }
            //for (int i = 0; i < G.getNodeSize(); i++)
            //{
            //    Koordinat2D force = B.getForce(ref G.getNode(i), 0.5);
            //    G.getNode(i).updatePos(force, 1/20.0);
            //}
            G.normalizeKoordinat(width, height);
            base.Update(gameTime);
        }

       
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            for (int i = 0; i < this.G.getEdgeSize(); i++)
            {
                Edge tempEdge = this.G.getEdge(i);
                Node tempNode1 = G.getNode(tempEdge.getFrom());
                Node tempNode2 = G.getNode(tempEdge.getTo());
                DrawLine(
                    tempNode1.pos.copy(),
                    tempNode2.pos.copy()
                , 1, tempEdge.getColor());
            }

            for (int i = 0; i < this.G.getNodeSize(); i++)
            {
                Node temp = G.getNode(i);
                float x = (float)temp.pos.x;
                float y = (float)temp.pos.y;
                float tempPad = (float)(0.5 * Math.Sqrt(Math.Pow(20, 2) * 2));
                spriteBatch.Draw(rec, new Vector2(x - tempPad, y - tempPad), null, Color.Chocolate, 0f, Vector2.Zero, new Vector2(20, 20), SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, temp.getID().ToString(), new Vector2(x - tempPad, y - tempPad), Color.Black, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawLine(Koordinat2D start, Koordinat2D end, int thick, int colorID)
        {
            Koordinat2D edge = end - start;
            float angle = (float)Math.Atan2(edge.y, edge.x);
            Color color;
            if (colorID == 0)
            {
                color = Color.Black;
            }
            else
            {
                color = Color.Red;
            }
            spriteBatch.Draw(t,
                new Rectangle(
                    (int)start.x,
                    (int)start.y,
                    (int)edge.magnitude(),
                    thick), //width of line, change this to make thicker line
                null,
                color, //colour of line
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
