using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout
{
    public class Paddle
    {
        public Sprite sprite;
        public const float paddleW = 150;
        public const float paddleH = paddleW / 4;
        public int speed = 450;
        Vector2f startPosition = new Vector2f(599, 849);

        public Paddle()
        {
            sprite = new Sprite();
            sprite.Texture = new Texture("assets/paddle.png");
            sprite.Position = startPosition;

            Vector2f paddleTextureSize = (Vector2f)sprite.Texture.Size;
            sprite.Origin = 0.5f * paddleTextureSize;
            sprite.Scale = new Vector2f(paddleW / paddleTextureSize.X, paddleH / paddleTextureSize.Y);
        }

        public void update(float deltaTime)
        {
            Vector2f newPos = sprite.Position;
            //Player Control
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                newPos.X += speed * deltaTime;
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                newPos.X -= speed * deltaTime;
            }

            //Edge Collision
            if (sprite.Position.X > Program.ScreenW - paddleW / 2)
            {
                newPos.X = Program.ScreenW - paddleW / 2;

            }

            if (sprite.Position.X < paddleW / 2)
            {
                newPos.X = paddleW / 2;

            }

            sprite.Position = newPos;
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(sprite);
        }

    }
}