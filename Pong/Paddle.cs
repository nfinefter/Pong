using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;
using System.Text;

namespace Pong
{
    class Paddle : Sprite
    {

        public Paddle (Vector2 position, Texture2D texture, Point size, Color tint, Vector2 speed) 
            : base(position, texture, size, tint, speed)
        {

        }
        public void Update(int ScreenHeight)
        {
            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Down) && Position.Y + 100 <= ScreenHeight)
            {
                Position += new Vector2(0, Speed.Y);
            }
            else if (keyboard.IsKeyDown(Keys.Up) && Position.Y >= 0)
            {
                Position -= new Vector2(0, Speed.Y);
            }

            
        }

        public void Update(int ScreenHeight, int randNum, Ball ball)
        {
            if (Position.Y + Size.Y <= ScreenHeight && Speed.Y > 0 && randNum == 0)
            {
                Position += new Vector2(0, Math.Abs(ball.Speed.Y));
            }
            else if (Speed.Y < 0 && Position.Y >= 0 && randNum == 0)
            {
                Position += new Vector2(0, -Math.Abs(ball.Speed.Y));
            }
        } 

    }
}
