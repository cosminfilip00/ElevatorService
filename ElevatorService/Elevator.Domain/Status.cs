namespace Elevator.Domain;
public class Status
{
    public int CurrenFloor { get; set; }
    public Direction Direction { get; set; } = Direction.None;
    public int NumberOfPassengers { get; set; }

}