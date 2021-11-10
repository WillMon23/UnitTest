using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace CoolMathForGames
{
    class Camera : Actor
    {
        public Camera(float x, float y, float z, string name, Shape shape) : base(x, y, name = "Camera", shape = Shape.CUBE)
        {

        }
    }
}
