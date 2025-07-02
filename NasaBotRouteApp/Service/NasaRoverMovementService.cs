namespace NasaBotRouteApp.Service;

public class NasaRoverMovementService : INasaRoverMovementService
{
    private readonly RoverRobotValidationFilterData _roverRobotValidationFilterData;
    private RoverRobot[] RoverRobots { get; set; }
    private int? CurrentRoverIndex { get; set; } = null;

    public NasaRoverMovementService(string? input, int robotCout = 2)
    {
        RoverRobots = robotCout > 0
                    ? new RoverRobot[robotCout]
                    : throw new ArgumentOutOfRangeException(nameof(robotCout), "Robot count must be greater than 0.");

        NasaMovementHelper.ParseMaxCoordinates(input.Value(), out int maxX, out int maxY);
        _roverRobotValidationFilterData = new RoverRobotValidationFilterData(maxX, maxY);
    }
    public void CreateRoverRobot(string? input)
    {
        NasaMovementHelper.ParseRoverRobbotInitCoordinates(input.Value(), out int deltaX, out int deltaY, out CompassDirectionTypes direction);
        var roverRobot = new RoverRobot(deltaX, deltaY, direction, _roverRobotValidationFilterData);
        SetRoverRobots(roverRobot);
    }

    public void MoveUpdate(string? input, int? roverIndex = null)
    {

        CheckAndSetCurrentIndex(roverIndex);
        var roverRobot = RoverRobots[CurrentRoverIndex!.Value];
        NasaMovementHelper.ParseRoverMoveList(input.Value(), out List<NasaMoveDirectionTypes> nasaMoveDirectionTypes);
        for (int i = 0; i < nasaMoveDirectionTypes.Count; i++)
        {
            switch (nasaMoveDirectionTypes[i])
            {
                case NasaMoveDirectionTypes.Left:
                case NasaMoveDirectionTypes.Right:
                    var direction = NasaMovementHelper.GetTurnDirection(roverRobot.Direction, nasaMoveDirectionTypes[i]);
                    roverRobot.ChangeDirection(direction);
                    break;
                case NasaMoveDirectionTypes.Move:
                    var (deltaX, deltaY) = NasaMovementHelper.GetMovementDelta(roverRobot.Direction);
                    var otherRoverRobots = RoverRobots.Where((r, index) => index != CurrentRoverIndex && r != null);
                    if (roverRobot.CheckValidity(deltaX, deltaY, otherRoverRobots, _roverRobotValidationFilterData))
                    {
                        roverRobot.Move(deltaX, deltaY);
                    }
                    break;
                default:
                    throw new ArgumentException("Invalid move direction type.");
            }
        }

    }

    public void ReportPositions(int? selectedIndex = null) => GetRoverRobots(selectedIndex).ToList().ForEach(rover => rover.ReportPosition());
    public IEnumerable<RoverRobot> GetRoverRobots(int? selectedIndex = null) =>
                              RoverRobots.Where((r, index) =>
                              (selectedIndex == null || selectedIndex == index)
                              && r != null);
    private void SetRoverRobots(RoverRobot roverRobot)
    {
        if (CurrentRoverIndex.HasValue)
        {
            CurrentRoverIndex = CurrentRoverIndex.Value + 1;
            CheckRoverRobot(roverRobot);
            RoverRobots[CurrentRoverIndex.Value] = roverRobot;
        }
        else
        {
            CurrentRoverIndex = 0;
            RoverRobots[CurrentRoverIndex.Value] = roverRobot;
        }
    }
    private void CheckRoverRobot(RoverRobot roverRobot)
    {
        if (CurrentRoverIndex >= RoverRobots.Length)
        {
            throw new InvalidOperationException("Maximum number of rover robots reached.");
        }
        var otherRoverRobots = RoverRobots.Where((r, index) => index != CurrentRoverIndex && r != null).ToList() ?? [];
        if (!roverRobot.CheckValidity(0, 0, [.. otherRoverRobots], _roverRobotValidationFilterData))
        {
            throw new InvalidOperationException("Rover robot cannot be placed at the same position as another rover robot.");
        }
    }
    private void CheckAndSetCurrentIndex(int? roverIndex)
    {
        if (roverIndex.HasValue)
        {
            CurrentRoverIndex = roverIndex.Value;
        }
        bool isInvalidIndex = !CurrentRoverIndex.HasValue
                  || CurrentRoverIndex < 0
                  || CurrentRoverIndex >= RoverRobots.Length;
        if (isInvalidIndex)
        {
            throw new ArgumentOutOfRangeException(nameof(CurrentRoverIndex), "Current rover index is out of range or the rover robot does not exist.");
        }
    }
}
