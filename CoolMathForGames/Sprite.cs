using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace CoolMathForGames
{
    class Sprite
    {
        private Texture2D _texture;

        /// <summary>
        /// Width of the loaded texture
        /// </summary>
        public int Width
        {
            get { return _texture.width; }
            private set { _texture.width = value; }
        }

         public int Height
        {
            get { return _texture.height; }
            private set { _texture.height = value; }
        }


        /// <param name="path">The File of the image to use as the testure</param>
        public Sprite(string path)
        {
            _texture = Raylib.LoadTexture(path);
        }


        /// <summary>
        /// Draws the sprite using the rotation, translation and the scale of the given transform
        /// </summary>
        /// <param name="transfrom">The transform of the actor that owns the sprite</param>
        public void Draw(Matrix3 transfrom)
        {
            //Finds the scale of the sprite 
            Width  = (int)Math.Round(new Vector2(transfrom.M00, transfrom.M10).Magnitude);
            Height = (int)Math.Round(new Vector2(transfrom.M01, transfrom.M11).Magnitude);

            //Set the sprites center to the transform orgin
            System.Numerics.Vector2 postion = new System.Numerics.Vector2(transfrom.M02, transfrom.M12);
            System.Numerics.Vector2 forward = new System.Numerics.Vector2(transfrom.M00, transfrom.M10);
            System.Numerics.Vector2 up = new System.Numerics.Vector2(transfrom.M01, transfrom.M11);

            postion -= System.Numerics.Vector2.Normalize(forward) * Width / 2;
            postion -= System.Numerics.Vector2.Normalize(up) * Height / 2;


            float rotation = (float)Math.Atan2(transfrom.M10, transfrom.M00);

            Raylib.DrawTextureEx(_texture, postion, (float)(rotation * 180 / Math.PI), 1, Color.WHITE);



        }

    }
}
