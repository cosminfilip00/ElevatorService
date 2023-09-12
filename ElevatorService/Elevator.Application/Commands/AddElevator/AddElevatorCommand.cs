using Elevator.Application.Common;
using Elevator.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Elevator.Application.Commands.AddElevator
{
    public class AddElevatorCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public int Capacity { get; set; }
    }

    public class AddElevatorCommandHandler : IRequestHandler<AddElevatorCommand, Guid>
    {
        private readonly IRepository _repository;

        public AddElevatorCommandHandler(IRepository repository)
        {
            _repository = repository;

        }
        public async Task<Guid> Handle(AddElevatorCommand command, CancellationToken cancellationToken)
        {
            //TODO: improve this with AutoMapper
            var elevatorEntity = new ElevatorEntity
            {
                Id = Guid.NewGuid(),
                Name = command.Name,
                Capacity = command.Capacity
            };

            await _repository.Add(elevatorEntity);

            return elevatorEntity.Id;
        }
    }
}
