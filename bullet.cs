using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace endgame
{
    internal class bullet
    {
       
        

        public Texture2D _texture;
        public Rectangle _location;
        private Vector2 _speed;
        private Vector2 _position;

        public bullet(Rectangle location, Texture2D texture, Vector2 speed)
        {
            _texture = texture;
            _speed = speed;
            _location = location;
            _position = new Vector2(location.X, location.Y);
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _location, Color.White);
        }

        public void update()
        {
            
            _position.X += _speed.X;
            _location.X = (int)Math.Round(_position.X);
        }
    }
}
