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
        public Vector2f direction = new Vector2f(1, 1) / MathF.Sqrt(2f);

        public Ball()
        {
            sprite = new Sprite();

            sprite.Texture = new Texture("assets/ball.png");
            sprite.Position = new Vector2f(599, 449);

            Vector2f ballTextureSize = (Vector2f)sprite.Texture.Size;
            sprite.Origin = 0.5f * ballTextureSize;
            sprite.Scale = new Vector2f(
                Diameter / ballTextureSize.X,
                Diameter / ballTextureSize.Y);
        }

        public void Update(float deltaTime)
        {
            Vector2f newPost = sprite.Position;
            newPost += direction * speed * deltaTime;
            sprite.Position = newPost;
        }

        public void Draw(RenderTarget target)
        {
            target.Draw(sprite);
        }
    }
}