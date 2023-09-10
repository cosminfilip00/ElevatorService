namespace Elevator.Domain;
public interface IControl
{
    void Call(int floorNumber);
    void IndicatePassengers(int floorNumber, int passengerCount);
}
