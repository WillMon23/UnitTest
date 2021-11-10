using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace CoolMathForGames
{
    class Enemy : Actor
    {

        private float _speed;

        private Vector3 _volocity;

        private Actor _target;

        private float _lineOfSightRange = 200f;

        private bool _alive = true;

        int _tally;

        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector3 Volocity { get { return _volocity; } set { _volocity = value; } }

        public bool Alive { get { return _alive; } }

        /// <summary>
        /// Enemy Contructor 
        /// What defines to be a enemy 
        /// </summary>
        /// <param name="icon">What it looks like in the console</param>
        /// <param name="x">x cooridinet position</param>
        /// <param name="y">y cooridinet position</param>
        /// <param name="name"> classification</param>
        /// <param name="color">There Color</param>
        public Enemy(float x, float y, float speed,Actor target, string name, Shape shape = Shape.SPHERE) : base( x, y, name, shape)
        {
            _speed = speed;
            _target = target;
        }

        public override void Start()
        {
            base.Start();

            _tally = 0;


            Volocity = new Vector3 { X = 2, Y = 2, Z = 3};
        }

        public override void Update(float deltaTime)
        {
            Volocity = _target.LocalPosition - LocalPosition;

            if (Volocity.Magnitude > 0)
                Forward = Volocity.Normalized;

            LocalPosition += Volocity.Normalized * 10 * deltaTime;

            //Posistion += Volocity.Normalzed * Speed * deltaTime;
            if (GetTargetInSight())
            { 
                LocalPosition += Volocity.Normalized * Speed * deltaTime;
            }

            base.Update(deltaTime);

        }
        public override void OnCollision(Actor actor)
        {
            if (actor.Name == "PlayerBullet")
            {
                
            }
        }

        /// <summary>
        /// If the 
        /// </summary>
        /// <returns></returns>
        private bool GetTargetInSight()
        {
            Vector3 directionTarget = (_target.LocalPosition - LocalPosition).Normalized;

            float distance = Vector3.Distance(_target.LocalPosition, LocalPosition);

            float cosTarget = distance / LocalPosition.Magnitude;

            return /*cosTarget < _lineOfSightRange && */(distance < _lineOfSightRange) || Vector3.DotProduct(directionTarget, Forward) < 0;
        }

        ///// <summary>
        ///// Creats Bullets to be sepolyed by the enemy 
        ///// </summary>
        //private void AddBullet()
        //{
        //    Random rng = new Random();

        //    int chance = rng.Next(1, 5);

        //    Bullet shot = new Bullet('.', Posistion, Color.GREEN, (Speed * 2), new Vector2(-1, 0), _currentScene, "EnemyBullet");


        //    if (chance == 1)
        //        shot = new Bullet('.', Posistion, Color.RED, (Speed * 3), new Vector2(-1, 0), _currentScene, "EnemyBullet");

        //    else if (chance == 2)
        //        shot = new Bullet('.', Posistion, Color.BLUE, (Speed * 4), new Vector2(-1, 0), _currentScene, "EnemyBullet");

        //    else if (chance >= 3)
        //        shot = new Bullet('.', Posistion, Color.GREEN, (Speed * 5), new Vector2(-1, 0), _currentScene, "EnemyBullet");

        //    CircleCollider shotCircleCollider = new CircleCollider(10, shot);
        //    shot.Collider = shotCircleCollider;


        //    _currentScene.AddActor(shot);
        //}
        public override void Draw()
        {
            base.Draw();
            //Collider.Draw();
        }

        private void Fallow()
        {

        }

    }
}
