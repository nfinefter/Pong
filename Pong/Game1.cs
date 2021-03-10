using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            playAgain.Tint = Color.White;
            yesButton.Tint = Color.White;
            noButton.Tint = Color.White;
            leftPaddle.Tint = Color.Transparent;
            rightPaddle.Tint = Color.Transparent;
            circle.Tint = Color.Transparent;
            circle.xSpeed = new Vector2(0, 0);
            circle.ySpeed = new Vector2(0, 0);
        }
        protected void Reset()
        {
            Vector2 leftPaddleYSpeed = new Vector2(0, 10);
            Vector2 leftPaddleXSpeed = new Vector2(0, 0);
            Vector2 rightPaddleYSpeed = new Vector2(0, 20);
            Vector2 rightPaddleXSpeed = new Vector2(0, 0);
            Vector2 circleYSpeed = new Vector2(0, 10);
            Vector2 circleXSpeed = new Vector2(10, 0);

            scoreNum = 0;
            
            Texture2D backImage = Content.Load<Texture2D>("buttonbackground");
            SpriteFont font = Content.Load<SpriteFont>("GameFont");

            Vector2 playAgainPos = (new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));

            leftPaddle = new Sprite(new Vector2(10, 250), pixel, new Point(30, 100), Color.White, leftPaddleXSpeed, leftPaddleYSpeed);
            rightPaddle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width - 40, 250), pixel, new Point(30, 100), Color.White, rightPaddleXSpeed, rightPaddleYSpeed);
            circle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), pixel, new Point(50, 50), Color.White, circleXSpeed, circleYSpeed);
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
            
            circle.Position += circle.xSpeed;
            circle.Position += circle.ySpeed;

            rightPaddle.Position += rightPaddle.ySpeed;
            if (rightPaddle.Position.Y + 100 <= GraphicsDevice.Viewport.Bounds.Height)
            {
                rightPaddle.ySpeed *= -1;
            }
            if (rightPaddle.Position.Y >= 0)
            {
                rightPaddle.ySpeed *= -1;
            }

            if (circle.Position.X <= 0)
            {
                circle.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);

                score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), $"YOU LOSE!", Color.Black);
                GameOver();
                if (yesButton.CheckMouse(IsPlayAgainSelected))
                {
                    Reset();
                }
            }
            else if (circle.Position.X + 50 >= GraphicsDevice.Viewport.Bounds.Width)
            {
                circle.Position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
                
                circle.xSpeed *= -1;
                circle.ySpeed *= -1;

                score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), $"Score: {scoreNum}", Color.Black);

                scoreNum++;
                if (scoreNum >= 6)
                {
                    score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), $"YOU WIN!", Color.Black);

                    GameOver();
                    if (yesButton.CheckMouse(IsPlayAgainSelected))
                    {
                        Reset();
                    }

                }
            
            }

            if (circle.Position.Y <= 0)
            {
                circle.ySpeed *= -1;
            }
            else if (circle.Position.Y + 50 >= GraphicsDevice.Viewport.Bounds.Height)
            {
                circle.ySpeed *= -1;
            }

            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Down) && leftPaddle.Position.Y + 100 <= GraphicsDevice.Viewport.Bounds.Height)
            {
                leftPaddle.Position += leftPaddle.ySpeed;
            }
            else if (keyboard.IsKeyDown(Keys.Up) && leftPaddle.Position.Y >= 0)
            {
                leftPaddle.Position -= leftPaddle.ySpeed;
            }
            

            // Intersect with paddle check
            if(leftPaddle.HitBox.Intersects(circle.HitBox))
            {
                circle.xSpeed *= -1;
                circle.ySpeed *= -1;
            }
            else if (rightPaddle.HitBox.Intersects(circle.HitBox))
            {
                circle.xSpeed *= -1;
                circle.ySpeed *= -1;
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
