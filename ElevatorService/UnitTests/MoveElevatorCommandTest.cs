using Elevator.Application.Commands.MoveElevator;
using Elevator.Application.Common;
using Elevator.Domain;
using Moq;

namespace UnitTests;

public class MoveElevatorCommandTests
{
    [Fact]
    public async Task Handle_ShouldUpdateElevatorAndReturnIt()
    {
        // Arrange
        var elevatorId = Guid.NewGuid();
        var requestedFloor = 5;
        var passengersCount = 3;

        var elevator = new ElevatorEntity
        {
            Id = elevatorId,
            CurrentFloor = 2,
            IsAvailable = true,
            CurrentPassengerCount = 0
        };

        // Create a mock for the repository
        var repositoryMock = new Mock<IRepository>();
        

        repositoryMock.Setup(repo => repo.GetElevatorById(elevatorId)).ReturnsAsync(elevator);

        var command = new MoveElevatorCommand
        {
            ElevatorId = elevatorId,
            RequestedFloor = requestedFloor,
            PassengersCount = passengersCount
        };
        var handler = new MoveElevatorCommandHandler(repositoryMock.Object);

        // Act
        var result = await handler.Handle(command, new CancellationToken());

        // Assert
        repositoryMock.Verify(repo => repo.GetElevatorById(elevatorId), Times.Once);

        Assert.False(elevator.IsAvailable);
        Assert.Equal(requestedFloor, elevator.CurrentFloor);
        Assert.Equal(passengersCount, elevator.CurrentPassengerCount);

        repositoryMock.Verify(repo => repo.Update(elevator), Times.Once);

        Assert.Equal(elevator, result);
    }
}
