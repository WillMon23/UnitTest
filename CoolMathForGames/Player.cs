using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;

namespace CoolMathForGames
{
    class Player : Actor
    {
        private float _speed;
        private Vector2 _volocity;
        
        public float Speed { get { return _speed; } set { _speed = value; } }

        public Vector2 Volocity {  get { return _volocity; } set { _volocity = value; } }

        public Player(char icon, float x, float y, float speed, string name = "Actor", ConsoleColor color = ConsoleColor.DarkRed) 
            :base( icon,  x,  y,  name = "Actor",  color = ConsoleColor.DarkRed)
        {
            _speed = speed;
            
        }

        public override void Update()
        {
            Vector2 moveDirecton = new Vector2();

            ConsoleKey keyPressed = Engine.GetNextKey();

            if (keyPressed == ConsoleKey.A)
                moveDirecton = new Vector2 { X = -1 };

            if (keyPressed == ConsoleKey.D)
                moveDirecton = new Vector2 { X = 1 };

            if (keyPressed == ConsoleKey.W)
                moveDirecton = new Vector2 { Y = -1 };

            if (keyPressed == ConsoleKey.S)
                moveDirecton = new Vector2 { Y = 1 };

            Volocity =  moveDirecton * Speed;

            Posistion = new Vector2 { X = Posistion.X + Volocity.X, Y = Posistion.Y + Volocity.Y };

        }


    }
}
