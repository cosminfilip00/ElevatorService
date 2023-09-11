using Elevator.Domain;

namespace Elevator.Application.Common;

static class ElevatorExtensionMethods
{
    public static int CalculateDistanceToRequestedFloor(this ElevatorEntity elevator, int requestedFloor)
    {
        return Math.Abs(elevator.CurrentFloor - requestedFloor);
    }

    public static bool HasCapacityToLoadPassengers(this ElevatorEntity elevator, int passengersCount)
    {
        return 
            elevator.IsAvailable && 
            passengersCount > 0 && 
            elevator.CurrentPassengerCount + passengersCount < elevator.Capacity ;
    }
}
