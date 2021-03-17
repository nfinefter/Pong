using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    class MyMouse
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }

        public Vector2 Size { get; set; }
        public Color Tint { get; set; }

        public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        public MyMouse (Texture2D text, Vector2 pos, Vector2 size, Color tint)
        {
            Texture = text;
            Position = pos;
            Size = size;
            Tint = tint;
        }
        

        public void UpdatePosition()
        {
            Position = Mouse.GetState().Position.ToVector2();
        }

        public void Draw (SpriteBatch spriteBatch)
        {


            spriteBatch.Draw(Texture, Position, Tint);
        }

        
    }
}
