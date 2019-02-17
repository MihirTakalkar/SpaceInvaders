using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders
{
    class Shield : Sprite
    {

        //Font
        SpriteFont font;
        int health;
        //Health

        public Shield(Vector2 position, Texture2D image, Color tint, SpriteFont font, int health) 
            : base(position, image, tint)
        {
            this.font = font;
            this.health = health;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); //draws the image of the shield

            //draw the text of the shield
            spriteBatch.DrawString(font, $"{health}", new Vector2(position.X + 80, position.Y + 40), Color.Red);
        }
    }
}
