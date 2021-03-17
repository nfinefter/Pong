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
        public SpriteFont Font { get; set; }
        public string ButtonText { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Size => Font.MeasureString(ButtonText);
        public Color Tint { get; set; }
        public Rectangle Hitbox => new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        public Button(Texture2D text, string buttonText, SpriteFont font, Vector2 position, Color tint)
        {
            Texture = text;
            Font = font;
            ButtonText = buttonText;
            Position = position;
            Tint = tint;
        }


        public bool IsClicked()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed && Hitbox.Contains(mouseState.Position))
            {
                Tint = Color.Gray * 0.9f;
                return true;
            }
            return false;

        }

        public void Hover()
        {

                MouseState mouseState = Mouse.GetState();
                if (Hitbox.Contains(mouseState.Position))
                {
                    Tint = Color.Black * 0.6f;
                }
                else
                {
                    Tint = Color.White;
                }
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Tint);
            spriteBatch.DrawString(Font, ButtonText, Position, Tint);
        }
    }
}
