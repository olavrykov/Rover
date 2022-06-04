using PlutoNS;

namespace PlutoTest
{
    [TestClass]
    public class PlutoTestClass
    {
        [TestMethod]
        public void Given_Path_Without_Obstacles_When_Pluto_Then_Run_Ok()
        {
            // Arrange
            PlutoMob pm = new PlutoMob();
            string path = "FFLFFFFBB";

            // Act
            PlutoPosition pp = pm.Run(path);

            // Assert
            Assert.AreEqual(false, pm.obstacleAppeared, "obstacleAppeared");
            Assert.AreEqual('W', pp.cdir, "Direction");
            Assert.AreEqual(98, pp.x, "X coord");
            Assert.AreEqual( 2, pp.y, "Y coord");
        }
        [TestMethod]
        public void Given_Path_With_Obstacles_When_Pluto_Then_Run_Ok()
        {
            // Arrange
            PlutoMob pm = new PlutoMob();
            var obstacle = new List<Point>();
            obstacle.Add(new Point(0, 1));
            pm.SetObstacle(obstacle);
            string path = "FFLFF";

            // Act
            PlutoPosition pp = pm.Run(path);

            // Assert
            Assert.AreEqual(true, pm.obstacleAppeared, "obstacleAppeared");
            Assert.AreEqual('N', pp.cdir, "Direction");
            Assert.AreEqual(0, pp.x, "X coord");
            Assert.AreEqual(0, pp.y, "Y coord");
        }
        [TestMethod]
        public void Given_Path_With_Obstacles_When_Pluto_Then_TryMove_True()
        {
            // Arrange
            PlutoMob pm = new PlutoMob();
            var obstacle = new List<Point>();
            obstacle.Add(new Point(0, 1));
            pm.SetObstacle(obstacle);

            // Act
            var r = pm.TryMove(false); // backwards

            // Assert
            Assert.AreEqual(true, r, "TryMove");
        }
    }
}