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

        while (window.IsOpen)
        {
            window.DispatchEvents();
            float deltaTime = clock.Restart().AsSeconds();
            ball.Update(deltaTime);
            window.Clear(new Color(100, 100, 100));
            ball.Draw(window);
            window.Display();
        }
    }
}


