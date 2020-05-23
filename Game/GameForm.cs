using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Linq;

namespace Game
{
    class GameForm : Form
    {
        public static GameModel Game = new GameModel(Sizes.Collection["game"], new Point(40, 300));
        public static SoundPlayer blasterSound = new SoundPlayer(@"..\..\res\sound\blaster.wav");
        public static SoundPlayer explosionSound = new SoundPlayer(@"..\..\res\sound\exp.wav");
        public static Image PlayerCurrentStructure = Textures.Collection["xwing_state1"];
        public static Timer timer = new Timer() { Interval = 1 };

        public GameForm()
        {
            Width = 1000;
            Height = Game.Height;
            DoubleBuffered = true;
            WindowState = FormWindowState.Maximized;
            Game.ShotsLimit = 3;

            var canvas = new CustomPanel() 
            {
                Width = Game.Width,
                Height = Game.Height,
                BackgroundImage = Textures.Collection["corusant_state1"],
                Dock = DockStyle.Fill,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            canvas.Paint += (sender, args) =>
            {
                Drawer.PaintStartScreen(args.Graphics, canvas);
                Drawer.PaintScore(args.Graphics, Game);
                Drawer.PaintPlayer(args.Graphics, Game.Player);
                Drawer.PaintObstacles(args.Graphics, Game);
                Drawer.PaintBlasts(args.Graphics, Game.Blasts);
                Drawer.PaintEnemies(args.Graphics);
                Drawer.PaintDeathScreen(args.Graphics, canvas);
            };
            KeyDown += KeyBoardHandler;

            Controls.Add(canvas);

            var bgCounter = 1;
            var planeCounter = 1;

            timer.Tick += (sender, args) =>
            {
                Tools.SetCanvasFlickering(canvas, ref bgCounter, 30); 
                Tools.SetPlaneFlickering(Game, ref planeCounter, 10);

                foreach (var obs in Game.Obstacles)
                {
                    obs.SetLocation(obs.X - 10, obs.Y);

                    if (obs.IntersectsWithPlayer(Game.Player))
                    {
                        explosionSound.Play();
                        PlayerCurrentStructure = Textures.Collection["xwing_destroyed"];
                        Game.Finish();
                        timer.Stop();
                    }    
                }

                var blastsToRemove = new HashSet<Blast>();
                var enemiesToRemove = new HashSet<Enemy>();

                foreach (var blast in Game.Blasts)
                {
                    var obsToRemove = new HashSet<Obstacle>();
                    blast.Move(MoveDirection.Right);

                    if (blast.X > Game.Width)
                        blastsToRemove.Add(blast);

                    foreach (var obs in Game.Obstacles)
                    {
                        if (blast.IntersectsWithObstacle(obs))
                        {
                            blastsToRemove.Add(blast);
                            obsToRemove.Add(obs);
                            Game.Score++;
                        }        
                    }

                    foreach (var enemy in Game.Enemies)
                    {
                        if (blast.IntersectsWithEnemy(enemy))
                        {
                            blastsToRemove.Add(blast);
                            enemiesToRemove.Add(enemy);
                            Game.Score *= 2;
                        }
                    }

                    foreach (var enemy in enemiesToRemove)
                        Game.Enemies.Remove(enemy);

                    foreach (var obs in obsToRemove)
                    {
                        Game.Obstacles.Remove(obs);
                        Game.Obstacles.Add(Game.GenerateObstacles(1).First());
                    }
                }

                foreach (var blast in blastsToRemove)
                    Game.Blasts.Remove(blast);
                    
                canvas.Invalidate();
            }; 
        }

        public void KeyBoardHandler(object sender, KeyEventArgs args)
        {
            if (args.KeyValue == 'W')
                Game.Player.Move(MoveDirection.Up);
            if (args.KeyValue == 'S')
                Game.Player.Move(MoveDirection.Down);
            if (args.KeyValue == 'F')
            {
                if (Game.Fire())
                    blasterSound.Play();
            }
            if (args.KeyValue == 'A')
                Game.Player.Move(MoveDirection.Left);
            if (args.KeyValue == 'D')
                Game.Player.Move(MoveDirection.Right);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
        
        }

        public class CustomPanel : Panel
        {
            public CustomPanel()
            {
                DoubleBuffered = true;
            }
        }
    }
}
