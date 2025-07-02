namespace NasaBotRouteApp.Models.Abstract;

public abstract class BaseSurfaceRobot(int x, int y, CompassDirectionTypes direction) : BaseRobot(x, y, direction)
{
    public override void Move(int deltaX, int deltaY)
    {
        X += deltaX;
        Y += deltaY;
    }
    public override void ChangeDirection(CompassDirectionTypes direction)
    {
        Direction = direction;
    }
    public override void ReportPosition()
    {
        Console.WriteLine($"{X} {Y} {Direction.ToStringKey()}");
    }
}
