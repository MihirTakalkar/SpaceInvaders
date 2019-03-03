using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    internal class Bullet : Sprite
    {
        private Vector2 speed;

        public Bullet(Texture2D image, Vector2 position, Vector2 speed, Color tint) : base(position, image, tint)
        {
            this.speed = speed;
        }

        public void Update()
        {
            position.Y += speed.Y;
        }
    }
}
