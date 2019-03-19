using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tubes2Stima
{
    public class VisualizeGraph : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D t;
        Texture2D rec;
        MouseState mouseState;
        KeyboardState keyboardState;
        private SpriteFont font;
        private Graph G;
        private int width, height;
        private int previousScrollValue;
        private int previousXValue;
        private int previousYValue;

        public VisualizeGraph(ref Graph _G, int _w, int _h)
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            G = _G;
            width = _w;
            height = _h;
            this.IsMouseVisible = true;
            previousScrollValue = mouseState.ScrollWheelValue;
            graphics.PreferredBackBufferWidth = width;
            graphics.PreferredBackBufferHeight = height;
            graphics.ApplyChanges();
        }
        
        protected override void Initialize()
        {
            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / 30.0f);
            this.IsFixedTimeStep = false;
            G.generateWeight(G.getNode(0), 0);
            G.unvisitAll();
            G.GeneratePosition(this.width, this.height);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("NodeID");
            t = new Texture2D(GraphicsDevice, 1, 1);
            t.SetData<Color>(new Color[] { Color.White });
            rec = new Texture2D(GraphicsDevice, 1, 1);
            rec.SetData(new[] { Color.White });
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            spriteBatch.Dispose();
            rec.Dispose();
            t.Dispose();
        }

        protected override void Update(GameTime gameTime)
        {
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
                Boolean found=al.SearchPath(0, G.getNode(0), G.getNode(122), G);
                Console.WriteLine(found);
            }else if (keyboardState.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
          
            base.Update(gameTime);
        }

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
                    new Vector2(tempNode1.getX(), tempNode1.getY()),
                    new Vector2(tempNode2.getX(), tempNode2.getY())
                , 1,tempEdge.getColor());
            }

            for (int i = 0; i < this.G.getNodeSize(); i++)
            {
                Node temp = G.getNode(i);
                float x = (float)temp.getX();
                float y = (float)temp.getY();
                float tempPad = (float)(0.5 * Math.Sqrt(Math.Pow(20, 2) * 2));
                spriteBatch.Draw(rec, new Vector2(x-tempPad, y-tempPad), null,Color.Chocolate, 0f, Vector2.Zero, new Vector2(20, 20),SpriteEffects.None, 0f);
                spriteBatch.DrawString(font,temp.getID().ToString(), new Vector2(x-tempPad, y-tempPad), Color.Black,0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawLine(Vector2 start, Vector2 end,int thick,int colorID)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            Color color;
            if (colorID == 0){
                color = Color.Black;
            }
            else
            {
                color = Color.Red;
            }
            spriteBatch.Draw(t,
                new Rectangle(
                    (int)start.X,
                    (int)start.Y,
                    (int)edge.Length(),
                    thick), //width of line, change this to make thicker line
                null,
                color, //colour of line
                angle,     //angle of line (calulated above)
                new Vector2(0, 0), // point in line about which to rotate
                SpriteEffects.None,
                0);

        }
    }
}
