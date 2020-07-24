namespace RobotRover.Domain
{
    public interface IRover
    {
        Position Position { get; }

        void SetPosition(int x, int y, string direction);

        void Move(string commands);
    }
}
