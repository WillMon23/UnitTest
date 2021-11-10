using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace CoolMathForGames
{
    class AABBCollider : Collider
    {
        private float _width;
        private float _height;

        public float Width { get { return _width; } set { _width = value; } }

        public float Height { get { return _height; } set { _height = value; } }

        /// <summary>
        /// Furthest opposing left 
        /// </summary>
        public float Left { get { return Owner.LocalPosition.X - (Height / 2) ; } }

        public float Right { get { return Owner.LocalPosition.X + (Height / 2); } }

        public float Top { get { return Owner.LocalPosition. Y - (Width / 2); } }

        public float Bottom { get { return Owner.LocalPosition.Y + (Width / 2); } }


        public AABBCollider(float width, float height, Actor owner) : base(owner, ColliderType.AABB)
        {
            _width = width;
            _height = height;
        }

        public override bool CheckCollisionAABB(AABBCollider other)
        {
            //Checks collision with it self
            if (other.Owner == Owner)
                return false;
            // Returns true if there is a overlap
            if(other.Left <= Right &&
                other.Top <= Bottom &&
                Left <= other.Right &&
                Top <= other.Bottom)
            {
                return true;
            }

            // returns false if there is no overlap
            return false;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            return other.CheckCollisionAABB(this);
        }

        public override void Draw()
        {
            Raylib.DrawRectangleLines((int)Left, (int)Top, (int)Width, (int)Height, Color.GREEN);
        }

    }


}
