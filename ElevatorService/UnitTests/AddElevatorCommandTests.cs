using Elevator.Application.Commands.AddElevator;
using Elevator.Application.Common;
using Elevator.Domain;
using Moq;

namespace UnitTests;
public class AddElevatorCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldAddElevatorAndReturnId()
    {
        // Arrange
        var command = new AddElevatorCommand
        {
            Name = "Elevator 1",
            Capacity = 10
        };
        var cancellationToken = new CancellationToken();
        var repositoryMock = new Mock<IRepository>();

        var handler = new AddElevatorCommandHandler(repositoryMock.Object);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        repositoryMock.Verify(repo => repo.Add(It.Is<ElevatorEntity>(elevator =>
            elevator.Name == command.Name &&
            elevator.Capacity == command.Capacity)), Times.Once);
        
        Assert.Equal(result, command.Name);
    }
}