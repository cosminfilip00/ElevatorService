using Elevator.Application.Commands.MoveElevator;
using Elevator.Application.Commands.SelectElevator;
using Elevator.Application.Common;
using MediatR;

namespace Elevator.Application.Services.Handlers;

public class HandleMoveElevatorCommand : ICommandHandler
{
    private readonly IMediator _mediator;

    public HandleMoveElevatorCommand(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task HandleCommandAsync()
    {
        //TODO: Add validations using FluentValidation
        Console.WriteLine("Insert floor(1-100):");

        if (!int.TryParse(Console.ReadLine(), out int floorNumber) || floorNumber < 1 || floorNumber > 100)
        {
            Console.WriteLine("Invalid floor. Please select a command:");
            return;
        }

        Console.WriteLine("Insert passenger number(1-20):");
        if (!int.TryParse(Console.ReadLine(), out int passengerCount) || passengerCount < 1 || floorNumber > 20)
        {
            Console.WriteLine("Invalid passenger number. Please select a command:");
            return;
        }

        var selectBestElevatorCommand = new SelectBestElevatorCommand
        {
            RequestedFloor = floorNumber,
            PassengersCount = passengerCount
        };

        var selectedElevator = await _mediator.Send(selectBestElevatorCommand);

        if (selectedElevator is null)
        {
            Console.WriteLine("No elevator matching the requested capacity. Please add a new elevator.");
            Console.WriteLine("Please select a command:");
            return;
        }

        Console.WriteLine($"Elevator {selectedElevator.Name} is moving from floor {selectedElevator.CurrentFloor} to floor {selectBestElevatorCommand.RequestedFloor}");

        var command = new MoveElevatorCommand
        {
            ElevatorId = selectedElevator.Id,
            RequestedFloor = floorNumber,
            PassengersCount = passengerCount
        };

        var requestedElevator = await _mediator.Send(command);

        Console.WriteLine($"Elevator {requestedElevator.Name} arrived at floor {requestedElevator.CurrentFloor.DisplayOrdinal()} with {requestedElevator.CurrentPassengerCount} passengers.");

        Console.WriteLine("Please select a command:");
    }
}
