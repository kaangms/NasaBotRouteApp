namespace NasaBotRouteApp.Models.Dtos
{
    public record RoverRobotValidationFilterData(int MaxX, int MaxY, int MinX = 0, int MinY = 0) : IDTo;
}