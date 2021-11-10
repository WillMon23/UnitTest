using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace CoolMathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector3 _volocity;
        
        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector3 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public Player( float x, float y, float speed, string name, Shape shape = Shape.SPHERE) 
            :base(   x,  y,   name , shape)
        {
            _speed = speed;
            
        }

        public override void Start()
        {
            base.Start();
            Volocity = new Vector3 { X = 2, Y = 0, Z = 2 };
        }

        public override void Update(float deltaTime)
        {
            

            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) 
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));

            int zDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) 
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            Vector3 moveDirecton = new Vector3(xDirection, 0, zDirection);

            Volocity =  moveDirecton * Speed * deltaTime;

            //if (Volocity.Magnitude > 0)
            //    Forward = Volocity.Normalized;

            LocalPosition += Volocity;
            
            base.Update(deltaTime);
        }

        public override void OnCollision(Actor actor)
        {

        }

        public override void Draw()
        {
            base.Draw();
            Collider.Draw();
        }


    }
}
