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
        Vector2 circleXSpeed;
        Vector2 circleYSpeed;
        Vector2 paddle1Speed;
        Vector2 paddle2Speed;
        


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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pixel = Content.Load<Texture2D>("pixel");


            var paddleTexture = Content.Load <Texture2D>("rectangle");
            var circleTexture = Content.Load<Texture2D>("circle");
            leftPaddle = new Sprite(new Vector2(10, 250), pixel, new Point(30,100), Color.White);
            rightPaddle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width - 40, 250), pixel, new Point(30,100), Color.White);
            circle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), pixel, new Point (50, 50), Color.White);
            score = new TextSprite(Vector2.Zero, Content.Load<SpriteFont>("GameFont"), "Score: 0", Color.Black);
            paddle1Speed = new Vector2(0, 10);
            paddle2Speed = new Vector2(0, 10);
            circleXSpeed = new Vector2(8, 0);
            circleYSpeed = new Vector2(0, 8);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            circle.Position += circleXSpeed;
            circle.Position += circleYSpeed;

            rightPaddle.Position += paddle2Speed;
            if (rightPaddle.Position.Y + 100 <= GraphicsDevice.Viewport.Bounds.Height)
            {
                paddle2Speed *= -1;
            }
            if (rightPaddle.Position.Y >= 0)
            {
                paddle2Speed *= -1;
            }

            //change all bouncing to losing
            if (circle.Position.X <= 0)
            {
                circle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), pixel, new Point(50, 50), Color.White);
            }
            else if (circle.Position.X + 50 >= GraphicsDevice.Viewport.Bounds.Width)
            {

                circle = new Sprite(new Vector2(GraphicsDevice.Viewport.Bounds.Width / 2, GraphicsDevice.Viewport.Bounds.Height / 2), pixel, new Point (50, 50), Color.White);
            }

            if (circle.Position.Y <= 0)
            {
                circleYSpeed *= -1;
            }
            else if (circle.Position.Y + 50 >= GraphicsDevice.Viewport.Bounds.Height)
            {
                circleYSpeed *= -1;
            }

            var keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Down) && leftPaddle.Position.Y + 100 <= GraphicsDevice.Viewport.Bounds.Height)
            {
                leftPaddle.Position += paddle1Speed;
            }
            else if (keyboard.IsKeyDown(Keys.Up) && leftPaddle.Position.Y >= 0)
            {
                leftPaddle.Position -= paddle1Speed;
            }
            

            // Intersect with paddle check
            if(leftPaddle.HitBox.Intersects(circle.HitBox))
            {
                circleXSpeed *= -1;
                circleYSpeed *= -1;
            }
            else if (rightPaddle.HitBox.Intersects(circle.HitBox))
            {
                circleXSpeed *= -1;
                circleYSpeed *= -1;
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
