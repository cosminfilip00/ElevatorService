using Elevator.Application.Common;
using Elevator.Application.Common.Exceptions;
using Elevator.Domain;
using MediatR;

namespace Elevator.Application.Commands.SelectElevator
{
    public class SelectBestElevatorCommand : IRequest<ElevatorEntity>
    {
        public int RequestedFloor { get; set; }
        public int PassengersCount { get; set; }
    }

    public class SelectBestElevatorCommandHandler : IRequestHandler<SelectBestElevatorCommand, ElevatorEntity>
    {
        private readonly IRepository _repository;

        public SelectBestElevatorCommandHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<ElevatorEntity> Handle(SelectBestElevatorCommand command, CancellationToken cancellationToken)
        {
                var allElevators = await _repository.GetAll();

                var closestElevator = Sort(allElevators.ToArray(), command.RequestedFloor, command.PassengersCount).FirstOrDefault(); ;

                return closestElevator!;
        }

        private ElevatorEntity[] Sort(ElevatorEntity[] elevators, int requestedFloor, int passengerCount)
        {
            ElevatorEntity[] left;
            ElevatorEntity[] right;

            var totalNumberOfElevators = elevators.Length;

            ElevatorEntity[] result = new ElevatorEntity[totalNumberOfElevators];
            if (totalNumberOfElevators <= 1)
                return elevators;

            int midPoint = totalNumberOfElevators / 2;
            left = new ElevatorEntity[midPoint];

            if (totalNumberOfElevators % 2 == 0)
                right = new ElevatorEntity[midPoint];
            else
                right = new ElevatorEntity[midPoint + 1];
            for (int i = 0; i < midPoint; i++)
                left[i] = elevators[i];
            
            int x = 0;
            for (int i = midPoint; i < totalNumberOfElevators; i++)
            {
                right[x] = elevators[i];
                x++;
            }
            
            left = Sort(left, requestedFloor, passengerCount);
            right = Sort(right, requestedFloor, passengerCount);
            
            result = Merge(left, right, requestedFloor, passengerCount);
            return result;
        }

        private ElevatorEntity[] Merge(ElevatorEntity[] left, ElevatorEntity[] right, int requestedFloor, int passengerCount)
        {
            int resultLength = right.Length + left.Length;
            ElevatorEntity[] result = new ElevatorEntity[resultLength];

            int indexLeft = 0, indexRight = 0, indexResult = 0;
            
            while (indexLeft < left.Length || indexRight < right.Length)
            {
                if (indexLeft < left.Length && indexRight < right.Length)
                {
                    var leftScore = left[indexLeft].CalculateMatchScore(requestedFloor, passengerCount);
                    var rightScore = right[indexRight].CalculateMatchScore(requestedFloor, passengerCount);
                    if (leftScore >= rightScore)
                    {
                        result[indexResult] = left[indexLeft];
                        indexLeft++;
                        indexResult++;
                    }
                    else
                    {
                        result[indexResult] = right[indexRight];
                        indexRight++;
                        indexResult++;
                    }
                }
                else if (indexLeft < left.Length)
                {
                    result[indexResult] = left[indexLeft];
                    indexLeft++;
                    indexResult++;
                }
                else if (indexRight < right.Length)
                {
                    result[indexResult] = right[indexRight];
                    indexRight++;
                    indexResult++;
                }
            }
         
            return result;
        }
    }
}
