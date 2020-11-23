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
        private float _fireCoolDown = 0.5f;
        private float _timeSinceFire = 0;
        private bool _inCoolDown = false;

        private char _facingType = 'm';

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        } //Speed Property

        public Player(float y, float x)
            : base(x, y)
        {
            _sprite = new Sprite("Images/PNG/Survivor 1/survivor1_stand.png");
        } //Constructor

        public override void Start()
        {
            //GameManager.onWin += DrawWinText;
            base.Start();
        }

        public override void Update(float deltaTime)
        {
            //Gets the player's input to determine which direction the actor will move in on each axis
            int xDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_A))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_D));
            int yDirection = -Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_W))
                + Convert.ToInt32(Game.GetKeyDown((int)KeyboardKey.KEY_S));

            _timeSinceFire += deltaTime;
            if (_timeSinceFire >= _fireCoolDown)
            {
                _inCoolDown = false;
                _sprite = new Sprite("Images/PNG/Survivor 1/survivor1_stand.png");
            }
            else
                _inCoolDown = true;

            if (Game.GetKeyDown((int)KeyboardKey.KEY_DOWN) && _scale.m11 - 1 > 0)
            {
                SetScale(_scale.m11 - 1, _scale.m22 - 1);
                _collRadius -= 0.5f;
            } //If scaling down

            if (Game.GetKeyDown((int)KeyboardKey.KEY_SPACE) && !_inCoolDown)
            {
                _sprite = new Sprite("Images/PNG/Survivor 1/survivor1_hold.png");
                Sprite sprite = new Sprite("Images/Screenshot 2020-11-23 124849.png");
                Scene scene = Game.GetScenes(Game.CurrentSceneIndex);
                Projectile projectile = new Projectile(GlobalPosition+Forward, Forward, _damage, sprite);
                scene.AddActor(projectile);
                _timeSinceFire = 0;
            } //If can and are firing a projectile

            Acceleration = new Vector2(xDirection, yDirection);

            if (Acceleration.Magnitude > 0)
                Forward = Acceleration.Normalized;

            base.Update(deltaTime);
        } //Update override

        public override void UpdateFacing()
        {
            switch(_facingType)
            {
                case 'm': //Case Mouse
                    SetRotation((float)Math.Atan2((GlobalPosition.X - Raylib.GetMousePosition().X), (GlobalPosition.Y -  Raylib.GetMousePosition().Y)));

                    //SetRotation((float)Math.Atan2(Raylib.GetMousePosition().Y, Raylib.GetMousePosition().X));

                    //if (Raylib.GetMousePosition().X >= GlobalPosition.X && Raylib.GetMousePosition().Y >= GlobalPosition.Y)
                    //{
                    //    SetRotation((float)Math.Atan2(-Raylib.GetMousePosition().X, -Raylib.GetMousePosition().Y));

                    //}
                    //else if (Raylib.GetMousePosition().X < GlobalPosition.X && Raylib.GetMousePosition().Y >= GlobalPosition.Y)
                    //{
                    //    SetRotation((float)Math.Atan2(Raylib.GetMousePosition().X, Raylib.GetMousePosition().Y));

                    //}
                    //else if (Raylib.GetMousePosition().X >= GlobalPosition.X && Raylib.GetMousePosition().Y < GlobalPosition.Y)
                    //{
                    //    SetRotation((float)Math.Atan2(-Raylib.GetMousePosition().X, Raylib.GetMousePosition().Y));

                    //}
                    //else
                    //{
                    //    SetRotation((float)Math.Atan2(Raylib.GetMousePosition().X, -Raylib.GetMousePosition().Y));

                    //}
                    break;

                case 'a': //Case Arrows
                    base.UpdateFacing();
                    break;
            }
        }

        public void DrawWinText()
        {
            Raylib.DrawText("You Win!", 150, 150, 50, Color.GRAY);
        }
    } //Player
} //Maths For Games Assessment