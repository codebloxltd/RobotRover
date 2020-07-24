using System;
using System.Collections.Generic;

namespace RobotRover.Domain
{
    public class Rover : IRover
    {
        public Position Position { get; }
        public Grid Grid { get; set; }
        public bool MoveIsPermissible { get; set; }
        public List<string> Commands { get; set; }

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

            // Initialise properties
            Commands = new List<string>();
            MoveIsPermissible = true;
        }

        public void SetPosition(int x, int y, string direction)
        {
            Position.X = x;
            Position.Y = y;
            Position.Direction = (Direction)Convert.ToChar(direction);
        }

        public void Move(string commands)
        {
            MoveIsPermissible = true;
            Commands.Clear();

            char[] commandList = commands.ToCharArray(); // Split commands and perform each separate command

            foreach (char command in commandList)
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
                        MoveIsPermissible = PermissibleMove(steps);
                        if (MoveIsPermissible) // If the move is permissible then move rover
                            MoveForward(steps);
                        break;
                }

                if (!MoveIsPermissible)
                {
                    Commands.Add($"Command: '{command}' is out of bounds");
                    break;
                }

                Commands.Add($"Command: '{command}' => [{Position.X},{Position.Y},{(char)Position.Direction}]");
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

        private bool PermissibleMove(int steps)
        {
            var permissible = true;

            var move = new Dictionary<Direction, Action>
            {
                { Direction.North, () => permissible = (Position.Y + steps) >= 0 && (Position.Y + steps) <= Grid.Y },
                { Direction.East, () => permissible = (Position.X + steps) >= 0 && (Position.X + steps) <= Grid.X },
                { Direction.South, () => permissible = (Position.Y - steps) >= 0 && (Position.Y - steps) <= Grid.Y },
                { Direction.West, () => permissible = (Position.X - steps) >= 0 && (Position.X - steps) <= Grid.X  }
            };

            move[Position.Direction].Invoke();

            return permissible;
        }
    }
}