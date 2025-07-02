namespace NasaBotRouteApp.Models.Concrete;

public class RoverRobot(int x, int y, CompassDirectionTypes direction) : BaseSurfaceRobot(x, y, direction)
{
    public RoverRobot(int x, int y, CompassDirectionTypes direction, RoverRobotValidationFilterData filterData) : this(x, y, direction)
    {
        if (x < NasaKeyWordsConstants.MinX || x > filterData.MaxX)
        {
            throw new ArgumentOutOfRangeException(nameof(x), $"X coordinate must be between {NasaKeyWordsConstants.MinX} and {filterData.MaxX}.");
        }

        if (y < NasaKeyWordsConstants.MinY || y > filterData.MaxY)
        {
            throw new ArgumentOutOfRangeException(nameof(y), $"Y coordinate must be between {NasaKeyWordsConstants.MinY} and {filterData.MaxY}.");
        }
    }

    public bool CheckValidity(int deltaX, int deltaY, IEnumerable<RoverRobot>? otherRoverRobots, RoverRobotValidationFilterData filterData)
    {
        var testX = X + deltaX;
        var testY = Y + deltaY;
        bool isWithinBounds =
                testX >= NasaKeyWordsConstants.MinX && testX <= filterData.MaxX &&
                testY >= NasaKeyWordsConstants.MinY && testY <= filterData.MaxY;

        if (isWithinBounds is false)
        {
            return false;
        }
        if (otherRoverRobots is null || !otherRoverRobots.Any())
        {
            return true; // No other robots to collide with
        }
        foreach (var otherRobot in otherRoverRobots)
        {
            if (otherRobot.X == testX && otherRobot.Y == testY)
            {
                return false; // Collision detected
            }
        }
        return true;
    }
}