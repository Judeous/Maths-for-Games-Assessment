﻿using MathLibrary;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    public class Actor
    {
        protected Color _rayColor;
        protected Sprite _sprite;

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
            set { _globalTransform.m11 = value.X; _globalTransform.m21 = value.Y; }
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
            : this((char)x, y)
        {
            sprite = _sprite;
        } //Overload Constructor with Sprite

        public virtual void Start()
        {
            Started = true;
        } //Start

        public virtual void Update(float deltaTime)
        {
            if (_health <= 0)
            {
                
            }
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
            if (_sprite != null)
                _sprite.Draw(_globalTransform);
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
            Vector2 direction = actor.GlobalPosition - GlobalPosition;
            actor.SetTranslate(actor.LocalPosition + direction.Normalized);
        } //On Collision function

        public void SetTranslate(Vector2 position)
        {
            _translation = Matrix3.CreateTranslation(position);
        } //Set Translate function

        public void SetRotation(float radians)
        {
            _rotation = Matrix3.CreateRotation(radians);
        } //Set Rotation function

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
                SetRotation(-(float)Math.Atan2(Velocity.Y, Velocity.X));
        } //Update Facing function

        public void DealDamage(Actor actor)
        {
            actor.TakeDamage(_damage);
        } //Deal Damage function

        public void TakeDamage(float damage)
        {
            _health -= damage;
        } //Take Damage function
    } //Actor
} //Maths For Games Assessment