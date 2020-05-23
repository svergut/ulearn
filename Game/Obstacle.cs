using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Obstacle : GameObject
    {
        public Obstacle(int width, int height, int x, int y) : base(width, height, x, y) { }
        public Obstacle(Size size, int x, int y) : base(size, x, y) { }
        public bool IntersectsWithPlayer(Plane plane)
        {
           if (Tools.Intersect(X, X + Width, plane.X, plane.X + plane.Width,
               Y, Y + Height,  plane.Y, plane.Y + plane.Height))
               return true;

           return false;
        }
    }
}
