using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snake.Model
{
    class Objects
    {
        protected int x { get; set; }
        protected int y { get; set; }
        protected int width { get; set; }
        public Objects()
        {
            width = 15;
        }

        public bool Intersections(Objects obj)
        {
            int interX = Math.Abs(x - obj.x);
            int interY = Math.Abs(y - obj.y);
            if (interX >= 0 && interX < width && interY < width && interY >= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
