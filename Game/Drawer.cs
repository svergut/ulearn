using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class Drawer
    {
        public static void PaintTitles(Graphics g, Panel canvas)
        {
            g.DrawImage(Image.FromFile(@"..\..\res\title.png"), new Point(0, 0));
        }

        public static void PaintEnemies(Graphics g)
        {
            foreach (var enemy in GameForm.Game.Enemies)
            {
                g.ResetTransform();
                g.DrawImage(Textures.Collection["enemy"], enemy.X, enemy.Y, enemy.Width, enemy.Height);
            }
        }

        public static void PaintBlasts(Graphics g, HashSet<Blast> blasts)
        {
            var toRemove = new HashSet<Blast>();

            foreach (var blast in blasts)
            {
                blast.GotOutOfScreen += () =>
                {
                    toRemove.Add(blast);
                };

                g.ResetTransform();
                g.DrawImage(Textures.Collection["blast"], blast.X, blast.Y, blast.Width, blast.Height);
            }

            foreach (var blast in toRemove)
                blasts.Remove(blast);
        }

        public static void PaintStartScreen(Graphics g, Panel canvas)
        {
            var brush = new SolidBrush(Color.FromArgb(110, 0, 0, 0));
            var mainFont = new Font("Arial", 40);
            var background = new Rectangle()
            {
                Width = canvas.Width,
                Height = canvas.Height,
            };
            var mainTextRectangle = new Rectangle()
            {
                Width = 800,
                Height = 70,
                X = canvas.Width / 2 - 300,
                Y = canvas.Height / 2 - 200
            };

            if (!GameForm.Game.Running && !GameForm.Game.Lost)
            {
                g.ResetTransform();
                g.FillRectangle(brush, background);
                g.DrawString("Click anywhere to start", mainFont, Brushes.White, mainTextRectangle);

                canvas.Click += (sender, args) =>
                {
                    if (!GameForm.Game.Running && !GameForm.Game.Lost)
                    {
                        GameForm.Game.Start(5);
                        GameForm.timer.Start();
                    }        
                };
            }    
        }



        public static void PaintDeathScreen(Graphics g, Panel canvas)
        {
            var brush = new SolidBrush(Color.FromArgb(90, 0, 0, 0));
            var mainFont = new Font("Arial", 40);
            var additionalFont = new Font("Arial", 20);
            var background = new Rectangle()
            {
                Width = canvas.Width,
                Height = canvas.Height,       
            };
            var mainTextRectangle = new Rectangle()
            {
                Width = 800,
                Height = 50,
                X = canvas.Width / 2 - 200,
                Y = canvas.Height / 2 - 200
            };
            var additionalTextRectangle = new Rectangle()
            {
                Width = 800,
                Height = 50,
                X = canvas.Width / 2 - 130,
                Y = canvas.Height / 2 - 120
            };

            if (GameForm.Game.Lost)
            {
                g.ResetTransform();
                g.FillRectangle(brush, background);
                g.DrawString("YOU'VE LOST!", mainFont, Brushes.White, mainTextRectangle);
                g.DrawString("YOUR SCORE IS " + GameForm.Game.Score, additionalFont, Brushes.White, additionalTextRectangle);
                canvas.Click += (sender, args) =>
                {
                    if (GameForm.Game.Lost)
                    {
                        GameForm.Game.Restart(5);
                        GameForm.timer.Start();
                    }                 
                };
            }   
        }

        public static void PaintScore(Graphics g, GameModel game)
        {
            g.ResetTransform();
            var font = new Font("Arial", 16);
            g.DrawString("SCORE: " + game.Score, font, Brushes.White, 0, 0);
        }

        public static void PaintPlayer(Graphics g, Plane player)
        {
            g.DrawImage(GameForm.PlayerCurrentStructure, player.Location.X,
                player.Y, player.Width, player.Height);
        }

        public static void PaintObstacles(Graphics g, GameModel game)
        {
            foreach (var obs in game.Obstacles)
            {
                obs.GotOutOfScreen += () =>
                {
                    game.CalculateObstaclePoisiton(obs);
                };

                g.DrawImage(Textures.Collection["obstacle1"], obs.X, obs.Y, obs.Width, obs.Height);
                g.RotateTransform(0.8f);
            }
        }
    }
}
