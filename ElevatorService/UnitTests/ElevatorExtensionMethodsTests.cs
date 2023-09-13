using Elevator.Application.Common;
using Elevator.Application.Common.Exceptions;
using Elevator.Domain;

namespace UnitTests;

public class ElevatorExtensionMethodsTests
{
    [Theory]
    [InlineData(3, 1, 1, 0)]
    [InlineData(10,2,4, 0.2)]
    public void CalculateDifferenceToMaxCapacity_ShouldCalculateCorrectDifference(int capacity, int currentPassengerCount, int passengerCount, double expected)
    {
        // Arrange
        var elevator = new ElevatorEntity { CurrentPassengerCount = currentPassengerCount, Capacity = capacity };

        // Act
        double difference = elevator.CalculateDifferenceToMaxCapacity(passengerCount);

        // Assert
        Assert.Equal(expected, difference);
    }


    [Fact]
    public void CalculateDifferenceToMaxCapacity_ShouldThrowExceptionWhenZeroPassengerCount()
    {
        // Arrange
        var elevator = new ElevatorEntity { CurrentPassengerCount = 0, Capacity = 10 };
        int passengerCount = 0;

        // Act & Assert
        Assert.Throws<InvalidPassengerNumberException>(() => elevator.CalculateDifferenceToMaxCapacity(passengerCount));
    }
}