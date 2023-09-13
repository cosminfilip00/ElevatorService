using Elevator.Application.Common;
using Elevator.Application.Services.Handlers;
using MediatR;

namespace Elevator.Application.Services;

public static class CommandHandlerFactory
{
    public static ICommandHandler CreateHandler(string option, IMediator mediator)
    {
        switch (option)
        {
            case Constants.ADD_ELEVATOR_OPTION:
                return new HandleAddElevatorCommand(mediator);
            case Constants.GET_ELEVATORS_OPTION:
                return new HandleGetElevatorsCommand(mediator);
            case Constants.MOVE_ELEVATOR_OPTION:
                return new HandleMoveElevatorCommand(mediator);
            default:
                throw new ArgumentException("Invalid option", nameof(option));
        }
    }
}
