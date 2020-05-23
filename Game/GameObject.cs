using System;
using System.Drawing;
using System.Windows.Forms;

namespace Game
{
    public class GameObject
    {
        public Point Location { get; protected set; }
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public int Speed { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }

        public virtual event Action LocationChanged;
        public virtual event Action SpeedChanged;
        public virtual event Action GotOutOfScreen;

        public GameObject(int width, int height, int x, int y)
        {
            Width = width;
            Height = height;
            SetLocation(x, y);
        }

        public GameObject(int width, int height, int x, int y, int speed)
        {
            Width = width;
            Height = height;
            Speed = speed;
            SetLocation(x, y);
        }

        public GameObject(Size size, int x, int y)
        {
            Width = size.Width;
            Height = size.Height;
            SetLocation(x, y);
        }

        public void Move(MoveDirection direction)
        {
            var newX = X;
            var newY = Y;

            if (direction == MoveDirection.Up)
                newY -= Speed * 2;
            if (direction == MoveDirection.Down)
                newY += Speed * 2;
            if (direction == MoveDirection.Right)
                newX += Speed;
            if (direction == MoveDirection.Left)
                newX -= Speed;

            SetLocation(newX, newY);
        }

        public virtual void SetLocation(int x, int y)
        {
            Location = new Point(x, y);
            X = x; Y = y;

            if (!InBounds())
                GotOutOfScreen?.Invoke();

            LocationChanged?.Invoke();
        }

        public void SetSpeed(int speed)
        {
            Speed = speed;
            SpeedChanged?.Invoke();
        }

        protected virtual bool InBounds()
        {
            if (Location.X < -130) 
                return false;

            return true;
        }

    }
}
