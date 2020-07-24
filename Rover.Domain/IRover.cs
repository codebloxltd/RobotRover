using System.Collections.Generic;

namespace RobotRover.Domain
{
    public interface IRover
    {
        Position Position { get; }
        Grid Grid { get; set; }
        bool MoveIsPermissible { get; set; }
        List<string> Commands { get; set; }
        void SetPosition(int x, int y, string direction);
        void Move(string commands);
    }
}
