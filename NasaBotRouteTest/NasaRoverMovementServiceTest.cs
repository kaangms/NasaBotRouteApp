namespace NasaBotRouteTest;

public class NasaRoverMovementServiceTest
{
    private readonly Random _random = new Random();
    private const string ValidInput = "5 5";
    private const int ValidRobotCount = 2;
    private const string FirstRoverRobotCurrentInput = "1 2 N";
    private const string FirstRoverRobotMoveInput = "LMLMLMLMM";
    private const string SecondRoverRobotCurrentInput = "3 3 E";
    private const string SecondRoverRobotMoveInput = "MMRMMRMRRM";

    [Theory]
    [InlineData(null, 1, typeof(ArgumentNullException))]
    [InlineData("", 1, typeof(ArgumentNullException))]
    [InlineData("-1 0", 1, typeof(ArgumentOutOfRangeException))]
    [InlineData("0 0", 1, typeof(ArgumentOutOfRangeException))]
    [InlineData("0 -1", 1, typeof(ArgumentOutOfRangeException))]
    [InlineData("1 1", 0, typeof(ArgumentOutOfRangeException))]
    public void CreateErrorInstanceNasaRoverMovementService(string? instanceInput, int robotCount, Type expectedException)
    {
        var exception = Record.Exception(() => new NasaRoverMovementService(instanceInput, robotCount));
        Assert.NotNull(exception);
        Assert.IsType(expectedException, exception);
    }
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(10)]
    public void CheckCreateRobot(int countCreateRobot)
    {
        try
        {
            var instanceService = new NasaRoverMovementService(ValidInput, countCreateRobot);
            var filterData = new RoverRobotValidationFilterData(5, 5, 0, 0);
            List<RoverRobot> roverRobots = [];
            for (int i = 0; i < countCreateRobot; i++)
            {
                var deltaX = _random.Next(0, 15);
                var deltaY = _random.Next(0, 15);
                var direction = (CompassDirectionTypes)_random.Next(0, 4);
                var randomInputCreateRoverRobot = $"{deltaX} {deltaY} {direction.ToStringKey()}";
                if (deltaX < 0 || deltaY < 0 || deltaX > 5 || deltaY > 5)
                {
                    Assert.Throws<ArgumentOutOfRangeException>(() => instanceService.CreateRoverRobot(randomInputCreateRoverRobot));
                }
                else
                {
                    var roverRobot = new RoverRobot(deltaX, deltaY, direction, filterData);
                    if (roverRobot.CheckValidity(0, 0, roverRobots, filterData))
                    {
                        instanceService.CreateRoverRobot(randomInputCreateRoverRobot);
                        roverRobots.Add(roverRobot);
                    }
                    else
                    {
                        Assert.Throws<InvalidOperationException>(() => instanceService.CreateRoverRobot(randomInputCreateRoverRobot));
                    }
                }

            }
        }
        catch (System.Exception)
        {

            Assert.Fail("An exception occurred during the rover robot creation test.");
        }
        Assert.True(true, "Rover robot creation test completed successfully.");

    }
    [Fact]
    public void MoveCheckRobot()
    {
        try
        {
            var instanceService = new NasaRoverMovementService(ValidInput, ValidRobotCount);
            #region FirstRoverRobot
            instanceService.CreateRoverRobot(FirstRoverRobotCurrentInput);
            var moveInput = string.Empty;
            var moveCount = _random.Next(0, 100);
            for (int i = 0; i < moveCount; i++)
            {
                NasaMoveDirectionTypes moveDirection = (NasaMoveDirectionTypes)_random.Next(0, 3);
                moveInput += moveDirection.ToStringKey();
            }
            instanceService.MoveUpdate(moveInput);
            var robots = instanceService.GetRoverRobots();
            Assert.Single(robots);
            var firstRobot = robots.First();
            Assert.InRange(firstRobot.X, 0, 5);
            Assert.InRange(firstRobot.Y, 0, 5);
            Assert.Contains(firstRobot.Direction.ToStringKey(), new[] { "N", "E", "S", "W" });
            #endregion

            #region SecondRoverRobot
            // Create a new rover robot with a different initial position and direction
            var newX = firstRobot.X == 0 ? 1 : 0;
            instanceService.CreateRoverRobot(newX + " " + firstRobot.Y + " " + firstRobot.Direction.ToStringKey());
            moveInput = string.Empty;
            moveCount = _random.Next(0, 100);
            for (int i = 0; i < moveCount; i++)
            {
                NasaMoveDirectionTypes moveDirection = (NasaMoveDirectionTypes)_random.Next(0, 3);
                moveInput += moveDirection.ToStringKey();
            }
            instanceService.MoveUpdate(moveInput);
            robots = instanceService.GetRoverRobots();
            Assert.Equal(2, robots.Count());
            var secondRobot = robots.Last();
            Assert.InRange(secondRobot.X, 0, 5);
            Assert.InRange(secondRobot.Y, 0, 5);
            Assert.Contains(secondRobot.Direction.ToStringKey(), new[] { "N", "E", "S", "W" });
            #endregion
        }
        catch (System.Exception)
        {
            Assert.Fail("An exception occurred during the rover movement test.");
        }
        Assert.True(true, "Rover movement test completed successfully.");
    }
    [Fact]
    public void CheckCaseOutputResult()
    {
        try
        {
            var instanceService = new NasaRoverMovementService(ValidInput, ValidRobotCount);
            instanceService.CreateRoverRobot(FirstRoverRobotCurrentInput);
            instanceService.MoveUpdate(FirstRoverRobotMoveInput);
            instanceService.CreateRoverRobot(SecondRoverRobotCurrentInput);
            instanceService.MoveUpdate(SecondRoverRobotMoveInput);
            var robots = instanceService.GetRoverRobots().ToList();
            Assert.Equal(2, robots.Count);
            Assert.Equal("1 3 N", $"{robots[0].X} {robots[0].Y} {robots[0].Direction.ToStringKey()}");
            Assert.Equal("5 1 E", $"{robots[1].X} {robots[1].Y} {robots[1].Direction.ToStringKey()}");
        }
        catch (System.Exception)
        {
            Assert.Fail("An exception occurred during the rover movement test.");
        }
        Assert.True(true, "Rover movement test completed successfully.");
    }
}
