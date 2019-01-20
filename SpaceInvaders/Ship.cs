using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceInvaders
{
    class Ship : Sprite
    {
        Vector2 speed;
      
        public Ship(Texture2D tex, Vector2 position, Vector2 speed, Color tint) : base(position, tex, tint)
        {
            this.speed = speed;
        }

        public void Update(Viewport viewport)
        {
            KeyboardState ks = Keyboard.GetState();

            if(ks.IsKeyDown(Keys.Left))
            {
                position.X -= 5;
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                position.X += 5;
            }

           if(position.X > viewport.Width)
            {
                position.X = -image.Width; 
            }

            if (position.X + image.Width + 5 <= 0)
            {
                position.X = viewport.Width + 2;
            }
        }
    }
}
