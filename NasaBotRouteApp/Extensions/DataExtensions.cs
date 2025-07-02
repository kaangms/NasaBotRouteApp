namespace NasaBotRouteApp.Extensions;

public static class DataExtensions
{
    public static string Value(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            throw new ArgumentNullException(nameof(input), "Input cannot be null or empty.");
        }
        return input!;
    }
    public static NasaMoveDirectionTypes ToNasaMoveDirection(this string moveDirection) => moveDirection switch
    {
        "L" => NasaMoveDirectionTypes.Left,
        "R" => NasaMoveDirectionTypes.Right,
        "M" => NasaMoveDirectionTypes.Move,
        _ => throw new ArgumentException("Invalid move direction type. Use 'L', 'R', or 'M'.")
    };
    public static CompassDirectionTypes ToCompassDirectionTypes(this string direction) => direction switch
    {
        "N" => CompassDirectionTypes.North,
        "E" => CompassDirectionTypes.East,
        "S" => CompassDirectionTypes.South,
        "W" => CompassDirectionTypes.West,
        _ => throw new ArgumentException("Invalid compass direction type. Use 'N', 'E', 'S', or 'W'.")
    };
    public static string ToStringKey(this NasaMoveDirectionTypes moveDirection) => moveDirection switch
    {
        NasaMoveDirectionTypes.Left => "L",
        NasaMoveDirectionTypes.Right => "R",
        NasaMoveDirectionTypes.Move => "M",
        _ => throw new ArgumentOutOfRangeException(nameof(moveDirection), "Invalid move direction type.")
    };
    public static string ToStringKey(this CompassDirectionTypes direction) => direction switch
    {
        CompassDirectionTypes.North => "N",
        CompassDirectionTypes.East => "E",
        CompassDirectionTypes.South => "S",
        CompassDirectionTypes.West => "W",
        _ => throw new ArgumentOutOfRangeException(nameof(direction), "Invalid compass direction type.")
    };
}