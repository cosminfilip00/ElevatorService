using Elevator.Application.Commands.GetElevators;
using Elevator.Application.Common;
using MediatR;

namespace Elevator.Application.Services.Handlers;

public class HandleGetElevatorsCommand : ICommandHandler
{
    private readonly IMediator _mediator;

    public HandleGetElevatorsCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task HandleCommandAsync()
    {
        var command = new GetElevatorsCommand();

        var existingElevators = await _mediator.Send(command);

        Console.WriteLine($"Currently, there are {existingElevators.Count} in use.");

        foreach (var elevator in existingElevators)
        {
            Console.WriteLine($"Elevator {elevator.Name} " +
                $"with capacity of {elevator.Capacity} " +
                $"at {elevator.CurrentFloor.DisplayOrdinal()} " +
                $"floor with {elevator.CurrentPassengerCount} passengers.");
        }

        Console.WriteLine("Please select a command:");

    }
}
