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
            paddle1Speed = new Vector2(0, 10);
            paddle2Speed = new Vector2(0, 10);
            circleXSpeed = Vector2.UnitX;
            circleYSpeed = Vector2.UnitY;
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
                circleXSpeed *= -1;
            }
            if (circle.Position.Y <= 0)
            {
                circleYSpeed *= -1;
            }
            if (circle.Position.X + 50 >= GraphicsDevice.Viewport.Bounds.Width)
            {
                circleXSpeed *= -1;
            }
            if (circle.Position.Y + 50 >= GraphicsDevice.Viewport.Bounds.Height)
            {
                circleYSpeed *= -1;
            }

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Down) && leftPaddle.Position.Y + 100 <= GraphicsDevice.Viewport.Bounds.Height)
            {
                leftPaddle.Position += paddle1Speed;
            }
            if (keyboard.IsKeyDown(Keys.Up) && leftPaddle.Position.Y >= 0)
            {
                leftPaddle.Position -= paddle1Speed;
            }
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            circle.Draw(spriteBatch);
            leftPaddle.Draw(spriteBatch);
            rightPaddle.Draw(spriteBatch);

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
