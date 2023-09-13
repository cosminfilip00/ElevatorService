using Elevator.Application.Common;
using Elevator.Application.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    private static IMediator? _mediator;
    
    public static async Task Main(string[] args)
    {
        CreateConfig();

        Console.WriteLine("This is a real time elevator control app. Please type a command or 'e' for exit the app");

        var isRunning = true;

        Console.WriteLine("Please select an option:");

        while (isRunning)
        {
            var option = Console.ReadLine();

            if(string.IsNullOrEmpty(option)) 
            {
                Console.WriteLine("Please select an option:");
                return;
            }

            if (option.ToLower().Equals(Constants.EXIT_COMMAND))
            {
                isRunning = false;
                Console.WriteLine("You exit the app succesfully.");
            }

            try
            {
                var handler = CommandHandlerFactory.CreateHandler(option, _mediator);
                await handler.HandleCommandAsync();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
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







