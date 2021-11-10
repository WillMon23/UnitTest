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
        private Vector2 _volocity;
        
        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector2 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public Player( float x, float y, float speed, string name, string path = "") 
            :base(   x,  y,  name , path)
        {
            _speed = speed;
            
        }

        public override void Start()
        {
            base.Start();
            Volocity = new Vector2 { X = 2, Y = 3 };
        }

        public override void Update(float deltaTime)
        {
            

            int xDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_A)) 
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_D));

            int yDirection = -Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_W)) 
                + Convert.ToInt32(Raylib.IsKeyDown(KeyboardKey.KEY_S));

            Vector2 moveDirecton = new Vector2(xDirection, yDirection);

            Volocity =  moveDirecton * Speed * deltaTime;

            if (Volocity.Magnitude > 0)
                Forward = Volocity.Normalzed;

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
