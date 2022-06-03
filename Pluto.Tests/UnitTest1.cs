using PlutoNS;

namespace PlutoTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestRun()
        {
            // Arrange
            PlutoMob pm = new PlutoMob();
            string path = "FFLFFFFBB";

            // Act
            var r = pm.Run(path);

            // Assert
            Assert.AreEqual(false, pm.obstacleAppeared, "obstacleAppeared");
            Assert.AreEqual('W', r.Item1, "Direction");
            Assert.AreEqual(98, r.Item2, "X coord");
            Assert.AreEqual(2, r.Item3, "Y coord");
        }
        [TestMethod]
        public void TestRunObstacle()
        {
            // Arrange
            PlutoMob pm = new PlutoMob();
            var obstacle = new List<Tuple<int, int>>();
            obstacle.Add(new Tuple<int, int>(0, 1));
            pm.SetObstacle(obstacle);
            string path = "FFLFF";

            // Act
            var r = pm.Run(path);

            // Assert
            Assert.AreEqual(true, pm.obstacleAppeared, "obstacleAppeared");
            Assert.AreEqual('N', r.Item1, "Direction");
            Assert.AreEqual(0, r.Item2, "X coord");
            Assert.AreEqual(0, r.Item3, "Y coord");
        }
        [TestMethod]
        public void TestTryMove()
        {
            // Arrange
            PlutoMob pm = new PlutoMob();
            var obstacle = new List<Tuple<int, int>>();
            obstacle.Add(new Tuple<int, int>(0, 1));
            pm.SetObstacle(obstacle);

            // Act
            var r = pm.TryMove(false); // backwards

            // Assert
            Assert.AreEqual(true, r, "TryMove");
        }
    }
}