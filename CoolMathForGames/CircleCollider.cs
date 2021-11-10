using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace CoolMathForGames
{
    class CircleCollider : Collider
    {
        private float _collisionRadius;


        public float CollisionRadius { get { return _collisionRadius; } set { _collisionRadius = value; } }
        public CircleCollider(float colldionRadius, Actor owner) : base(owner, ColliderType.CIRCLE)
        {
            _collisionRadius = colldionRadius;
        }

        public override bool CheckCollisionCircle(CircleCollider other)
        {
            if (other.Owner == Owner)
                return false;

            float distance = Vector3.Distance(other.Owner.LocalPosition, Owner.LocalPosition);

            float combinedRadii = other.CollisionRadius + CollisionRadius;

            return distance <= combinedRadii;
        }

        public override bool CheckCollisionAABB(AABBCollider other)
        {

            if (other.Owner == Owner)
                return false;
            //Get the direction from this collider to the AABB
            Vector3 direction = Owner.LocalPosition - other.Owner.LocalPosition;

            //Clamp the direction vector to be within the bounds of the AABB       
            direction.X = Math.Clamp(direction.X, -other.Width / 2, other.Width / 2);
            direction.Y = Math.Clamp(direction.Z, -other.Height / 2, other.Height / 2);

            //Add the direction vector to the AABB center to get closet point to the circle
            Vector3 closetsPoint = other.Owner.LocalPosition + direction;

            //Find the distance from the circle's center to the closest point
            float distanceFromClosestPoint = Vector3.Distance(Owner.LocalPosition, closetsPoint);

            //Return whether or not distance is less than the circle's radius
            return distanceFromClosestPoint <= CollisionRadius;
        }

        public override void Draw()
        {
            Raylib.DrawCircleLines((int)Owner.LocalPosition.X,(int)Owner.LocalPosition.Y,CollisionRadius,Color.PINK);
        }




    }
}
