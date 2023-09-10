using MediatR;

namespace Elevator.Application.Commands.RequestElevator
{
    public class RequestElevatorCommand : IRequest<RequestElevatorResponse>
    {
        public int FloorNumber { get; set; }
        public int PassengerNumber { get; set; }
    }

    public class RequestElevatorCommandHandler : IRequestHandler<RequestElevatorCommand, RequestElevatorResponse>
    {
        public Task<RequestElevatorResponse> Handle(RequestElevatorCommand request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new RequestElevatorResponse());
        }
    }
}
