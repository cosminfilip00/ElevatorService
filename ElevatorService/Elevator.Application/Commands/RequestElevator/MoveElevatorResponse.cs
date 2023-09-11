using Elevator.Application.Common;
using Elevator.Domain;

namespace Elevator.Application.Commands.RequestElevator;

public class MoveElevatorResponse : BaseResponse
{
    public ElevatorEntity Elevator { get; set; }
}
