using System;
using System.Collections.Generic;

namespace RobotRover.Domain
{
    public class Rover : IRover
    {
        //private bool _withinGrid = true;
        public Position Position { get; }
        public Grid Grid { get; set; }

        public Rover()
        {
            // Set default position
            Position = new Position
            {
                X = 0,
                Y = 0,
                Direction = Direction.North
            };

            //Set grid size
            Grid = new Grid
            {
                X = 40,
                Y = 30
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
                var withinGrid = true;

                // Perform each command
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
                        withinGrid = CheckWithinGrid(steps);
                        if (withinGrid)
                            MoveForward(steps);
                        break;
                }

                if (!withinGrid)
                {
                    Console.WriteLine("Command: '{0}' is out of bounds", command);
                    break;
                }

                // Display each command in console
                Console.WriteLine("Command: '{0}' => [{1},{2},{3}]", command, Position.X, Position.Y, (char)Position.Direction);
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

            move[Position.Direction].Invoke();
        }

        private bool CheckWithinGrid(int steps)
        {
            var _withinGrid = true;

            var move = new Dictionary<Direction, Action>
            {
                { Direction.North, () => _withinGrid = (Position.Y + steps) >= 0 && (Position.Y + steps) <= Grid.Y },
                { Direction.East, () => _withinGrid = (Position.X + steps) >= 0 && (Position.X + steps) <= Grid.X },
                { Direction.South, () => _withinGrid = (Position.Y - steps) >= 0 && (Position.Y - steps) <= Grid.Y },
                { Direction.West, () => _withinGrid = (Position.X - steps) >= 0 && (Position.X - steps) <= Grid.X  }
            };

            move[Position.Direction].Invoke();

            return _withinGrid;
        }
    }
}
