using Elevator.Application.Common;
using Elevator.Domain;
using MediatR;

namespace Elevator.Application.Commands.RequestElevator
{
    public class MoveElevatorCommand : IRequest<ElevatorEntity>
    {
        public Guid ElevatorId { get; set; }
        public int RequestedFloor { get; set; }
        public int PassengersCount { get; set; }
    }

    public class MoveElevatorCommandHandler : IRequestHandler<MoveElevatorCommand, ElevatorEntity>
    {
        private readonly IRepository _repository;
        public MoveElevatorCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ElevatorEntity> Handle(MoveElevatorCommand command, CancellationToken cancellationToken)
        {
            var elevator = await _repository.GetElevatorById(command.ElevatorId);
    
            var movingTime = elevator.CalculateDistanceToRequestedFloor(command.RequestedFloor) * 1000;

            await Task.Delay(movingTime, cancellationToken);
            
            elevator.IsAvailable= false;
            elevator.CurrentFloor = command.RequestedFloor;
            elevator.CurrentPassengerCount += command.PassengersCount;

            await _repository.Update(elevator);

            return elevator;
        }
    }
}
