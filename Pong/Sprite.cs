using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    internal class Sprite
    {
        public Vector2 Position { get; set; }
        public Texture2D Texture { get; set; }
        public Color Tint { get; set; }
        public Vector2 xSpeed { get; set;}
        public Vector2 ySpeed {get; set; }

        public Point Size { get; set; }

        public Rectangle HitBox
        {
            get
            {
                return new Rectangle(Position.ToPoint(), Size);
            }
        }
            
        public Sprite(Vector2 position, Texture2D texture, Point size, Color tint, Vector2 xspeed, Vector2 yspeed)
        {
            Size = size;
            Position = position;
            Texture = texture;
            Tint = tint;
            xSpeed = xspeed;
            ySpeed = yspeed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, HitBox, Tint);
        }
    }
}