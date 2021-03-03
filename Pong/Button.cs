using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    // ask instructor to show you events
    public class Button
    {
        public Texture2D Texture { get; set; }
        public string ButtonText { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Color Tint { get; set; }
        public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        public Button(string buttonText, Texture2D tex, Vector2 position, Vector2 size, Color tint)
        {
            Texture = tex;
            ButtonText = buttonText;
            Position = position;
            Size = size;
            Tint = tint;
        }


        public void CheckMouse()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && Hitbox.Contains(mouseState.Position))
            {
                // button was pressed
            }

        }


        public void DrawButton(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Hitbox, Tint);
        }
    }
}
