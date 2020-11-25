using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    class Enemy : Actor
    {
        public Enemy(float x, float y)
            : base(x, y)
        { } //Constructor

        public Enemy(float x, float y, string name)
            : base(x, y)
        {

        }

        public override void Start()
        {
            GameManager.enemyCount++;
            base.Start();
        }

        public override void End()
        {
            GameManager.enemyCount--;
            base.End();
        }
    } //Enemy
} //Actor