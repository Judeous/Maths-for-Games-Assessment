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

        public Enemy(float x, float y, Sprite sprite)
            : base(x, y)
        { } //Overload Constructor with Sprite

        public override void Start()
        {
            GameManager.enemyCount++;
            base.Start();
        } //Start

        public override void End()
        {
            GameManager.enemyCount--;
            base.End();
        } //End
    } //Enemy
} //Actor