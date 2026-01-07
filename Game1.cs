using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;

namespace endgame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle window;
        List <Texture2D> gobtex;
        Texture2D homebase;
        Rectangle homebaserec;

       
        List <bad_guy> goblins;
        int basehelth = 3;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {

           
            gobtex = new List<Texture2D>();
            goblins = new List<bad_guy>();
            basehelth = 3;

            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;

            _graphics.ApplyChanges();
            
            
            base.Initialize();
            for (int i = 0; i < 10; i++)
            {
                goblins.Add(new bad_guy(gobtex, new Rectangle(0, 40 * i
                    , 40, 40)));
            }
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            gobtex.Add(Content.Load<Texture2D>("goblin/goblin"));
            homebase = Content.Load<Texture2D>("homebase1");
            homebaserec = new Rectangle(200, 185, 64, 64);



        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            for (int i = 0; i < goblins.Count; i++)
            {
                goblins[i].update();
                if (homebaserec.Intersects(goblins[i].Location))
                {
                    basehelth -= 1;
                    goblins.RemoveAt(i);
                    i--;

                }
            }
            if (basehelth <= 0)
            {
                Exit();
            }




            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            for (int i = 0; i < goblins.Count; i++)
            {


                goblins[i].draw(_spriteBatch);
            }
            _spriteBatch.Draw(homebase, homebaserec, Color.White);




            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
