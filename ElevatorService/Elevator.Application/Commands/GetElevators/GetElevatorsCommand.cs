using Elevator.Application.Common;
using Elevator.Domain;
using MediatR;

namespace Elevator.Application.Commands.GetElevators
{
    public class GetElevatorsCommand : IRequest<List<ElevatorEntity>>
    {
    }

    public class GetElevatorsCommandHandler : IRequestHandler<GetElevatorsCommand, List<ElevatorEntity>>
    {
        private readonly IRepository _repository;

        public GetElevatorsCommandHandler(IRepository repository)
        {
            _repository = repository;

        }

        public async Task<List<ElevatorEntity>> Handle(GetElevatorsCommand request, CancellationToken cancellationToken)
        {

            var existingElevators = await _repository.GetAll();

            return existingElevators;
        }
    }
}
