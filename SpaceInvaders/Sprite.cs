using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    internal class Sprite
    {
        public Vector2 position;
        public Texture2D image;
        public Color tint;
        public SpriteEffects spriteEffects;

        public Rectangle Hitbox => new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);

        public Sprite(Vector2 position, Texture2D image, Color tint)
        {
            this.position = position;
            this.image = image;
            this.tint = tint;

            spriteEffects = SpriteEffects.None;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, position, null, tint, 0, Vector2.Zero, Vector2.One, spriteEffects, 0);
        }
    }
}
