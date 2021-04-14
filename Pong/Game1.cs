using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Game1 : Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Ball ball;
        private Paddle leftPaddle;
        private Paddle rightPaddle;

        private TextSprite score;
        private TextSprite playAgain;

        private Button yesButton;
        private Button noButton;
        private bool IsPlayAgainSelected;
        private bool IsExitSelected;

        private bool gameEnded;

        private int randNum = 0;
        private Random rand ;

        // TODO: Why do these exist??? Aren't they part of their classes???

        int scoreNum;

        Texture2D pixel;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        protected void EndingGameOptions()
        {
            ball.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            if (scoreNum < 0)
            {
                score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), $"YOU LOSE!", Color.Black);
            }
            if (scoreNum >= 6)
            {
                score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), $"YOU WIN!", Color.Black);
            }
            
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
        protected void GameOver()
        {
            
            gameEnded = true;
            playAgain.Tint = Color.White;
            yesButton.Tint = Color.White;
            noButton.Tint = Color.White;
            leftPaddle.Tint = Color.Transparent;
            rightPaddle.Tint = Color.Transparent;
            ball.Tint = Color.Transparent;
            ball.Speed = new Vector2(0, 0);
        }
        protected void Reset()
        {
            IsPlayAgainSelected = false;
            IsExitSelected = false;

            Vector2 leftPaddleSpeed = new Vector2(0, 20);
            Vector2 rightPaddleSpeed = new Vector2(0, 20);
            Vector2 circleSpeed = new Vector2(12, 12);

            scoreNum = 0;
            gameEnded = false;
            
            Texture2D backImage = Content.Load<Texture2D>("buttonbackground");
            SpriteFont font = Content.Load<SpriteFont>("GameFont");

            Vector2 playAgainPos = (new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));

            leftPaddle = new Paddle(new Vector2(10, 250), pixel, new Point(30, 100), Color.White, leftPaddleSpeed);
            rightPaddle = new Paddle(new Vector2(GraphicsDevice.Viewport.Bounds.Width - 40, 250), pixel, new Point(30, 100), Color.White, rightPaddleSpeed);
            ball = new Ball(new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), pixel, new Point(50, 50), Color.White, circleSpeed);
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
            rand = new Random();

            pixel = Content.Load<Texture2D>("pixel");

            var paddleTexture = Content.Load <Texture2D>("rectangle");
            var circleTexture = Content.Load<Texture2D>("circle");

            Reset();
        }

        protected override void Update(GameTime gameTime)
        {

            IsPlayAgainSelected = yesButton.IsClicked();
            IsExitSelected = noButton.IsClicked();

            ball.Position += ball.Speed;

            //if you are within the screen area and the speed of the ball is positive, have your paddle move down matching the speed of the ball
            //however, if the ball speed is neagative, have your paddle move up matching the ball speed

            leftPaddle.Update(GraphicsDevice.Viewport.Height);
            rightPaddle.Update(GraphicsDevice.Viewport.Height, randNum, ball);

            if (ball.Position.X < 0)
            {
                scoreNum--;
            }

            if (scoreNum < 0 || scoreNum >= 6)
            {
                EndingGameOptions();
            }

            else if (ball.Position.X + ball.Size.X >= GraphicsDevice.Viewport.Bounds.Width)
            {
                ball.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                
                ball.Speed *= -1;

                scoreNum++;

                score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), $"Score: {scoreNum}", Color.Black);            
            }

            if (ball.Position.Y <= 0)
            {
                ball.Speed = new Vector2(ball.Speed.X, Math.Abs(ball.Speed.Y));
            }
            else if (ball.Position.Y + 50 >= GraphicsDevice.Viewport.Bounds.Height)
            {
                ball.Speed = new Vector2(ball.Speed.X, -Math.Abs(ball.Speed.Y));
            }

            // Intersect with paddle check
            if(leftPaddle.HitBox.Intersects(ball.HitBox))
            {
                ball.Speed = new Vector2(Math.Abs(ball.Speed.X), Math.Abs(ball.Speed.Y));
                randNum = rand.Next(0, 2);
            }
            else if (rightPaddle.HitBox.Intersects(ball.HitBox))
            {
                ball.Speed = new Vector2(-Math.Abs(ball.Speed.X), -Math.Abs(ball.Speed.Y));
                
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            ball.Draw(spriteBatch);
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
