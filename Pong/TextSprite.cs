using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    internal class TextSprite
    {
        public Vector2 Position { get; set; }
        public Color Tint { get; set; }
        public SpriteFont Font { get; set; }
        public string Text { get; set; }

        public TextSprite(Vector2 position, SpriteFont font, string text, Color tint)
        {
            Position = position;
            Font = font;
            Text = text;
            Tint = tint;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, Text, Position, Tint);
        }
    }
}