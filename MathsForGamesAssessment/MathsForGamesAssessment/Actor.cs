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

        protected Actor _parent;
        protected Actor[] _children = new Actor[0];

        protected Vector3 _velocity;

        protected Matrix4 _localTransform = new Matrix4();
        protected Matrix4 _globalTransform = new Matrix4();
        //Pieces of _transform matrix
        protected Matrix4 _translation = new Matrix4();
        protected Matrix4 _rotation = new Matrix4();
        protected Matrix4 _scale = new Matrix4();

        protected float _collRadius = 0.5f;

        protected float _health = 10;
        protected float _damage = 1;

        public bool Started { get; private set; } //Started property

        public Vector3 Forward
        {
            get { return new Vector3(_globalTransform.m11, _globalTransform.m21, _globalTransform.m31); }
            set { _globalTransform.m11 = value.X; _globalTransform.m21 = value.Y; _globalTransform.m31 = value.Z; }
        } //Forward property

        public Vector3 LocalPosition
        {
            get { return new Vector3(_localTransform.m14, _localTransform.m24, _localTransform.m34); }
            set { SetTranslate(value); }
        } //Position property

        public Vector3 GlobalPosition
        {
            get { return new Vector3(_globalTransform.m14, _globalTransform.m24, _globalTransform.m34); }
        } //Position property

        public Vector3 Velocity
        {
            get { return _velocity; }
            set { _velocity = value; }
        } //Velocity property

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
        public Actor(float x, float y, float z)
        {
            _rayColor = Color.WHITE;
            _localTransform = new Matrix4();
            LocalPosition = new Vector3(x, y, z);
            Velocity = new Vector3();
            Forward = new Vector3(1, 0, 0);
        } //Actor Constructor

        /// <param name="x">Position on the x axis</param>
        /// <param name="y">Position on the y axis</param>
        /// <param name="rayColor">The color of the symbol that will appear when drawn to raylib</param>
        /// <param name="icon">The symbol that will appear when drawn</param>
        public Actor(float x, float y, float z, Color rayColor)
            : this((char)x, y, z)
        {
            _rayColor = rayColor;
        } //Overload Constructor with Raylib

        public virtual void Start()
        {
            Started = true;
        } //Start

        public virtual void Update(float deltaTime)
        {
            UpdateFacing();

            UpdateLocalTransform();
            UpdateGlobalTransform();

            //Increase position by the current velocity
            LocalPosition += Velocity * deltaTime;
        } //Update

        public virtual void Draw()
        {
            Raylib.DrawCylinder(new System.Numerics.Vector3(GlobalPosition.X, GlobalPosition.Y, GlobalPosition.Z), _collRadius, _collRadius, _collRadius * 2, 100, Color.DARKBLUE);
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
            Vector3 direction = actor.GlobalPosition - GlobalPosition;
            actor.SetTranslate(actor.LocalPosition + direction.Normalized);
        } //On Collision function

        public void SetTranslate(Vector3 position)
        {
            _translation = Matrix4.CreateTranslation(position);
        } //Set Translate function

        public void SetRotationX(float radians)
        {
            _rotation = Matrix4.CreateRotationX(radians);
        } //Set Rotation function

        public void SetRotationY(float radians)
        {
            _rotation = Matrix4.CreateRotationY(radians);
        } //Set Rotation function

        public void SetRotationZ(float radians)
        {
            _rotation = Matrix4.CreateRotationZ(radians);
        } //Set Rotation function

        public void SetScale(float x, float y, float z)
        {
            _scale = Matrix4.CreateScale(x, y, z);
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

        private void UpdateFacing()
        {
            //If the actor hasn't moved, then don't change the direction
            if (Velocity.Magnitude <= 0)
                return;
            Forward = Velocity.Normalized;
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