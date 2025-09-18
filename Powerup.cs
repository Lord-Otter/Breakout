using System;
using System.Collections.Generic;
using SFML.Graphics;
using SFML.System;

namespace breakout
{
    public class Powerup : Ball
    {
        public Powerup(Vector2f position) : base(position)
        {
            sprite.Color = Color.Red;
            speed = 150;
            direction = new Vector2f(0, 1);
        }
    }
}