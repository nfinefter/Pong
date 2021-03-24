﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Sprite circle;
        private Sprite leftPaddle;
        private Sprite rightPaddle;

        private TextSprite score;
        private TextSprite playAgain;

        private Button yesButton;
        private Button noButton;
        private bool IsPlayAgainSelected;
        private bool IsExitSelected;

        private bool gameEnded;

        // TODO: Why do these exist??? Aren't they part of their classes???

        int scoreNum;

        Texture2D pixel;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected void GameOver()
        {
            
            gameEnded = true;
            playAgain.Tint = Color.White;
            yesButton.Tint = Color.White;
            noButton.Tint = Color.White;
            leftPaddle.Tint = Color.Transparent;
            rightPaddle.Tint = Color.Transparent;
            circle.Tint = Color.Transparent;
            circle.Speed = new Vector2(0, 0);
        }
        protected void Reset()
        {
            IsPlayAgainSelected = false;
            IsExitSelected = false;

            Vector2 leftPaddleSpeed = new Vector2(10, 10);
            Vector2 rightPaddleSpeed = new Vector2(20, 20);
            Vector2 circleSpeed = new Vector2(10, 10);

            scoreNum = 0;
            gameEnded = false;
            
            Texture2D backImage = Content.Load<Texture2D>("buttonbackground");
            SpriteFont font = Content.Load<SpriteFont>("GameFont");

            Vector2 playAgainPos = (new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));

            leftPaddle = new Sprite(new Vector2(10, 250), pixel, new Point(30, 100), Color.White, leftPaddleSpeed);
            rightPaddle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width - 40, 250), pixel, new Point(30, 100), Color.White, rightPaddleSpeed);
            circle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), pixel, new Point(50, 50), Color.White, circleSpeed);
            score = new TextSprite(Vector2.Zero, font, $"Score: {scoreNum}", Color.Black);
            playAgain = new TextSprite(playAgainPos, font, "PLAY AGAIN?", Color.Transparent);

            Vector2 yesbuttonPos = new Vector2(playAgainPos.X, playAgainPos.Y + 60);
            Vector2 nobuttonPos = new Vector2(playAgainPos.X + 60, playAgainPos.Y + 60);

            yesButton = new Button(backImage, "Yes", font, yesbuttonPos, Color.Transparent );
            noButton = new Button(backImage, "No", font, nobuttonPos, Color.Transparent);

        }

        protected override void Initialize()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            IsMouseVisible = true;
             

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = Content.Load<Texture2D>("pixel");

            var paddleTexture = Content.Load <Texture2D>("rectangle");
            var circleTexture = Content.Load<Texture2D>("circle");

            Reset();
        }

        protected override void Update(GameTime gameTime)
        {


            IsPlayAgainSelected = yesButton.IsClicked();
            IsExitSelected = noButton.IsClicked();

            circle.Position += circle.Speed;

            rightPaddle.Position += rightPaddle.Speed;

            if (rightPaddle.Position.Y + 100 <= GraphicsDevice.Viewport.Bounds.Height)
            {
                rightPaddle.Position -= new Vector2(0, rightPaddle.Speed.Y);
            }
            if (rightPaddle.Position.Y >= 0)
            {
                rightPaddle.Position += new Vector2(0, rightPaddle.Speed.Y);
            }
            if (circle.Position.X < 0)
            {
                scoreNum --;
            }
            if (scoreNum < 0)
            {
                circle.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

                score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), $"YOU LOSE!", Color.Black);
                GameOver();

                yesButton.Hover();
                noButton.Hover();

                IsPlayAgainSelected = yesButton.IsClicked();
                IsExitSelected = noButton.IsClicked();

                if (IsPlayAgainSelected)
                {
                    Reset();             
                }
                else if (IsExitSelected)
                {
                    Exit();
                }
            }
            else if (scoreNum >= 6)
            {
                score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), $"YOU WIN!", Color.Black);

                GameOver();

                yesButton.Hover();
                noButton.Hover();

                IsPlayAgainSelected = yesButton.IsClicked();
                IsExitSelected = noButton.IsClicked();

                if (IsPlayAgainSelected)
                {
                    Reset();
                }
                else if (IsExitSelected)
                {
                    Exit();
                }
            }
            else if (circle.Position.X + 50 >= GraphicsDevice.Viewport.Bounds.Width)
            {
                circle.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                
                circle.Speed *= -1;

                score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), $"Score: {scoreNum}", Color.Black);

                scoreNum++;
            
            }

            if (circle.Position.Y <= 0)
            {
                circle.Speed = new Vector2(circle.Speed.X, Math.Abs(circle.Speed.Y));
            }
            else if (circle.Position.Y + 50 >= GraphicsDevice.Viewport.Bounds.Height)
            {
                circle.Speed = new Vector2(circle.Speed.X, -Math.Abs(circle.Speed.Y));
            }

            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Down) && leftPaddle.Position.Y + 100 <= GraphicsDevice.Viewport.Bounds.Height)
            {
                leftPaddle.Position += new Vector2(0, leftPaddle.Speed.Y);
            }
            else if (keyboard.IsKeyDown(Keys.Up) && leftPaddle.Position.Y >= 0)
            {
                leftPaddle.Position -= new Vector2(0, leftPaddle.Speed.Y);
            }
            

            // Intersect with paddle check
            if(leftPaddle.HitBox.Intersects(circle.HitBox))
            {
                circle.Speed = new Vector2(Math.Abs(circle.Speed.X), Math.Abs(circle.Speed.Y));
            }
            else if (rightPaddle.HitBox.Intersects(circle.HitBox))
            {
                circle.Speed = new Vector2(-Math.Abs(circle.Speed.X), -Math.Abs(circle.Speed.Y));
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            circle.Draw(spriteBatch);
            leftPaddle.Draw(spriteBatch);
            rightPaddle.Draw(spriteBatch);
            score.Draw(spriteBatch);
            

            playAgain.Draw(spriteBatch);

            noButton.Draw(spriteBatch);

            yesButton.Draw(spriteBatch);
            

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
