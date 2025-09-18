using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace breakout
{
    public class Tiles
    {
        public Sprite[] sprites;
        public Vector2f size;
        public List<(Vector2f position, int color)> tiles;
        public GameManager gameManager;

        public Tiles()
        {
            // init tiles
            sprites = new Sprite[3];

            // init blue
            Sprite blueSprite = new Sprite();
            blueSprite.Texture = new Texture("assets/tileBlue.png");

            // init defaults
            Vector2f textureSize = (Vector2f)blueSprite.Texture.Size;
            size = new Vector2f(textureSize.X / textureSize.Y * 24, 24); // Assume all tile textures are the same size

            // complete blue init
            blueSprite.Origin = 0.5f * textureSize;
            blueSprite.Scale = new Vector2f(size.X / textureSize.X, size.Y / textureSize.Y);
            sprites[0] = blueSprite;

            // init green
            Sprite greenSprite = new Sprite();
            greenSprite.Texture = new Texture("assets/tileGreen.png");
            greenSprite.Origin = 0.5f * textureSize;
            greenSprite.Scale = new Vector2f(size.X / textureSize.X, size.Y / textureSize.Y);
            sprites[1] = greenSprite;

            // init pink
            Sprite pinkSprite = new Sprite();
            pinkSprite.Texture = new Texture("assets/tilePink.png");
            pinkSprite.Origin = 0.5f * textureSize;
            pinkSprite.Scale = new Vector2f(size.X / textureSize.X, size.Y / textureSize.Y);
            sprites[2] = pinkSprite;

            // init tile grid
            tiles = new List<(Vector2f, int)>();
            ResetGrid();
        }

        public void ResetGrid()
        {
            tiles.Clear();
            int count = 0;
            for (int i = -2; i <= 2; i++)
            {
                for (int j = -2; j <= 2; j++)
                {
                    var pos = new Vector2f(
                    Program.ScreenW * 0.5f + i * 96.0f,
                    Program.ScreenH * 0.3f + j * 48.0f);
                    tiles.Add((pos, count++ % sprites.Length)); // => get count mod 3 then increment count
                }
            }
        }

        public void Update(float deltaTime, List<Ball> balls)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                (Vector2f position, _) = tiles[i];
                foreach (Ball ball in balls)
                {
                    if (Collision.CircleRectangle(ball.sprite.Position, Ball.Radius, position, size, out Vector2f hit))
                    {
                        ball.sprite.Position += hit;
                        ball.Reflect(hit.Normalized());

                        gameManager.currentScore += 100;
                        tiles.RemoveAt(i); // this will shift all element after the i:th element back 1 index.
                        i--; // move back 1 index to correct (could also do a reverse loop instead)
                    }
                }
            }
        }

        public void Draw(RenderTarget target)
        {
            foreach ((Vector2f position, int color) in tiles)
            {
                Sprite sprite = sprites[color];
                sprite.Position = position;
                target.Draw(sprite);
            }
        }
    }
}
