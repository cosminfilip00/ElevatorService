namespace Elevator.Application.Common.Exceptions;

public class InvalidPassengerNumberException : Exception
{
    public InvalidPassengerNumberException(int passengerNumber) : base($"{passengerNumber} is an invalid passenger number.") 
    {
        
    }
}
