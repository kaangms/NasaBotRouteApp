using NasaBotRouteApp.Service;

var robotCount = 2;
var input = Console.ReadLine()!;
var nasaMovementService = new NasaRoverMovementService(input, robotCount);
while (robotCount != 0)
{
    robotCount--;
    input = Console.ReadLine();
    nasaMovementService.CreateRoverRobot(input);
    input = Console.ReadLine();
    nasaMovementService.MoveUpdate(input);
}

nasaMovementService.ReportPositions();


