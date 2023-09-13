using Elevator.Application.Commands.AddElevator;
using Elevator.Application.Commands.GetElevators;
using Elevator.Application.Commands.MoveElevator;
using Elevator.Application.Commands.SelectElevator;
using Elevator.Application.Common;
using Elevator.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static IMediator _mediator;
    
    public static void Main(string[] args)
    {
        CreateConfig();

        Console.WriteLine("This is a real time elevator control app. Please type a command or 'e' for exit the app");

        var isRunning = true;

        Console.WriteLine("Please select a command:");

        while (isRunning)
        {
            var command = Console.ReadLine();

            //TODO: handle this validation later with an custom exception handler 
            if(string.IsNullOrEmpty(command)) 
            {
                Console.WriteLine("Please select a command:");
                return;
            }

            if (command.Equals(Constants.EXIT_COMMAND))
            {
                isRunning = false;
                Console.WriteLine("You exit the app succesfully.");
            }
            
            if (int.TryParse(command, out int option))
            {
                switch (option)
                {
                    case 1:
                        AddElevator();
                        break;
                    case 2:
                        GetAllElevators();
                        break;
                    case 3:
                        CallElevator();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    private async static void CallElevator()
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

    private static void AddElevator()
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

        _mediator?.Send(command);

        Console.WriteLine($"Elevator {command.Name} was created.");

        Console.WriteLine("Please select a command:");
    }

    private async static void GetAllElevators()
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

    private async static void MoveElevator(Guid elevatorId, int floorNumber, int passengerCount)
    {
        var command1 = new MoveElevatorCommand
        {
            ElevatorId = elevatorId,
            RequestedFloor = floorNumber,
            PassengersCount = passengerCount
        };

        var requestedElevator = await _mediator.Send(command1);

        Console.WriteLine($"Elevator {requestedElevator.Name} arrived at floor {requestedElevator.CurrentFloor.DisplayOrdinal()} with {requestedElevator.CurrentPassengerCount} passengers.");

        Console.WriteLine("Please select a command:");
    }

    public static void CreateConfig()
    {
        var services = new ServiceCollection();
        
        services.AddConsoleServices();
        services.AddInfrastructureServices();

        _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
    }
}







