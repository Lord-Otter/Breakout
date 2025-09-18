using System;
using breakout;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using System.Collections.Generic;


class Program
{
    public const int ScreenW = 1200;
    public const int ScreenH = 900;

    static void Main()
    {
        RenderWindow window = new RenderWindow(new VideoMode(ScreenW, ScreenH), "Breakout");
        window.Closed += (s, e) => window.Close();

        Clock clock = new Clock();

        // init game
        Paddle paddle = new Paddle();
        GameManager gameManager = new GameManager();
        gameManager.player = paddle;

        List<Ball> balls;
        List<Powerup> powerups = new List<Powerup>();

        Tiles tiles = new Tiles();
        tiles.gameManager = gameManager;

        ResetGame();

        void ResetGame()
        {
            gameManager.gameOver = false;
            if (gameManager.health <= 0)
            {
                gameManager.currentScore = 0;
                gameManager.health = 3;
            }
                
            balls = [createBall(paddle)];
            tiles.ResetGrid();
            powerups.Clear();
        }

        while (window.IsOpen)
        {
            window.DispatchEvents();
            float deltaTime = clock.Restart().AsSeconds();
            window.Clear(new Color(100, 100, 100));

            if (!gameManager.gameOver)
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.LShift))
                    deltaTime *= 4;

                foreach (Ball ball in balls)
                {
                    ball.Update(deltaTime);
                    ball.Draw(window);
                }

                foreach (Powerup powerup in powerups)
                {
                    powerup.Update(deltaTime);
                    powerup.Draw(window);
                }

                balls.RemoveAll(ball => ball.queuedForDeletion); // Remove all balls that have collided with the bottom of the screen

                paddle.update(deltaTime, balls, powerups);
                paddle.Draw(window);

                powerups.RemoveAll(powerup => powerup.queuedForDeletion);

                tiles.Update(deltaTime, balls, powerups);
                tiles.Draw(window);

                gameManager.Draw(window);

                // Check for game over
                if (balls.Count == 0)
                {
                    gameManager.LoseHealth();
                    balls.Add(createBall(paddle));
                }

                if (tiles.tiles.Count == 0)
                    gameManager.gameOver = true;
            }
            else
            {
                if (Keyboard.IsKeyPressed(Keyboard.Key.R))
                    ResetGame();

                gameManager.Draw(window);

                Text gui = gameManager.gui;
                gui.CharacterSize = 56;
                gui.DisplayedString = gameManager.health == 0 ? "YOU LOST!" : "YOU WON!";
                gui.Position = new Vector2f(
                    (ScreenW - gui.GetGlobalBounds().Width) / 2,
                    (ScreenH - gui.GetGlobalBounds().Height) / 2
                );
                window.Draw(gui);

                gui.CharacterSize = 24;
                gui.DisplayedString = gameManager.health == 0 ? "Press 'R' to play again" : "Press 'R' to continue";
                gui.Position = new Vector2f(
                    (ScreenW - gui.GetGlobalBounds().Width) / 2,
                    (ScreenH - gui.GetGlobalBounds().Height) / 2 + 100
                );
                window.Draw(gui);
            }

            window.Display();
        }
    }
    
    public static Ball createBall(Paddle paddle)
    {
        return new Ball(paddle.sprite.Position - new Vector2f(0, 50)) { extraBounce = false, Paddle = paddle };
    }
}


