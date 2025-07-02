namespace NasaBotRouteApp.Service
{
    public interface INasaRoverMovementService
    {
        void CreateRoverRobot(string? input);
        void MoveUpdate(string? input, int? roverIndex = null);
        void ReportPositions(int? selectedIndex = null);
        IEnumerable<RoverRobot> GetRoverRobots(int? selectedIndex = null);
    }
}
