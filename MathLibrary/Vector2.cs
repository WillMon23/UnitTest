using System;

namespace MathLibrary
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Get the length of the vectors 
        /// </summary>
        public float Magnitude { get { return (float)Math.Sqrt(X * X + Y * Y); } }

        public Vector2 Normalzed
        {
            get
            {
                Vector2 value = this;
                return value.Normalize();
            }
        }

        /// <summary>
        /// Change this vector to have a magnitude that is equal to one 
        /// </summary>
        /// <returns>The result of the normalization. Returns an empty vector if the magnitude is zero</returns>
        public Vector2 Normalize()
        {
            if (Magnitude == 0)
                return new Vector2();

            return this / Magnitude;
        }

        /// <summary>
        /// Gets two vectors and multyplies there X and Y's 
        /// in oder to add them together
        /// </summary>
        /// <param name="lhs">left ha</param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float DotProduct(Vector2 lhs, Vector2 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y);
        }

        /// <summary>
        /// Finds the distance from the first vector to the second
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float Distance(Vector2 lhs, Vector2 rhs)
        {
            return (lhs - rhs).Magnitude;
        }

        /// <summary>
        /// Overrides the plus oporator in order to add 
        /// two vectors together 
        /// </summary>
        /// <param name="lhs">whats being added on the left hand side</param>
        /// <param name="rhs">whats being added on the right hand side</param>
        /// <returns>the Addition of both vectors</returns>
        public static Vector2 operator +(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y };
        }

        /// <summary>
        /// Overrides the minus oporator in order to subtract 
        /// two vectors together 
        /// </summary>
        /// <param name="lhs">whats being subtract on the left hand side</param>
        /// <param name="rhs">whats being subtract on the right hand side</param>
        /// <returns>the Subtraction of both vectors</returns>
        public static Vector2 operator -(Vector2 lhs, Vector2 rhs)
        {
            return new Vector2 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y };
        }

        /// <summary>
        /// Overrides the multiply oporator in order to multiply 
        /// a vector by a value  
        /// </summary>
        /// <param name="lhs">whats being multiply on the left hand side</param>
        /// <param name="scalar">whats being multiply on the right hand side</param>
        /// <returns>the Multiplication of both vectors</returns>
        public static Vector2 operator *(Vector2 lhs, float scalar)
        {
            return new Vector2 { X = lhs.X * scalar, Y = lhs.Y * scalar };
        }

        /// <summary>
        /// Overrides the devide oporator in order to devide 
        ///  a vector by a value  
        /// </summary>
        /// <param name="lhs">whats being devide on the left hand side</param>
        /// <param name="rhs">whats being devide on the right hand side</param>
        /// <returns>the Devision of both vectors</returns>
        public static Vector2 operator /(Vector2 lhs, float scalar)
        {
            return new Vector2 { X = lhs.X / scalar, Y = lhs.Y / scalar };
        }

        /// <summary>
        /// Overrides the equals equals oporator in order to
        /// checks to see if the left hand side vector X and Y 
        /// matches the X and Y on the right hand side
        /// </summary>
        /// <param name="lhs">left hand side vector </param>
        /// <param name="rhs">right hand side vector</param>
        /// <returns>if there equal to each other</returns>
        public static bool operator ==(Vector2 lhs, Vector2 rhs)
        {
            return (lhs.X == rhs.X && lhs.Y == rhs.Y);
        }

        /// <summary>
        /// Overrides the not equals oporator in order to
        /// checks to see if the left hand side vector X and Y 
        /// does not match the X and Y on the right hand side
        /// </summary>
        /// <param name="lhs">left hand side vector </param>
        /// <param name="rhs">right hand side vector</param>
        /// <returns>if there not equal to each other</returns>
        public static bool operator !=(Vector2 lhs, Vector2 rhs)
        {
            return (lhs.X != rhs.X || lhs.Y != rhs.Y);


        }

        public static Vector2 operator -(Vector2 rhs)
        {
            return new Vector2 { X = -rhs.X, Y = -rhs.Y };
        }
    }
}

