using RobotRover.Domain;

namespace RobotRover.Infrastructure
{
    public interface IRoverAccessor
    {
        Position Position { get; }

        void SetPosition(int x, int y, string direction);

        void Move(string commands);
    }
}
