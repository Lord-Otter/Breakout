using System;
using breakout;
using SFML.System;
using SFML.Window;
using SFML.Graphics;


class Program
{
    public const int ScreenW = 1200;
    public const int ScreenH = 900;

    static void Main()
    {
        RenderWindow window = new RenderWindow(new VideoMode(ScreenW, ScreenH), "Breakout");
        window.Closed += (s, e) => window.Close();

        Clock clock = new Clock();
        Ball ball = new Ball();
        Paddle paddle = new Paddle();

        GameManager gameManager = new GameManager();

        while (window.IsOpen)
        {
            window.DispatchEvents();
            float deltaTime = clock.Restart().AsSeconds();
            window.Clear(new Color(100, 100, 100));

            if (!gameManager.gameOver)
            {
                ball.Update(deltaTime);
                ball.Draw(window);

                paddle.update(deltaTime);
                paddle.Draw(window);
            }
            else
            {
                //Show game over screen, final score, etc.
            }

            window.Display();
        }
    }
}


