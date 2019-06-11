using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake.Model
{
    class Food : Objects
    {
        public Food()
        {
            x = GenerationPosition(753); // Длину родителя
            y = GenerationPosition(539); // Высоту родителя
        }

        public void ShowFood(Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(Color.Red),x,y,width,width);
        }

        public void Regeneration()
        {
            x = GenerationPosition(753); // Длину родителя
            y = GenerationPosition(539); // Высоту родителя
        }
        private int GenerationPosition(int number)
        {
            Random rnd = new Random();
            return rnd.Next(0, number);
        }
    }
}
