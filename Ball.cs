using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout
{
    public class Ball
    {
        public Sprite sprite;
        public Paddle Paddle;
        public bool queuedForDeletion = false;

        public const float Diameter = 30f;
        public const float Radius = Diameter * 0.5f;

        public float speed = 500;
        public Vector2f direction = new Vector2f(0, 0);

        //Power Ups
        public bool extraBounce = true;

        public Ball(Paddle paddle)
        {
            sprite = new Sprite();
            Paddle = paddle;

            sprite.Texture = new Texture("assets/ball.png");
            sprite.Position = paddle.sprite.Position - new Vector2f(0, 50);

            Vector2f ballTextureSize = (Vector2f)sprite.Texture.Size;
            sprite.Origin = 0.5f * ballTextureSize;
            sprite.Scale = new Vector2f(
                Diameter / ballTextureSize.X,
                Diameter / ballTextureSize.Y
            );
        }

        public void Update(float deltaTime)
        {
            Vector2f newPos = sprite.Position;

            newPos += direction * speed * deltaTime;

            //Start
            if (direction.X == 0 && direction.Y == 0)
            {
                sprite.Position = Paddle.sprite.Position - new Vector2f(0, 100);

                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    direction = RandomDirection();
                }
                else
                {
                    return;
                }
            }

            //Bouncing
            if (sprite.Position.X > Program.ScreenW - Radius)
            {
                newPos.X = Program.ScreenW - Radius;
                direction.X *= -1;
            }

            if (sprite.Position.X < Radius)
            {
                newPos.X = Radius;
                direction.X *= -1;
            }

            if (sprite.Position.Y > Program.ScreenH - Radius)
            {
                if (extraBounce)
                {
                    newPos.Y = Program.ScreenH - Radius;
                    direction.Y *= -1;
                    extraBounce = false;
                }
                else
                {
                    queuedForDeletion = true;
                    return;
                }
            }

            if (sprite.Position.Y < Radius)
            {
                newPos.Y = Radius;
                direction.Y *= -1;
            }

            sprite.Position = newPos;
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(sprite);
        }

        public void Reflect(Vector2f normal) {
            direction -= normal * (2 * (direction.X * normal.X + direction.Y * normal.Y));
        }

        public static Vector2f RandomDirection()
        {
            Random random = new Random();
            return new Vector2f(
                MathF.Cos(random.NextSingle() * MathF.PI / 2 - 3 * MathF.PI / 4),
                MathF.Sin(random.NextSingle() * MathF.PI / 2 - 3 * MathF.PI / 4)
            );
        }
    }
}