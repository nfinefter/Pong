using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Game1 : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Sprite circle;
        Sprite leftPaddle;
        Sprite rightPaddle;

        TextSprite score;

        // TODO: Why do these exist??? Aren't they part of their classes???
        


        Texture2D pixel;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            leftPaddle.ySpeed = new Vector2(0, 10);
            rightPaddle.ySpeed = new Vector2(0, 10);
            leftPaddle = new Sprite(new Vector2(10, 250), pixel, new Point(30,100), Color.White, leftPaddle.xSpeed, leftPaddle.ySpeed);
            rightPaddle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width - 40, 250), pixel, new Point(30,100), Color.White, leftPaddle.xSpeed, leftPaddle.ySpeed);
            circle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), pixel, new Point (50, 50), Color.White, circle.xSpeed, circle.ySpeed);
            score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), "Score: 0", Color.Black);
            // TODO: use this.Content to load your game content here
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

            //change all bouncing to losing
            if (circle.Position.X <= 0)
            {
                circle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), pixel, new Point(50, 50), Color.White, circle.xSpeed, circle.ySpeed);
            }
            else if (circle.Position.X + 50 >= GraphicsDevice.Viewport.Bounds.Width)
            {

                circle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), pixel, new Point (50, 50), Color.White, circle.xSpeed, circle.ySpeed);
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

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
