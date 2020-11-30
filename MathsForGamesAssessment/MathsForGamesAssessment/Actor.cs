using MathLibrary;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    public class Actor
    {
        protected Color _rayColor;
        protected Sprite _currentSprite;
        protected Sprite[] _sprite = new Sprite[0];

        protected Actor _parent;
        protected Actor[] _children = new Actor[0];

        private Vector2 _velocity = new Vector2();
        private Vector2 acceleration = new Vector2();
        private float _maxSpeed = 20;

        protected Matrix3 _localTransform = new Matrix3();
        protected Matrix3 _globalTransform = new Matrix3();
        //Pieces of _transform matrix
        protected Matrix3 _translation = new Matrix3();
        protected Matrix3 _rotation = new Matrix3();
        protected Matrix3 _scale = new Matrix3();

        protected float _collRadius = 0.5f;

        protected float _health = 10;
        protected float _damage = 1;

        public bool Started { get; private set; } //Started property

        public Vector2 Forward
        {
            get { return new Vector2(_globalTransform.m11, _globalTransform.m21); }
            set { LookAt(value.Normalized + GlobalPosition); }
        } //Forward property

        public Vector2 LocalPosition
        {
            get { return new Vector2(_localTransform.m13, _localTransform.m23); }
            set { SetTranslate(value); }
        } //Position property

        public Vector2 GlobalPosition
        {
            get { return new Vector2(_globalTransform.m13, _globalTransform.m23); }
        } //Position property

        public Vector2 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        } //Velocity property

        public Vector2 Acceleration { get => acceleration; set => acceleration = value; }
        public float MaxSpeed { get => _maxSpeed; set => _maxSpeed = value; }

        public void AddChild(Actor child)
        {
            Actor[] tempArray = new Actor[_children.Length + 1];

            for (int i = 0; i < _children.Length; i++)
                tempArray[i] = _children[i];

            tempArray[_children.Length] = child;
            _children = tempArray;
            child._parent = this;
        } //Add Child function

        public bool RemoveChild(Actor child)
        {
            if (child == null)
                return false;

            Actor[] tempArray = new Actor[_children.Length];
            bool childRemoved = false;

            Actor[] newArray = new Actor[_children.Length - 1];

            int j = 0;

            for (int i = 0; i < _children.Length; i++)
            {
                if (child != _children[i])
                {
                    newArray[j] = _children[i];
                    j++;
                }
                else
                    childRemoved = true;
            } //for every child

            _children = tempArray;
            child._parent = null;
            return childRemoved;
        } //Remove Child by Child function

        public void AddSprite(Sprite sprite)
        {
            //Create a new array with a size one greater than our old array
            Sprite[] appendedArray = new Sprite[_sprite.Length + 1];
            //Copy the values from the old array to the new array
            for (int i = 0; i < _sprite.Length; i++)
            {
                appendedArray[i] = _sprite[i];
            }
            //Set the last value in the new array to be the sprite we want to add
            appendedArray[_sprite.Length] = sprite;
            //Set old array to hold the values of the new array
            _sprite = appendedArray;
        } //Add Sprite function

        public bool RemoveSprite(int index)
        {
            //Check to see if the index is outside the bounds of our array
            if (index < 0 || index >= _sprite.Length)
                return false;

            bool spriteRemoved = false;

            //Create a new array with a size one less than our old array 
            Sprite[] newArray = new Sprite[_sprite.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _sprite.Length; i++)
            {
                //If the current index is not the index that needs to be removed,
                //add the value into the old array and increment j
                if (i != index)
                {
                    newArray[j] = _sprite[i];
                    j++;
                }
            } //For every Sprite

            //Set the old array to be the tempArray
            _sprite = newArray;
            return spriteRemoved;
        } //Remove Sprite by index

        public bool RemoveSprite(Sprite sprite)
        {
            //Check to see if the actor was null
            if (sprite == null)
                return false;

            bool spriteRemoved = false;
            //Create a new array with a size one less than our old array
            Sprite[] newArray = new Sprite[_sprite.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _sprite.Length; i++)
            {
                if (sprite != _sprite[i])
                {
                    newArray[j] = _sprite[i];
                    j++;
                }
            }

            //Set the old array to the new array
            _sprite = newArray;
            //Return whether or not the removal was successful
            return spriteRemoved;
        } //Remove Sprite by Sprite

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        /// <param name="color">The color of the symbol that will appear when drawn</param>
        public Actor(float x, float y)
        {
            _rayColor = Color.WHITE;
            _localTransform = new Matrix3();
            LocalPosition = new Vector2(x, y);
            Velocity = new Vector2();
            Forward = new Vector2(1, 0);
        } //Actor Constructor

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        public Actor(float x, float y, Sprite sprite)
            : this(x, y)
        {
            sprite = _currentSprite;
        } //Overload Constructor with Sprite

        public virtual void Start()
        {
            Started = true;
        } //Start

        public virtual void Update(float deltaTime)
        {
            //if (_health <= 0)
            //{
            //    Scene scene = Game.GetScenes(Game.CurrentSceneIndex);
            //    scene.RemoveActor(this);
            //}

            UpdateFacing();

            UpdateLocalTransform();
            UpdateGlobalTransform();

            if (Velocity.Magnitude != 0)
            {
                Velocity.X -= Velocity.X / 5;
                Velocity.Y -= Velocity.Y / 5;
            }

            Velocity += Acceleration;
            if (Velocity.Magnitude > MaxSpeed)
                Velocity = Velocity.Normalized * MaxSpeed;

            //Increase position by the current velocity
            LocalPosition += Velocity * deltaTime;
        } //Update

        public virtual void Draw()
        {
            //If Actor has a Sprite, draw it
            if (_currentSprite != null)
                _currentSprite.Draw(_globalTransform);
        } //Draw

        public virtual void End()
        {
            Started = false;
        } //End

        /// <summary>
        /// Called when a collision is detected by the scene
        /// </summary>
        /// <param name="actor">Collided-with Actor</param>
        /// <returns></returns>
        public bool CheckCollision(Actor actor)
        {
            if (actor._collRadius + _collRadius > (actor.GlobalPosition - GlobalPosition).Magnitude && actor != this)
            { //If distance between this Actor and the passed in Actor is less than the two radii
                OnCollision(actor);
            }
            return false;
        } //Check Collision function

        public virtual void OnCollision(Actor actor)
        {
            if (!(actor is Tile))
            {
                Vector2 direction = actor.GlobalPosition - GlobalPosition;
                actor.SetTranslate(actor.LocalPosition + direction.Normalized);
            } //If actor isn't Tile
        } //On Collision function

        public void SetTranslate(Vector2 position)
        {
            _translation = Matrix3.CreateTranslation(position);
        } //Set Translate function

        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        } //Set Rotation function

        public void Rotate(float radians)
        {
            _rotation *= Matrix3.CreateRotation(radians);
        } //Rotate function

        public void SetScale(float x, float y)
        {
            _scale = Matrix3.CreateScale(x, y);
        } //Set Scale function

        public void UpdateLocalTransform()
        {
            _localTransform = _translation * _rotation * _scale;
        } //Update Transform function

        public void UpdateGlobalTransform()
        {
            if (_parent != null)
                _globalTransform = _parent._globalTransform * _localTransform;
            else
                _globalTransform = Game.GetCurrentScene().World * _localTransform;
        } //Update Global Transform

        virtual public void UpdateFacing()
        {
            if (Velocity.Magnitude != 0)
                Forward = Velocity;
        } //Update Facing function

        public void LookAt(Vector2 position)
        {
            Vector2 direction = (position - GlobalPosition).Normalized;

            float angle = Vector2.FindAngle(Forward, direction);

            Rotate(-angle);
        } //Look At function

        public void DealDamage(Actor actor)
        {
            actor.TakeDamage(_damage);
        } //Deal Damage function

        public void TakeDamage(float damage)
        {
            _health -= damage;
        } //Take Damage function

        public void CreateProjectile(Projectile projectile)
        {
            Scene scene = Game.GetScenes(Game.CurrentSceneIndex);
            scene.AddActor(projectile);
        } //Create Projectile function
    } //Actor
} //Maths For Games Assessment