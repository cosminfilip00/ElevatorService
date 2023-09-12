namespace Elevator.Domain;

public class ElevatorEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Capacity { get; set; } = 5;
    public int CurrentFloor { get; set; } = 1;
    public int CurrentPassengerCount { get; set; }
    public bool IsAvailable { get; set; } = true;
}
