using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout
{
    public class Paddle
    {
        public const float paddleW = 150;
        public const float paddleH = paddleW / 4;

        public Sprite sprite;
        Vector2f startPosition = new Vector2f(599, 849);
        public int speed = 450;
        public Vector2f size;

        public float powerupTimer = 0f;

        public Paddle()
        {
            sprite = new Sprite();
            sprite.Texture = new Texture("assets/paddle.png");
            sprite.Position = startPosition;

            Vector2f paddleTextureSize = (Vector2f)sprite.Texture.Size;
            sprite.Origin = 0.5f * paddleTextureSize;
            sprite.Scale = new Vector2f(paddleW / paddleTextureSize.X, paddleH / paddleTextureSize.Y);
            size = new Vector2f(paddleW, paddleH);
        }

        public void update(float deltaTime, List<Ball> balls, List<Powerup> powerups)
        {
            powerupTimer -= deltaTime;

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
            if (sprite.Position.X > Program.ScreenW - size.X / 2)
            {
                newPos.X = Program.ScreenW - size.X / 2;
            }

            if (sprite.Position.X < size.X / 2)
            {
                newPos.X = size.X / 2;
            }

            // Ball collisions
            foreach (Ball ball in balls)
            {
                if (Collision.CircleRectangle(ball.sprite.Position, Ball.Radius, sprite.Position, size, out Vector2f hit))
                {
                    ball.sprite.Position += hit;
                    hit = hit.Normalized();
                    if (hit.Y == 0)
                    {
                        ball.Reflect(hit);
                    }
                    else
                    {
                        ball.direction = (ball.sprite.Position - (sprite.Position + new Vector2f(0, 20))).Normalized(); // direction away from a bit below the center of the paddle    
                    }
                }
            }

            foreach (Powerup powerup in powerups)
            {
                if (Collision.CircleRectangle(powerup.sprite.Position, Ball.Radius, sprite.Position, size, out _))
                {
                    if (powerupTimer < 0) 
                        powerupTimer = 4f;
                    else
                        powerupTimer += 4f; //Förlänger powerup timern om man plockar upp flera.

                    powerup.queuedForDeletion = true;
                }
            }

            Vector2f paddleTextureSize = (Vector2f)sprite.Texture.Size;
            if (powerupTimer > 0)
            {
                sprite.Scale = new Vector2f(paddleW * 1.5f / paddleTextureSize.X, paddleH * 1.5f / paddleTextureSize.Y);
                size = new Vector2f(paddleW * 1.5f, paddleH * 1.5f);
            }
            else
            {
                sprite.Scale = new Vector2f(paddleW / paddleTextureSize.X, paddleH / paddleTextureSize.Y);
                size = new Vector2f(paddleW, paddleH);
            }



            sprite.Position = newPos;
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(sprite);
        }

    }
}