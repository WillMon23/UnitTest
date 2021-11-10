using System;
using System.Collections.Generic;
using System.Text;

namespace MathLibrary
{
    public struct Vector4
    {
        public float X;
        public float Y;
        public float Z;
        public float W;

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public float Magnitude { get { return (float)Math.Sqrt(X * X + Y * Y + Z * Z + W * W); } }

        public Vector4 Normalized
        {
            get
            {
                Vector4 value = this;
                return value.Normalize();
            }
        }


        public static float DotProduct(Vector4 lhs, Vector4 rhs)
        {
            return (lhs.X * rhs.X) + (lhs.Y * rhs.Y) + (lhs.Z * rhs.Z);
        }

        public Vector4 Normalize()
        {
            if (Magnitude == 0)
                return new Vector4();
            return this /= Magnitude;
        }


        public static Vector4 operator +(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { W = lhs.W + rhs.W, X = lhs.X + rhs.X, Y = lhs.Y + rhs.Y, Z = lhs.Z + rhs.Z };
        }

        public static Vector4 operator -(Vector4 lhs, Vector4 rhs)
        {
            return new Vector4 { W = lhs.W - rhs.W, X = lhs.X - rhs.X, Y = lhs.Y - rhs.Y, Z = lhs.Z - rhs.Z };
        }

        public static Vector4 operator *(Vector4 lhs, float scaler)
        {
            return new Vector4 { W = lhs.W * scaler, X = lhs.X * scaler, Y = lhs.Y * scaler, Z = lhs.Z * scaler };
        }

        public static Vector4 operator /(Vector4 lhs, float scaler)
        {
            return new Vector4 { W = lhs.W / scaler, X = lhs.X / scaler, Y = lhs.Y / scaler, Z = lhs.Z / scaler };
        }

    }
}
