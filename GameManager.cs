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
        public int health = 3;
        public int currentScore;

        public bool gameOver = false;

        public void LoseHealth()
        {
            if (health <= 0)
                gameOver = true;

                health--;
        }
    }
}