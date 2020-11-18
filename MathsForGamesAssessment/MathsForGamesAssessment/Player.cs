using MathLibrary;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    class Player : Actor
    {
        private float _speed = 5;

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        } //Speed Property

        public Player(float y, float x, float z)
            : base(x, y, z)
        { } //Constructor

        public override void Start()
        {
            GameManager.onWin += DrawWinText;
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis
            int xDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_LEFT_SHIFT))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_SPACE));
            int zDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));

            if (Game.GetKeyDown((int)KeyboardKey.KEY_M))
            {
                Scene scene = Game.GetScenes(Game.CurrentSceneIndex);
                Projectile projectile = new Projectile(GlobalPosition.X, GlobalPosition.Y, GlobalPosition.Z, Forward, 1, _damage);
                scene.AddActor(projectile);
            } //If scaling up

            if (Game.GetKeyDown((int)KeyboardKey.KEY_DOWN) && _scale.m11 - 1 > 0)
            {
                SetScale(_scale.m11 - 1, _scale.m22 - 1, _scale.m33 - 1);
                _collRadius -= 0.5f;
            } //If scaling down

            if (Game.GetKeyDown((int)KeyboardKey.KEY_RIGHT))
                Speed += 1;

            if (Game.GetKeyDown((int)KeyboardKey.KEY_LEFT) && Speed > 0)
                Speed -= 1;

            //Set the actors current velocity to be the a vector with the direction found scaled by the speed
            Velocity = new Vector3(xDirection, yDirection, zDirection);
            Velocity = Velocity.Normalized * Speed;

            if (Velocity.Magnitude <= 0)
                return;
            Forward = Velocity.Normalized;

            base.Update(deltaTime);
        } //Update override

        public void DrawWinText()
        {
            Raylib.DrawText("You Win!", 150, 150, 50, Color.GRAY);
        }
    } //Player
} //Maths For Games Assessment