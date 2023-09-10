namespace Elevator.Application.Commands.RequestElevator
{
    public class RequestElevatorResponse
    {
        public string Name { get; set; } = string.Empty;
        public Guid Id { get;}
        public int Capacity { get; }
    }
}
