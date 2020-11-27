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
        private float _rotate;
        private Sprite _projectileSprite = new Sprite("Images/Screenshot 2020-11-23 124849.png");

        private char _facingType = 'c';

        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        } //Speed Property

        public Player(float y, float x)
            : base(x, y)
        { } //Constructor

        public override void Start()
        {
            //GameManager.onWin += DrawWinText;
            AddSprite(new Sprite("Images/PNG/Survivor 1/survivor1_gun.png"));
            AddSprite(new Sprite("Images/PNG/Survivor 1/survivor1_hold.png"));
            AddSprite(new Sprite("Images/PNG/Survivor 1/survivor1_machine.png"));
            AddSprite(new Sprite("Images/PNG/Survivor 1/survivor1_reload.png"));
            AddSprite(new Sprite("Images/PNG/Survivor 1/survivor1_silencer.png"));
            AddSprite(new Sprite("Images/PNG/Survivor 1/survivor1_stand.png"));
            base.Start();
        } //Start

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
                _currentSprite = _sprite[5];
            }
            else
                _inCoolDown = true;

            if (Game.GetKeyDown((int)KeyboardKey.KEY_SPACE) && !_inCoolDown)
            {
                _currentSprite = _sprite[1];

                Projectile projectile = new Projectile(GlobalPosition+Forward, Forward, _damage, _projectileSprite);
                CreateProjectile(projectile);
                _timeSinceFire = 0;
            } //If can and are firing a projectile

            Acceleration = new Vector2(xDirection, yDirection);

            base.Update(deltaTime);
        } //Update override

        public override void UpdateFacing()
        {
            switch (_facingType)
            {
                case 'c': //Case Cursor
                    LookAt(new Vector2(Raylib.GetMousePosition().X / 32, Raylib.GetMousePosition().Y / 32));
                    break;

                case 'a': //Case Arrows
                    if (Game.GetKeyDown((int)KeyboardKey.KEY_LEFT))
                        SetRotation(_rotate += .2f);

                    if (Game.GetKeyDown((int)KeyboardKey.KEY_RIGHT))
                        SetRotation(_rotate -= .2f);
                    break;

                case 'm': //Case Movement
                    base.UpdateFacing();
                    break;
            } //Facing Type switch
        } //Update Facing function

        public void DrawWinText()
        {
            Raylib.DrawText("You Win!", 150, 150, 50, Color.GRAY);
        }
    } //Player
} //Maths For Games Assessment