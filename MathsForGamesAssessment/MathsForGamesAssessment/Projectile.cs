using MathLibrary;
using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathsForGamesAssessment
{
    public class Projectile : Actor
    {
        protected float _speed;
        protected float _duration = 0;
        protected float _timeSinceCreation = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position">Where the Projectile will start</param>
        /// <param name="direction">The direction the Projectile will move</param>
        /// <param name="damage">The damage the Projectile will deal to the Actor it collides with</param>
        /// <param name="speed">How fast the Projectile will move</param>
        /// <param name="duration">How long the Projectile will last before dissapearing</param>
        /// <param name="health">How many Actors the Projectile can hit before dissapearing</param>
        public Projectile(Vector2 position, Vector2 direction, float damage, Sprite sprite, float speed = 15, int duration = 5, float health = 1)
            : base(position.X, position.Y)
        {
            Forward = direction;
            _damage = damage;
            _currentSprite = sprite;
            _speed = speed;
            _duration = duration;
            _health = health;
        } //Constructor

        public override void Update(float deltaTime)
        {
            _timeSinceCreation += deltaTime;

            if (_timeSinceCreation >= _duration)
            {
                Scene scene = Game.GetScenes(Game.CurrentSceneIndex);
                scene.RemoveActor(this);
            } //If time since creation is greater than the duration

            Velocity = Forward * _speed;
            base.Update(deltaTime);
        } //Update

        public override void OnCollision(Actor actor)
        {
            if(actor is Projectile)
            {
                Vector2 direction = actor.GlobalPosition - GlobalPosition;
                actor.SetTranslate(actor.LocalPosition + direction.Normalized);
            }
            else if (!(actor is Tile))
            {
                Vector2 direction = actor.GlobalPosition - GlobalPosition;
                actor.SetTranslate(actor.LocalPosition + direction.Normalized);
                actor.TakeDamage(_damage);
                TakeDamage(1);
            }
        } //Collide override
    } //Projectile
} //Maths For Games Assessment
