using Elevator.Application.Commands.GetElevators;
using Elevator.Application.Common;
using Elevator.Domain;
using Moq;

namespace UnitTests;

public class GetElevatorsCommandTests
{
    [Fact]
    public async Task Handle_ShouldReturnListOfElevators()
    {
        // Arrange
        var elevators = new List<ElevatorEntity>
        {
            new ElevatorEntity { Id = Guid.NewGuid(), CurrentFloor = 1 },
            new ElevatorEntity { Id = Guid.NewGuid(), CurrentFloor = 2 }
        };
        var cancellationToken = new CancellationToken();

        var repositoryMock = new Mock<IRepository>();
        
        repositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(elevators);

        var command = new GetElevatorsCommand();
        var handler = new GetElevatorsCommandHandler(repositoryMock.Object);

        // Act
        var result = await handler.Handle(command, cancellationToken);

        // Assert
        repositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        Assert.Equal(elevators, result);
    }
}
