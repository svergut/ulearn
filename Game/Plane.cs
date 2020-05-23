using System;
using System.Drawing;

namespace Game
{
    public class Plane : GameObject
    {
        public Plane(int width, int height, int x, int y) : base(width, height, x, y) { }
        public Plane(int width, int height, int x, int y, int speed) : base(width, height, x, y, speed) { }

        public override event Action LocationChanged;

        public void MoveForward()
        {
            Location = new Point(Location.X + Speed, Location.Y);
        }

        public override void SetLocation(int x, int y)
        {
            if (x < Sizes.Collection["game"].Width - 200 &&
                x > -5 &&
                y > -5 &&
                y < Sizes.Collection["game"].Height)
            {
                Location = new Point(x, y);
                X = x; Y = y;
                LocationChanged?.Invoke();
            }
        }
    }
}
