using NUnit.Framework;
using System;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;

namespace Game
{
    [TestFixture]
    public class Tests
    {
        [TestFixture]
        public class BasicTests
        {
            private static GameModel Game = new GameModel(Sizes.Collection["game"], new Point(0, 0));
            private static Plane player = Game.Player;

            [Test]
            public void TestGameCreation()
            {
                Assert.AreEqual(0, Game.Score);
                Assert.AreEqual(0, Game.Blasts.Count);
                Assert.AreEqual(0, Game.Obstacles.Count);
            }

            [Test]
            public void TestGameRunning()
            {
                Game.Start(3);

                Assert.AreEqual(3, Game.Obstacles.Count);
                Assert.AreEqual(0, Game.Blasts.Count);
                Assert.AreNotEqual(0, Game.Player.Speed);
            }

            [Test]
            public void TestPlayerMoving()
            {
                var oldLocation = player.Location;

                player.Move(MoveDirection.Right);
                player.Move(MoveDirection.Down);
                Assert.AreEqual(new Point(oldLocation.X + player.Speed,
                    oldLocation.Y + 2 * player.Speed), player.Location);
            }

            [Test]
            public void TestGameLoosing()
            {
                Game.Start(1);
                Game.Finish();
                Assert.AreEqual(true, Game.Lost);
                Assert.AreEqual(false, Game.Running);
            }

            [Test]
            public void TestPositionCalculation()
            {
                Game.Start(1);

                var obstacle = Game.Obstacles.First();
                var oldLocation = obstacle.Location;
                Game.CalculateObstaclePoisiton(obstacle);

                Assert.AreNotEqual(oldLocation, obstacle.Location);
            }
        }

        [TestFixture]
        public class ShootingTests
        {
            [Test]
            public void TestSingleShotFiring()
            {
                var game = new GameModel(Sizes.Collection["game"], new Point(20, 100));
                game.Start(0);
                game.Fire();

                Assert.AreEqual(1, game.Blasts.Count);
            }

            [Test]
            public void TestSeveralShotsFiring()
            {
                var game = new GameModel(Sizes.Collection["game"], new Point(20, 100));
                game.Start(0);
                game.ShotsLimit = 7;
                var shotsCount = new Random().Next(0, 6);

                for (var i = 0; i < shotsCount; i++)
                    game.Fire();

                Assert.AreEqual(shotsCount, game.Blasts.Count);
            }

            [Test]
            public void TestShootingLimitation()
            {
                var game = new GameModel(Sizes.Collection["game"], new Point(20, 100));
                game.Start(0);
                game.ShotsLimit = 2;

                for (var i = 0; i < 3; i++)
                    game.Fire();

                Assert.AreEqual(2, game.Blasts.Count);
            }
        }

        [TestFixture]
        public class TestIntersection
        {
            [Test]
            public void TestObstacleIntersectionWithPlayer()
            {
                var game = new GameModel(Sizes.Collection["game"], new Point(20, 100));
                game.Start(1);
                var obstacle = game.Obstacles.First();
                obstacle.SetLocation(20, 100);

                Assert.AreEqual(true, obstacle.IntersectsWithPlayer(game.Player));
            }

        }
    }
}
