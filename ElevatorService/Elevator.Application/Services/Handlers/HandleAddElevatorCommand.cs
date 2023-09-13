using Elevator.Application.Commands.AddElevator;
using MediatR;

namespace Elevator.Application.Services.Handlers;

public class HandleAddElevatorCommand : ICommandHandler
{
    private readonly IMediator _mediator;

    public HandleAddElevatorCommand(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task HandleCommandAsync()
    {
        var command = new AddElevatorCommand();

        Console.WriteLine("Insert elevator name:");

        var name = Console.ReadLine();

        if (string.IsNullOrEmpty(name))
        {
            Console.WriteLine("Invalid name. Please select a command:");
            return;
        }

        command.Name = name;

        Console.WriteLine("Insert elevator capacity:");

        if (!int.TryParse(Console.ReadLine(), out int capacity))
        {
            Console.WriteLine("Invalid capacity. Please select a command:");
            return;
        }

        command.Capacity = capacity;

        var elevatorName = await _mediator.Send(command);

        Console.WriteLine($"Elevator {elevatorName} was created.");

        Console.WriteLine("Please select a command:");
    }
}
