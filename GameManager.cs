using System;
using System.Data;
using System.Reflection.Metadata.Ecma335;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace breakout
{
    public class GameManager
    {
        public int health;
        public int currentScore;
        public bool gameOver;
        public Paddle player;
        public Text gui;

        public GameManager()
        {
            gui = new Text();
            Font font = new Font("assets/future.ttf");
            gui.Font = font;
        }

        public void LoseHealth()
        {
            health--;
            if (health <= 0)
                gameOver = true;
        }

        public void Draw(RenderTarget target)
        {
            gui.DisplayedString = $"Health: {health}";
            gui.Position = new Vector2f(12, 8);
            target.Draw(gui);

            gui.DisplayedString = $"Score: {currentScore}";
            gui.Position = new Vector2f(Program.ScreenW - gui.GetGlobalBounds().Width - 12, 8);
            target.Draw(gui);
        }
    }
}