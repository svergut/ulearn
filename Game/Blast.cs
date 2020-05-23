using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Blast : GameObject
    {
        public Blast(int width, int height, int x, int y) : base(width, height, x, y) { }
        public Blast(int width, int height, int x, int y, int speed) : base(width, height, x, y, speed) { }

        public bool IntersectsWithObstacle(Obstacle obs)
        {
            if (Tools.Intersect(X, X + Width,
                obs.X, obs.X + obs.Width,
                Y, Y + Height,
                obs.Y, obs.Y + obs.Height))
                return true;

            return false;
        }

        public bool IntersectsWithEnemy(Enemy enemy)
        {
            if (Tools.Intersect(X, X + Width,
                enemy.X, enemy.X + enemy.Width,
                Y, Y + Height,
                enemy.Y, enemy.Y + enemy.Height))
                return true;

            return false;
        }

        protected override bool InBounds()
        {
            return X < Sizes.Collection["game"].Width;
        }
    }
}
