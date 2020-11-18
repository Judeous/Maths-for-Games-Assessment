using MathLibrary;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    class Projectile : Actor
    {
        protected Vector3 _direction;
        protected float _speed;
        protected int _duration = 0;
        protected int _timeSinceCreation = 0;

        public Projectile(float x, float y, float z, Vector3 direction, float health, float damage = 5, float speed = 20, int duration = 20)
            : base(x, y, z)
        {
            _direction = direction;
            _health = health;
            _damage = damage;
            _rayColor = Color.SKYBLUE;
            _speed = speed;
            _duration = duration;
        } //Overload Constructor

        public override void Update(float deltaTime)
        {
            _timeSinceCreation++;

            if (_timeSinceCreation >= _duration)
            {
                Scene scene = Game.GetScenes(Game.CurrentSceneIndex);
                scene.RemoveActor(this);
            } //If time since creation is greater than the duration

            Velocity = _direction * _speed;
            base.Update(deltaTime);
        } //Update

        public override void OnCollision(Actor actor)
        {
            if (actor !is Projectile)
            {
                actor.TakeDamage(_damage);
                Scene scene = Game.GetScenes(Game.CurrentSceneIndex);
                scene.RemoveActor(this);
            }
        } //Collide override
    } //Projectile
} //Maths For Games Assessment
