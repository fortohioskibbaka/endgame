using endgame.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using static System.Net.Mime.MediaTypeNames;

namespace endgame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Rectangle window;
        List<Texture2D> gobtex;
        Texture2D homebase;
        Rectangle homebaserec;
        Texture2D background;
        Texture2D bullettex;
        List<bullet> bullettexlist;

        float _fireTimer = 0.2f;
        float _fireTime = 0f;

      
        float _crossbowFireTimer = 1f;
        float _crossbowFireTime = 0f;

        bool crossbowvisible = false;

        List<bad_guy> goblins;
        int basehelth = 3;
        int coins = 0;
        SpriteFont font1;
        List<boss> bosstexlist;
        List<Texture2D> bostex;
        List<jef> jefftexlist;
        List<Texture2D> jefftex;

        Texture2D crossbow;
        Rectangle crossrec;
        bool crossbowactive = false;

        List<Infinitegob> infinitegobtexlist;
        List<Texture2D> infgobtex;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            jefftex = new List<Texture2D>();
            jefftexlist = new List<jef>();

            bostex = new List<Texture2D>();
            bosstexlist = new List<boss>();
            gobtex = new List<Texture2D>();
            goblins = new List<bad_guy>();
            bullettexlist = new List<bullet>();
            
            infgobtex = new List<Texture2D>();
           
           
            infinitegobtexlist = new List<Infinitegob>();


            basehelth = 3;

            window = new Rectangle(0, 0, 800, 600);
            _graphics.PreferredBackBufferWidth = window.Width;
            _graphics.PreferredBackBufferHeight = window.Height;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            jefftex.Add(Content.Load<Texture2D>("jeffry"));
            gobtex.Add(Content.Load<Texture2D>("goblin/goblin"));
            homebase = Content.Load<Texture2D>("homebase1");
            background = Content.Load<Texture2D>("path2");
            homebaserec = new Rectangle(170, 185, 100, 100);
            bullettex = Content.Load<Texture2D>("bullet");
            font1 = Content.Load<SpriteFont>("coinword");
            bostex.Add(Content.Load<Texture2D>("boss/bossgoblin"));
            crossbow = Content.Load<Texture2D>("crossbow2");
          
            infgobtex.Add(Content.Load<Texture2D>("ingob"));


            crossrec = new Rectangle(250, 100, 50, 50);

            for (int i = 0; i < 30; i++)
            {
                goblins.Add(new bad_guy(gobtex, new Rectangle(0, 0 - (40 * i)
                    , 40, 40)));
            }

            for (int i = 0; i < 15; i++)
            {
                bosstexlist.Add(new boss(bostex, new Rectangle(0, 0 - (40 * i)
                    , 45, 45)));
            }

            for (int i = 0; i < 3; i++)
            {
                jefftexlist.Add(new jef(jefftex, new Rectangle(0, 0 - (50 * i)
                    , 40, 40)));
                
                
            }

            for (int i = 0; i < 100; i++)
            {
                infinitegobtexlist.Add(new Infinitegob(infgobtex, new Rectangle(0, 0 - (50 * i)
                    , 40, 40)));
            }



        }

        private MouseState _previousMouseState;

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

            if (goblins.Count == 0)
            {
                for (int i = 0; i < bosstexlist.Count; i++)
                {
                    bosstexlist[i].update();
                    if (homebaserec.Intersects(bosstexlist[i].Location))
                    {
                        basehelth -= 1;
                        bosstexlist.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (bosstexlist.Count == 0)
            {
                for (int i = 0; i < jefftexlist.Count; i++)
                {
                    jefftexlist[i].update();
                    if (homebaserec.Intersects(jefftexlist[i].Location))
                    {
                        basehelth -= 3;
                        jefftexlist.RemoveAt(i);
                        i--;
                    }
                }
            }
            if (infinitegobtexlist.Count == 0)
            {
                for(int i = 0; i < infinitegobtexlist.Count; i++)
                {
                    infinitegobtexlist[i].update();
                    if (homebaserec.Intersects(infinitegobtexlist[i].Location))
                    {
                        basehelth -= 1;
                        infinitegobtexlist.RemoveAt(i);
                        i--;
                    }
                }


            }

            




            if (basehelth <= 0)
            {
                Exit();
            }

            
            _fireTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            _crossbowFireTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            MouseState mouse = Mouse.GetState();
            KeyboardState keyboard = Keyboard.GetState();

            bool mouseClicked = mouse.LeftButton == ButtonState.Pressed && _previousMouseState.LeftButton == ButtonState.Released;

            bool isFiring = mouseClicked || keyboard.IsKeyDown(Keys.Space);

            if (isFiring && _fireTime >= _fireTimer)
            {
                Rectangle bulletLocation = new Rectangle(200, 230, 20, 10);
                Vector2 bulletSpeed = new Vector2(10, 0);
                bullet newBullet = new bullet(bulletLocation, bullettex, bulletSpeed);
                bullettexlist.Add(newBullet);
                _fireTime = 0f;
            }

            
            if (crossbowvisible && _crossbowFireTime >= _crossbowFireTimer)
            {
               
                Rectangle bulletLocation = new Rectangle(250, 110, 20, 10);
                Vector2 bulletSpeed = new Vector2(10, 0);
                bullet newBullet = new bullet(bulletLocation, bullettex, bulletSpeed);
                bullettexlist.Add(newBullet);
                _crossbowFireTime = 0f;
                
            }
            


            if (bullettexlist.Count > 0)
            {
                for (int i = 0; i < bullettexlist.Count; i++)
                {
                    if (bullettexlist[i]._location.X > window.Width)
                    {
                        bullettexlist.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (bullettexlist.Count > 0 && goblins.Count > 0)
            {
                for (int i = 0; i < bullettexlist.Count; i++)
                {
                    for (int j = 0; j < goblins.Count; j++)
                    {
                        if (bullettexlist[i]._location.Intersects(goblins[j].Location))
                        {
                            coins += 1;
                            goblins.RemoveAt(j);
                            bullettexlist.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }

            if (bullettexlist.Count > 0 && goblins.Count == 0 && bosstexlist.Count > 0)
            {
                for (int i = 0; i < bullettexlist.Count; i++)
                {
                    for (int j = 0; j < bosstexlist.Count; j++)
                    {
                        if (bullettexlist[i]._location.Intersects(bosstexlist[j].Location))
                        {
                            coins += 1;
                            bosstexlist[j].Health -= 1;

                            bullettexlist.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }

            if (bosstexlist.Count > 0)
            {
                for (int i = 0; i < bosstexlist.Count; i++)
                {
                    if (bosstexlist[i].Health <= 0)
                    {
                        bosstexlist.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (bullettexlist.Count > 0 && goblins.Count == 0 && bosstexlist.Count == 0 && jefftexlist.Count > 0)
            {
                for (int i = 0; i < bullettexlist.Count; i++)
                {
                    for (int j = 0; j < jefftexlist.Count; j++)
                    {
                        if (bullettexlist[i]._location.Intersects(jefftexlist[j].Location))
                        {
                            

                            coins += 1;
                            jefftexlist[j].Health -= 1;
                            bullettexlist.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }
            if (jefftexlist.Count > 0)
            {
                for (int i = 0; i < jefftexlist.Count; i++)
                {
                    if (jefftexlist[i].Health <= 0)
                    {
                        jefftexlist.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (bullettexlist.Count > 0 && goblins.Count == 0 && bosstexlist.Count == 0 && infinitegobtexlist.Count > 0)
            {
                

                for (int i = 0; i < bullettexlist.Count; i++)
                {
                    for (int j = 0; j < infinitegobtexlist.Count; j++)
                    {
                        if (bullettexlist[i]._location.Intersects(infinitegobtexlist[j].Location))
                        {
                            coins += 1;
                            infinitegobtexlist[i].update();
                            bullettexlist.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
            }

            




            if (coins >= 50)
            {
                crossbowvisible = true;
            }
            if (crossbowactive == false && crossbowvisible == true)
            {
                coins -= 50;


            }

                crossbowactive = crossbowvisible;

            _previousMouseState = mouse;

            base.Update(gameTime);
            this.Window.Title = basehelth + "";
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(background, window, Color.White);
            for (int i = 0; i < goblins.Count; i++)
            {
                goblins[i].draw(_spriteBatch);
            }

            if (goblins.Count == 0)
            {
                for (int i = 0; i < bosstexlist.Count; i++)
                {
                    bosstexlist[i].draw(_spriteBatch);
                }
            }

            if (bosstexlist.Count == 0)
            {
                for (int i = 0; i < jefftexlist.Count; i++)
                {
                    jefftexlist[i].draw(_spriteBatch);
                }
            }
            if (jefftexlist.Count == 0)
            {
                for (int i = 0; i < infinitegobtexlist.Count; i++)
                {
                    infinitegobtexlist[i].draw(_spriteBatch);
                }
            }
           


            for (int i = 0; i < bullettexlist.Count; i++)
            {
                bullettexlist[i].update();
                bullettexlist[i].draw(_spriteBatch);
            }

            _spriteBatch.Draw(homebase, homebaserec, Color.White);

            if (crossbowvisible)
            {
                
                _spriteBatch.Draw(crossbow, crossrec, Color.White);
                
            }

            _spriteBatch.DrawString(font1, "coins" + coins, new Vector2(10, 10), Color.Black);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
