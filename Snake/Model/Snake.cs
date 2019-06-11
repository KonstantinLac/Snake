using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Snake.Model
{
    class SnakeBody : Objects
    {
        public SnakeBody dimension;
        public SnakeBody(int newX, int newY)
        {
            x = newX;
            y = newY;
        }
        public void PaintingSnake(Graphics graphics)
        {
            if (dimension != null)
            {
                dimension.PaintingSnake(graphics);
            }

            graphics.FillRectangle(new SolidBrush(Color.Green), x, y, width, width);
        }

        public void SetPosition(int newX, int newY)
        {
            if (dimension != null)
            {
                dimension.SetPosition(x,y);
            }

            x = newX;
            y = newY;
        }

        public void AddPointBody()
        {
            if (dimension == null)
            {
                dimension = new SnakeBody(x, y);
            }
            else
            {
                dimension.AddPointBody();
            }
        }

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

    }
}
