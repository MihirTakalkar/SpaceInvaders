using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceInvaders
{
    internal class Alien : Sprite
    {
        private Vector2 speed;

        public Alien(Texture2D image, Vector2 position, Vector2 speed, Color tint) : base(position, image, tint)
        {
            this.speed = speed;
        }

        public void Update(Viewport viewport)
        {

            if (position.X <= viewport.Width)
            {
                position.X += speed.X;

            }

            if (position.X + image.Width >= viewport.Width)
            {
                speed.X = -Math.Abs(speed.X);
                position.Y += 100;
            }

            if (position.X < 0)
            {
                speed.X = Math.Abs(speed.X);
                position.Y += 100;
            }

        }

    }
}
