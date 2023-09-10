using Elevator.Application.Common;
using Elevator.Domain;

namespace Elevator.Infrastructure
{
    public class Repository : IRepository
    {
        private List<ElevatorEntity> _elevators = new List<ElevatorEntity> { };

        public Repository()
        {
        }

        public Task Add(ElevatorEntity elevator)
        {
            _elevators.Add(elevator);
            
            return Task.CompletedTask;
        }

        public Task<List<ElevatorEntity>> GetAll()
        {
            return Task.FromResult(_elevators);
        }

        public ElevatorEntity GetElevatorById(Guid id)
        {
            var existingElevator = _elevators.FirstOrDefault(e => e.Id == id);

            if (existingElevator == null) 
            {
                throw new InvalidOperationException();
            }

            return existingElevator;
        }

        public void Remove(Guid id)
        {
            foreach (var elevator in _elevators)
            {
                if (elevator.Id == id)
                {
                    _elevators.Remove(elevator);
                }
            }
        }

        public void Update(ElevatorEntity elevator)
        {
            var existingElevator = GetElevatorById(elevator.Id);
         
            existingElevator.Name = elevator.Name;
            existingElevator.Capacity = elevator.Capacity;
        }
    }
}