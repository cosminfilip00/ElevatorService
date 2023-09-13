namespace Elevator.Application.Services;

public interface ICommandHandler
{
    Task HandleCommandAsync();
}
