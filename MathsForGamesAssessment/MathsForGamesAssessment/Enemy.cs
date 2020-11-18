using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    class Enemy : Actor
    {
        public Enemy(float y, float x, float z)
            : base(x, y, z)
        { } //Constructor

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