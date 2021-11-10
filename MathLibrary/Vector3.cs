using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector3
    {
        public float X;
        public float Y;
        public float Z;

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }


        /// <summary>
        /// Gets the length of the vector
        /// </summary>
        public float Magnitude { get { return (float)Math.Sqrt(X * X + Y * +Z * Z); } }

        public Vector3 Normalized
        {
            get
            {
                Vector3 value = this;
                return value.Normalize();
            }
        } 

        /// <summary>
        /// Change this vector to have the magnitude that is equal to one
        /// </summary>
        /// <returns> resualt of the normalization returns an
        /// empty vector if the magnitude is zero</returns>
        public Vector3 Normalize()
        {
            if (Magnitude == 0)
                return new Vector3();
            return this /= Magnitude;
        }

        /// <summary>
        /// Gets two vectors and multiplies there X, Y and Z
        /// in order to add them togetther
        /// </summary>
        /// <param name="lhs"> Left Hand Side</param>
        /// <param name="rhs">Right Hand Side</param>
        /// <returns></returns>
        public static float DotProduct(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        }

        /// <summary>
        /// Gets the diffrence of both vector 
        /// in oder to get a single scale 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns></returns>
        public static float Distance(Vector3 lhs, Vector3 rhs)
        {
            return (lhs - rhs).Magnitude;
        }

        /// <summary>
        /// Adds there indavisual values in order to 
        /// make a new vector
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>Added values</returns>
        public static Vector3 operator +(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z };
        }

        /// <summary>
        /// Subtract the values in order to make a new vactor 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>Subracted vector</returns>
        public static Vector3 operator -(Vector3 lhs, Vector3 rhs)
        {
            return new Vector3 { X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z };
        }

        /// <summary>
        /// Overrrides the  * operator in order to 
        /// creat a new vector with the values multiplied 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="scaler"></param>
        /// <returns></returns>
         public static Vector3 operator *(Vector3 lhs, float scaler)
        {
            return new Vector3 { X = lhs.X * scaler, Y = lhs.Y * scaler, Z = lhs.Z * scaler };
        }

        /// <summary>
        /// Overrides the / operator in order to 
        /// create a new vector with the values devided
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="scaler"></param>
        /// <returns></returns>
        public static Vector3 operator /(Vector3 lhs, float scaler)
        {
            return new Vector3 { X = lhs.X / scaler, Y = lhs.Y / scaler, Z = lhs.Z / scaler };
        }

        /// <summary>
        /// Overrides the == in order to get a bool wether the value in the 
        /// vectors are equal together 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>true</returns>
        public static bool operator ==(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X == rhs.X && lhs.Y == rhs.Y && lhs.Z == rhs.Z);
        }

        /// <summary>
        /// Overrides the != operator in order to get a 
        /// bool to see weather the vectors are diffrent
        /// 
        /// </summary>
        /// <param name="lhs"></param>
        /// <param name="rhs"></param>
        /// <returns>false</returns>
        public static bool operator !=(Vector3 lhs, Vector3 rhs)
        {
            return (lhs.X != rhs.X || lhs.Y != rhs.Y || lhs.Z != rhs.Z);
        }

        public static Vector3 operator -(Vector3 rhs)
        {
            return new Vector3 { X = -rhs.X, Y = -rhs.Y, Z = -rhs.Z };
        }

    }
}
