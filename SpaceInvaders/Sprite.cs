using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Sprite
    {
        public Vector2 position;
       public Texture2D image;
        Color tint;
        
        //example stuff
        public int Num
        {
            get { return 1; }
        }
        
        //make a hitbox property like the num example
        public Rectangle Hitbox
        {
             get { return new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height); }
        }

        public Sprite(Vector2 position, Texture2D image, Color tint)
        {
            this.position = position;
            this.image = image;
            this.tint = tint;            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, tint);
        }
    }
}
