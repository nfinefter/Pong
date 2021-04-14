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
            //Also works
            //Position += new Vector2(0, ball.Speed.Y);
            //Math.Max(0, Math.Min(ScreenHeight, Position.Y + Size.Y));



            if (Position.Y + Size.Y <= ScreenHeight && ball.Speed.Y > 0 && randNum == 0)
            {
                Position += new Vector2(0, Math.Abs(ball.Speed.Y));
            }
            else if (Position.Y >= 0 && ball.Speed.Y < 0 && randNum == 0)
            {
                Position += new Vector2(0, -Math.Abs(ball.Speed.Y));
            }

        }

    }
}
