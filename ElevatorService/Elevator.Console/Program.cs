using Elevator.Application.Common;

Console.WriteLine("This is a real time elevator control app. Please type a command or 'e' for exit the app");

var isRunning = true;

while(isRunning)
{
    Console.WriteLine("Please select a command:");
    var command = Console.ReadLine();

    //TODO: handle this validation later with an custom exception handler 
    if(command.Equals(Constants.EXIT_COMMAND))
    {
        isRunning = false;
        Console.WriteLine("You exit the app succesfully.");
    }
    else
    {
        var commandTranslation = string.Empty;
        Console.WriteLine($"You want to {commandTranslation}");
    }
}


