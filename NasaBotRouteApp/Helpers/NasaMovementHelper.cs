

namespace NasaBotRouteApp.Helpers;

public static class NasaMovementHelper
{
    public static (int deltaX, int deltaY) GetMovementDelta(CompassDirectionTypes direction, int moveDistance = 1)
    {
        return direction switch
        {
            CompassDirectionTypes.North => (0, moveDistance),
            CompassDirectionTypes.East => (moveDistance, 0),
            CompassDirectionTypes.South => (0, -moveDistance),
            CompassDirectionTypes.West => (-moveDistance, 0),
            _ => throw new ArgumentException("Invalid direction"),
        };
    }
    public static CompassDirectionTypes GetTurnDirection(CompassDirectionTypes currentDirection, NasaMoveDirectionTypes turnDirection)
    {
        var directions = Enum.GetValues(typeof(CompassDirectionTypes)).Cast<CompassDirectionTypes>().ToList();
        int currentIndex = directions.IndexOf(currentDirection);
        if (turnDirection == NasaMoveDirectionTypes.Left)
        {
            currentIndex = (currentIndex - 1 + directions.Count) % directions.Count; // Counter-clockwise
        }
        else if (turnDirection == NasaMoveDirectionTypes.Right)
        {
            currentIndex = (currentIndex + 1) % directions.Count; // Clockwise
        }
        else
        {
            throw new ArgumentException("Invalid turn direction");
        }
        return directions[currentIndex];
    }
    public static void ParseMaxCoordinates(string? input, out int maxX, out int maxY)
    {
        maxX = maxY = 0; // Default minimum coordinates
        var inputKeys = input?.Split(" ").ToArray();
        if (inputKeys is null || inputKeys.Length != 2)
        {
            throw new ArgumentException("Input must contain exactly two integers representing the maximum coordinates.");
        }
        var isValidIntInput = inputKeys!.All(key => int.TryParse(key.ToString(), out _));
        if (!isValidIntInput)
        {
            throw new ArgumentException("Input must contain valid integers for the maximum coordinates.");
        }
        maxX = int.Parse(inputKeys![0].ToString());
        maxY = int.Parse(inputKeys[1].ToString());
        if (maxX < NasaKeyWordsConstants.MinX || maxY < NasaKeyWordsConstants.MinY)
        {
            throw new ArgumentOutOfRangeException("Maximum coordinates must be greater than zero.");
        }
        var equalityCheck = maxX == NasaKeyWordsConstants.MinX && maxY == NasaKeyWordsConstants.MinY;
        if (equalityCheck)
        {
            throw new ArgumentOutOfRangeException("Maximum coordinates cannot be both zero.");
        }
    }
    public static void ParseRoverRobbotInitCoordinates(string? input, out int x, out int y, out CompassDirectionTypes direction)
    {
        x = y = 0;
        direction = CompassDirectionTypes.North; // Default direction
        var inputKeys = input?.Split(" ").ToArray();
        if (inputKeys is null || inputKeys.Length != 3)
        {
            throw new ArgumentException("Input must contain exactly three parts: two integers for coordinates and one character for direction.");
        }
        var isValidIntInput = inputKeys!.Take(2).All(key => int.TryParse(key.ToString(), out _));
        if (!isValidIntInput)
        {
            throw new ArgumentException("The first two parts of the input must be valid integers representing the rover's initial coordinates.");
        }
        x = int.Parse(inputKeys![0].ToString());
        y = int.Parse(inputKeys[1].ToString());
        direction = inputKeys[2].ToCompassDirectionTypes();
    }
    public static void ParseRoverMoveList(string? input, out List<NasaMoveDirectionTypes> nasaMoveDirectionTypes)
    {
        nasaMoveDirectionTypes = [];
        var inputKeys = input?.ToCharArray();
        if (inputKeys is null || inputKeys.Length == 0)
        {
            throw new ArgumentException("Input must contain at least one move direction character.");
        }
        foreach (var key in inputKeys)
        {
            nasaMoveDirectionTypes.Add(key.ToString().ToNasaMoveDirection());
        }
    }
}