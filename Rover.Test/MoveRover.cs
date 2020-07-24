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
            var rover = new Rover();

            // Act
            rover.SetPosition(10, 10, "N");
            rover.Move("R1R3L2L1");

            // Assert
            Assert.AreEqual(rover.Position.X, 13);
            Assert.AreEqual(rover.Position.Y, 8);
            Assert.AreEqual(rover.Position.Direction, Direction.North);
            Assert.AreEqual(rover.MoveIsPermissible, true);
        }

        [TestMethod]
        public void Impermissible_Command_Sequence_Causes_Rover_To_Stop()
        {
            // Arrange
            var rover = new Rover();

            // Act
            rover.SetPosition(10, 10, "N");
            rover.Move("R9L9R9L9R9L9");

            // Assert
            Assert.AreEqual(rover.MoveIsPermissible, false);
        }
    }
}
