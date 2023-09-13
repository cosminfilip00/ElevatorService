namespace Elevator.Application.Common.Exceptions;

public class InvalidFloorNumberException : Exception
{
    public InvalidFloorNumberException(int floorNumber) : base($"{floorNumber} is an invalid floor number.")
    {
        
    }
}
