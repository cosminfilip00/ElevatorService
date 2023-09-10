using Elevator.Application.Commands.AddElevator;
using Elevator.Application.Commands.GetElevators;
using Elevator.Application.Commands.RequestElevator;
using Elevator.Application.Common;
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

        while (isRunning)
        {
            Console.WriteLine("Please select a command:");
            var command = Console.ReadLine();

            //TODO: handle this validation later with an custom exception handler 
            if (command.Equals(Constants.EXIT_COMMAND))
            {
                isRunning = false;
                Console.WriteLine("You exit the app succesfully.");
            }
            else
            {
                switch (int.Parse(command))
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

    private static void CallElevator()
    {
        //TODO: Add validations using FluentValidation
        Console.WriteLine("Insert floor:");
        var floorNumber = int.Parse(Console.ReadLine());

        Console.WriteLine("Insert passenger number:");
        var passengerNumber = int.Parse(Console.ReadLine());

        var command = new RequestElevatorCommand
        {
            FloorNumber = floorNumber,
            PassengerNumber = passengerNumber
        };

        var requestedElevator = _mediator.Send(command).Result;

        Console.WriteLine($"Elevator {requestedElevator.Name} : {requestedElevator.Id} with capacity of {requestedElevator.Capacity} was requested.");
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

        _mediator.Send(command);

        Console.WriteLine($"Elevator {command.Name} was created.");
    }

    private static void GetAllElevators()
    {
        var command = new GetElevatorsCommand();

        var existingElevators = _mediator.Send(command).Result;

        Console.WriteLine($"Currently, there are {existingElevators.Count} in use.");
        foreach (var elevator in existingElevators)
        {
            Console.WriteLine($"{elevator.Name} : {elevator.Id} : {elevator.Capacity}");
        }
    }

    public static void CreateConfig()
    {
        var services = new ServiceCollection();
        
        services.AddConsoleServices();
        services.AddInfrastructureServices();

        _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
    }
}







