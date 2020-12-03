using MathLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    class Zombie : Actor
    {
        private float _rotate = 0;
        private float _timeSinceRotation = 0;

        public Zombie(float x, float y)
            : base(x, y)
        { } //Constructor

        public Zombie(float x, float y, Actor target)
            : base(x, y)
        {
            AddSprite(new Sprite("Images/PNG/Zombie 1/zoimbie1_stand.png"));
            AddSprite(new Sprite("Images/PNG/Zombie 1/zoimbie1_hold.png"));

            Target = target;

            _health = 5;

            _currentSprite = _sprite[0];
        } //Overload Constructor with Sprite

        public Actor Target { get; set; } //Target property

        public override void Start()
        {
            GameManager.enemyCount++;
            base.Start();
        } //Start

        public override void Update(float deltaTime)
        {
            _timeSinceRotation += deltaTime;

            if (CheckTargetInSight(0.5f, 10))
            { //If target is in sight
                Acceleration = Acceleration.Normalized + Forward.Normalized;
                _currentSprite = _sprite[1];
            }
            else
            { //If target isn't in sight
                GenerateMovement();
                _currentSprite = _sprite[0];
            }

            base.Update(deltaTime);
        } //Update

        public override void End()
        {
            GameManager.enemyCount--;
            base.End();
        } //End

        public override void OnCollision(Actor actor)
        {
            if (actor is Player)
                actor.TakeDamage(_damage);
            base.OnCollision(actor);
        } //On Collision override

        public bool CheckTargetInSight(float maxAngle, float maxRange)
        {
            if (Target == null)
                return false;

            //Finds the vector representing the distance between the actor and it's target
            Vector2 direction = Target.GlobalPosition - GlobalPosition;

            //Gets the magnitude of the distance vector
            float distance = direction.Magnitude;

            //Use the inverse cosine to find the angle of the dot product in radians
            float angle = (float)Math.Acos(Vector2.DotProduct(Forward, direction.Normalized));

            //If target is in front of Position and in range
            if (angle <= maxAngle && distance <= maxRange)
                return true;
            return false;
        } //Check Target In Sight function

        public void GenerateMovement()
        {
            Random r = new Random(); //Sets a variable for a randomizer
            int randomX = 0;
            int randomY = 0;

            int forwardsOrNot;
            forwardsOrNot = r.Next(1, 6);

            switch (forwardsOrNot)
            {
                case 1:
                    Velocity = Forward;
                    break;

                case 2:
                    randomX = r.Next(1, 5);
                    randomY = r.Next(1, 5);
                    break;

                case 3:
                    randomX = r.Next(1, 5);
                    randomY = r.Next(1, 5);
                    break;

                default:
                    if (_timeSinceRotation > 10)
                        GenerateRotation();
                    break;
            } //Forwards or not switch

            Convert(randomX, randomY);
        } //Generate Movement function

        /// <summary>
        /// Translates from the integer to a float that can be used by Velocity
        /// </summary>
        public void Convert(int randomX, int randomY)
        {
            switch (randomX)
            {
                case 1:
                    Acceleration.X = -1;
                    break;

                case 2:
                    Acceleration.X = 0;
                    break;

                case 3:
                    Acceleration.X = 0;
                    break;

                case 4:
                    Acceleration.X = 1;
                    break;

                case 5:
                    Acceleration.X = 1;
                    break;
            } //Random X switch

            switch (randomY)
            {
                case 1:
                    Acceleration.Y = -1;
                    break;

                case 2:
                    Acceleration.Y = 0;
                    break;

                case 3:
                    Acceleration.Y = 0;
                    break;

                case 4:
                    Acceleration.Y = 1;
                    break;

                case 5:
                    Acceleration.Y = 1;
                    break;
            } //Random Y switch
        } //Convert function

        public void GenerateRotation()
        {
            Random r = new Random(); //Sets a variable for a randomizer

            int direction;
            direction = r.Next(1, 3);

            switch (direction)
            {
                case 1: //Case CCW
                    SetRotation(_rotate += .02f);
                    break;

                case 2: //Case CW
                    SetRotation(_rotate -= .02f);
                    break;

                default:

                    break;
            } //Direction switch

            _timeSinceRotation = 0;
        } //Generate Rotation function
    } //Enemy
} //Actor