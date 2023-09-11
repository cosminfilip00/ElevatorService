using Elevator.Application.Common;
using Elevator.Domain;
using MediatR;

namespace Elevator.Application.Commands.SelectElevator;

public class SelectElevatorCommand : IRequest<ElevatorEntity>
{
    public int RequestedFloor { get; set; }
    public int PassengersCount { get; set; }
}

public class SelectElevatorCommandHandler : IRequestHandler<SelectElevatorCommand, ElevatorEntity>
{
    private readonly IRepository _repository;

    public SelectElevatorCommandHandler(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ElevatorEntity> Handle(SelectElevatorCommand command, CancellationToken cancellationToken)
    {
        var allElevator = await _repository.GetAll();

        var elevatorsWithCapacity = allElevator.Where(e=> e.HasCapacityToLoadPassengers(command.PassengersCount));

        //Order By is using quick sort algorithm
        var closestElevator = elevatorsWithCapacity.OrderBy(e => e.CalculateDistanceToRequestedFloor(command.RequestedFloor)).FirstOrDefault();

        return closestElevator!;
    }
}
