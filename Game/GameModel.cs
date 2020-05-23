using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Game
{
    public class GameModel
    {
        public Plane Player { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Score { get; set; }
        public int ShotsLimit { get; set; }
        public bool Running { get; private set; }
        public bool Lost { get; set; }

        public HashSet<Obstacle> Obstacles = new HashSet<Obstacle>();
        public HashSet<Blast> Blasts = new HashSet<Blast>();
        public HashSet<Enemy> Enemies = new HashSet<Enemy>();

        public GameModel(Size size, Point planeInitialPoint)
        {   
            Width = size.Width;
            Height = size.Height;
            ShotsLimit = 1;
            Player = new Plane(Sizes.Collection["xwing"].Width, Sizes.Collection["xwing"].Height,
                planeInitialPoint.X, planeInitialPoint.Y, 9);
        }

        public void Start(int obstaclesCount)
        {
            Obstacles = GenerateObstacles(obstaclesCount);
            Enemies.Add(new Enemy(Sizes.Collection["obstacle"], 1000, 300));
            Running = true;
        }

        public void Finish()
        {
            if (Running)
            {
                Running = false;
                Lost = true;
            }    
        }

        public void Restart(int obstaclesCount)
        {
            Score = 0;
            Lost = false;
            Obstacles = new HashSet<Obstacle>();
            Blasts = new HashSet<Blast>();
            Player.SetLocation(10, Height / 2);

            Start(obstaclesCount);
        }


        public bool Fire()
        {
            if (Running)
            {
                if (Blasts.Count < ShotsLimit)
                {
                    Blasts.Add(new Blast(Sizes.Collection["blast"].Width, Sizes.Collection["blast"].Height,
                        Player.X + Player.Width - 80, Player.Y - 6, 10));

                    return true;
                }
            }
            return false;
        }

        public void CalculateObstaclePoisiton(Obstacle obs)
        {
            Random rand = new Random();
            obs.SetLocation(Width + rand.Next(0, 200), rand.Next(50, Height - 50));
        }

        public HashSet<Obstacle> GenerateObstacles(int count)
        {
            Random rand = new Random();
            var result = new HashSet<Obstacle>();

            for (var i = 0; i < count; i++)
            {
                var startNumber = Obstacles.Count != 0 ? Obstacles.Last().Location.X : 0;

                var obs = new Obstacle(Sizes.Collection["obstacle"],
                    Width + rand.Next(startNumber, startNumber + 500),
                    rand.Next(0, Height));

                result.Add(obs);
            }
                
            return result;
        }
    }
}
