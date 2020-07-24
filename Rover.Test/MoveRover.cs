using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotRover.Domain;

namespace RobotRover.Test
{
    [TestClass]
    public class MoveRover
    {
        [TestMethod]
        public void Valid_Command_Sequence_Movement_Is_Successful()
        {
            // Arrange
            var navigation = new Rover();

            // Act
            navigation.SetPosition(10, 10, "N");
            navigation.Move("R1R3L2L1");

            // Assert
            Assert.AreEqual(navigation.Position.X, 13);
            Assert.AreEqual(navigation.Position.Y, 8);
            Assert.AreEqual(navigation.Position.Direction, Direction.North);
        }
    }
}
