using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Alien : Sprite
    {
        Vector2 speed;

        public Alien(Texture2D image, Vector2 position, Vector2 speed, Color tint) : base(position,image, tint)
        {
            this.speed = speed;
        }

        public void Update()
        {

        }
       
    }
}
