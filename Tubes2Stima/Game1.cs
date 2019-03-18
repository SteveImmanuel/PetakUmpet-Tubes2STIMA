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
        Texture2D t;
        MouseState mouseState;
        private SpriteFont font;
        private Graph G;
        private int width, height;
        private int previousScrollValue;

        public Game1(ref Graph _G, int _w, int _h)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            G = _G;
            width = _w;
            height = _h;
            mouseState = Mouse.GetState();
            previousScrollValue = mouseState.ScrollWheelValue;
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
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
            this.IsFixedTimeStep = false;
            G.generateWeight(G.getNode(0), 0);
            G.unvisitAll();
            G.GeneratePosition(this.width, this.height);
            //for (int i = 0; i < G.getNodeSize(); i++)
            //{
            //    G.getNode(i).printInfo();
            //    Console.WriteLine();
            //}

            //for (int i = 0; i < this.G.getEdgeSize(); i++)
            //{
            //    Edge tempEdge = this.G.getEdge(i);
            //    Console.WriteLine("{0},{1}", G.getEdge(i).getFrom(), G.getEdge(i).getTo());
            //    Node tempNode1 = G.getNode(tempEdge.getFrom());
            //    Node tempNode2 = G.getNode(tempEdge.getTo());
            //    Console.WriteLine("edge antara : {0},{1}", tempNode1.getID(), tempNode2.getID());
            //}
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("NodeID");
            t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData<Color>(new Color[] { Color.White });
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            int curScrollVal = mouseState.ScrollWheelValue;
            if (curScrollVal > previousScrollValue)
            {
                width += 20;
                height += 20;
                previousScrollValue = curScrollVal;
            }else if(curScrollVal < previousScrollValue)
            {
                width -= 20;
                height -= 20;
                previousScrollValue = curScrollVal;
            }
            G.GeneratePosition(width, height);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.BackToFront, null);
            for (int i = 0; i < this.G.getEdgeSize(); i++)
            {
                Edge tempEdge = this.G.getEdge(i);
                Node tempNode1 = G.getNode(tempEdge.getFrom());
                Node tempNode2 = G.getNode(tempEdge.getTo());
                DrawLine(
                    new Vector2(tempNode1.getX(), tempNode1.getY()),
                    new Vector2(tempNode2.getX(), tempNode2.getY())
                , 1);
            }

            for (int i = 0; i < this.G.getNodeSize(); i++)
            {
                Node temp = G.getNode(i);
                int x = (int)temp.getX();
                int y = (int)temp.getY();
                spriteBatch.DrawString(font,temp.getID().ToString(), new Vector2(x, y), Color.Black);
                DrawBorder(new Rectangle(x, y+20, 20, 20), 2);
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawBorder(Rectangle rec, int thick)
        {
            Vector2 topLeft = new Vector2(rec.X, rec.Y);
            Vector2 topRight = new Vector2(rec.X+ rec.Width, rec.Y);
            Vector2 bottomLeft = new Vector2(rec.X, rec.Y- rec.Height);
            Vector2 bottomRight = new Vector2(rec.X+rec.Width, rec.Y- rec.Height);
            DrawLine(topLeft, topRight,thick);
            DrawLine(topLeft, bottomLeft, thick);
            DrawLine(bottomLeft, bottomRight, thick);
            DrawLine(bottomRight, topRight, thick);
        }

        private void DrawLine(Vector2 start, Vector2 end,int thick)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            spriteBatch.Draw(t,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(),
                    thick), //width of line, change this to make thicker line
                null,
                Color.Black, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
