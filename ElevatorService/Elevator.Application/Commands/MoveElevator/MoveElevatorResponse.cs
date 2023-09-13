using Elevator.Application.Common;
using Elevator.Domain;

namespace Elevator.Application.Commands.MoveElevator;

public class MoveElevatorResponse : BaseResponse
{
    public ElevatorEntity Elevator { get; set; }
}
