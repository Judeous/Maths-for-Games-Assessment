using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    class Tile : Actor
    {
        public Tile(float x, float y)
            : base(x, y)
        { } //Constructor

        public Tile(float x, float y, Sprite sprite)
            : base(x, y)
        {
            _currentSprite = sprite;
        } //Overload Constructor with Sprite

        public override void OnCollision(Actor actor)
        { } //OnCollision override
    } //Tile
} //Maths For Games Assessment