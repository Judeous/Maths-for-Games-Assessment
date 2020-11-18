using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    delegate void gameEvent();

    static class GameManager
    {
        private static bool _gameOver = false;

        public static bool GameOver { get => _gameOver; set => _gameOver = value; }

        public static int enemyCount = 0;

        public static gameEvent onWin;

        public static void CheckWin()
        {
            if(enemyCount <= 0 && onWin != null)
            {
                onWin.Invoke();
            }
        } //Check for Win function
    } //Game Manager
} //Maths for Games Assessment
