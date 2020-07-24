using RobotRover.Domain;
using System;
using System.Collections.Generic;

namespace RobotRover.Infrastructure
{
    public class RoverAccessor : IRoverAccessor
    {
        public Position Position { get; }

        public RoverAccessor()
        {
            // Set default position
            Position = new Position
            {
                X = 0,
                Y = 0,
                Direction = Direction.North
            };
        }

        public void SetPosition(int x, int y, string direction)
        {
            Position.X = x;
            Position.Y = y;
            Position.Direction = (Direction)Convert.ToChar(direction);
        }

        public void Move(string commands)
        {
            char[] commandList = commands.ToCharArray(); // Split commands

            foreach (char command in commandList)
            {
                // Perform each command
                Command(command);

                // Display each command in console
                Console.WriteLine("Command: '{0}' => [{1},{2},{3}]", command, Position.X, Position.Y, (char)Position.Direction);
            }
        }

        private void Command(char command)
        {
            switch (command)
            {
                case (char)Navigate.Left: 
                    MoveLeft();
                    break;
                case (char)Navigate.Right: 
                    MoveRight();
                    break;
                default:
                    int steps;
                    int.TryParse(command.ToString(), out steps);
                    MoveForward(steps);
                    break;
            }
        }

        private void MoveLeft()
        {
            var move = new Dictionary<Direction, Direction>
            {
                { Direction.North, Direction.West },
                { Direction.East, Direction.North },
                { Direction.South, Direction.East },
                { Direction.West, Direction.South }
            };

            // Set new position
            Position.Direction = move[Position.Direction];
        }

        private void MoveRight()
        {
            var move = new Dictionary<Direction, Direction>
            {
                { Direction.North, Direction.East },
                { Direction.East, Direction.South },
                { Direction.South, Direction.West },
                { Direction.West, Direction.North }
            };

            // Set new position
            Position.Direction = move[Position.Direction];
        }

        private void MoveForward(int steps)
        {
            var move = new Dictionary<Direction, Action>
            {
                { Direction.North, () => Position.Y = Position.Y + steps },
                { Direction.East, () => Position.X = Position.X + steps },
                { Direction.South, () => Position.Y = Position.Y - steps },
                { Direction.West, () => Position.X = Position.X - steps }
            };

            // Set new position
            move[Position.Direction].Invoke();
        }
    }
}
