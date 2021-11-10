using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;

namespace CoolMathForGames
{
    class UIText : Actor
    {

        private string _text;
        private int _width;
        private int _height;
        private Font Font;
        private int _fontSize;
        private Color FontColor;

        /// <summary>
        /// Text being utalized 
        /// </summary>
        public string Text { get { return _text; } set { _text = value; } }

        public int Width { get { return _width; } set { _width = value; } }

        public int Height { get { return _height; } set { _height = value; } }



        public UIText(float x, float y, string name, Color color, int width, int height, int fontSize, string text = "")
            : base( x, y, name)
        {
            _text = text;
            _width = width;
            _height = height;
            _fontSize = fontSize;
            Font = Raylib.LoadFont("resources/font/alagard.png");
            FontColor = Color.GOLD;


        }

        public override void Draw()
        {
            Rectangle textBox = new Rectangle(LocalPosition.X, LocalPosition.Y, Width, Height);
            Raylib.DrawTextRec(Font, Text, textBox, _fontSize, 1, true, FontColor);
        }


    

    }
}
