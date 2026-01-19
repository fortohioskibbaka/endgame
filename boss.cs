using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace endgame
{
    internal class boss
    {
       
        private List<Texture2D> _textures;
        private Vector2 _speed;
        private Rectangle _location;
        private int _textureIndex;
        private SpriteEffects _direction;

       
        private int _health;

        public boss(List<Texture2D> textures, Rectangle location)
        {
            _textures = textures;
            _speed = new Vector2(0, 2);
            _location = location;
            _textureIndex = 0;
            _direction = SpriteEffects.None;

           
            _health = 3;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textures[_textureIndex], _location, null, Color.White, 0f, Vector2.Zero, _direction, 1f);
        }

        public void update()
        {
            _location.X += (int)_speed.X;
            _location.Y += (int)_speed.Y;

            if (_location.Left < 0 || _location.Right > 800)
            {
                _speed.X = -_speed.X;
                _speed.Y = 0;

                _direction = _speed.X < 0 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            }
            if ((_location.Top < 0 && _speed.Y < 0) || _location.Bottom > 600)
            {
                _speed.Y = -_speed.Y;
                _speed.X = 2;
            }
        }

        
        public Rectangle Location
        {
            get { return _location; }
        }

        
        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        
      
        

       
      
    }
}
