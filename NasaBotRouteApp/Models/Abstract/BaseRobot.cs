namespace NasaBotRouteApp.Models.Abstract;

public abstract class BaseRobot(int x, int y, CompassDirectionTypes direction) : IRobot
{
    public int X { get; protected set; } = x;
    public int Y { get; protected set; } = y;
    public CompassDirectionTypes Direction { get; protected set; } = direction;
    public abstract void Move(int deltaX, int deltaY);
    public abstract void ChangeDirection(CompassDirectionTypes direction);
    public abstract void ReportPosition();

}