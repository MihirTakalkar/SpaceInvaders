using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    internal class Shield : Sprite
    {
        private SpriteFont font;
        private int health;

        public Shield(Vector2 position, Texture2D image, Color tint, SpriteFont font, int health)
            : base(position, image, tint)
        {
            this.font = font;
            this.health = health;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            spriteBatch.DrawString(font, $"{health}", new Vector2(position.X + 40, position.Y + 40), Color.Red);
        }
    }
}
