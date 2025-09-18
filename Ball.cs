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

        public const float Diameter = 30f;
        public const float Radius = Diameter * 0.5f;

        public float speed = 150;
        public Vector2f direction = new Vector2f(1, -1) / MathF.Sqrt(2f);
        Vector2f startPosition = new Vector2f(599, 799);

        public Action OnFloorHit;

        GameManager gameManager = new GameManager();

        //Power Ups
        public bool extraBounce = true;

        public Ball()
        {
            sprite = new Sprite();

            sprite.Texture = new Texture("assets/ball.png");
            sprite.Position = startPosition;

            Vector2f ballTextureSize = (Vector2f)sprite.Texture.Size;
            sprite.Origin = 0.5f * ballTextureSize;
            sprite.Scale = new Vector2f(
                Diameter / ballTextureSize.X,
                Diameter / ballTextureSize.Y);
        }

        public void Update(float deltaTime)
        {
            Vector2f newPos = sprite.Position;

            newPos += direction * speed * deltaTime;

            //Start
            if (direction.X == 0 && direction.Y == 0)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
                {
                    direction.X = 1;
                    direction.Y = -1;
                    direction /= MathF.Sqrt(2);
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
                    LoseRound();
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

        public void LoseRound()
        {
            gameManager.LoseHealth();
            sprite.Position = startPosition; //Ändra så den sitter fast på paddeln
            direction.X = 0;
            direction.Y = 0;
            //Reset paddle position
        }
    }
}