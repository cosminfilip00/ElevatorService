using Elevator.Application.Common.Exceptions;
using Elevator.Domain;

namespace Elevator.Application.Common;

public static class ElevatorExtensionMethods
{
    public static int CalculateDistanceToRequestedFloor(this ElevatorEntity elevator, int requestedFloor)
    {
        if (requestedFloor < Constants.FLOOR_MIN_NUMBER || requestedFloor > Constants.FLOOR_MAX_NUMBER)
            throw new InvalidFloorNumberException(requestedFloor);
        return Math.Abs(elevator.CurrentFloor - requestedFloor);
    }

    public static bool HasCapacityToLoadPassengers(this ElevatorEntity elevator, int passengersCount)
    {
        return 
            elevator.IsAvailable && 
            passengersCount > 0 && 
            elevator.CurrentPassengerCount + passengersCount < elevator.Capacity ;
    }

    public static double CalculateDifferenceToMaxCapacity(this ElevatorEntity elevator, int passengerCount)
    {
        //I've added here the custom exception, just as an example of how this will be thrown.
        //But in this case, this is not catch by the upper handlers because the input is validated in the console app. Validate input and fail fast. 
        if (passengerCount < Constants.PASSENGER_MIN_NUMBER || passengerCount > Constants.PASSENGER_MAX_NUMBER)
            throw new InvalidPassengerNumberException(passengerCount);

          return (double)Math.Abs(elevator.CurrentPassengerCount - passengerCount) / (double)elevator.Capacity;
    }

    public static double CalculateMatchScore(this ElevatorEntity elevator, int requestedFloor, int passengersCount)
    {

        return elevator.CalculateDistanceToRequestedFloor(requestedFloor) + elevator.CalculateDifferenceToMaxCapacity(passengersCount);
    }
}
