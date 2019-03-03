using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceInvaders
{
    internal class Ship : Sprite
    {
        private Vector2 speed;

        public Ship(Texture2D tex, Vector2 position, Vector2 speed, Color tint) : base(position, tex, tint)
        {
            this.speed = speed;
        }

        public void Update(Viewport viewport)
        {
            KeyboardState ks = Keyboard.GetState();

            if (ks.IsKeyDown(Keys.Left))
            {
                position.X -= speed.X;
            }

            if (ks.IsKeyDown(Keys.Right))
            {
                position.X += speed.X;
            }

            if (position.X > viewport.Width)
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
