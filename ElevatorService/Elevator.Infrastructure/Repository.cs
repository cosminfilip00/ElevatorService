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

        public async Task Add(ElevatorEntity elevator)
        {
            _elevators.Add(elevator);
            
            //This delay simulate a database trip
            await Task.Delay(500);
        }

        public async Task<List<ElevatorEntity>> GetAll()
        {
            await Task.Delay(200);
            
            return _elevators;
        }

        public async Task<ElevatorEntity> GetElevatorById(Guid id)
        {
            var existingElevator = _elevators.FirstOrDefault(e => e.Id == id);

            if (existingElevator is null) 
            {
                throw new InvalidOperationException();
            }

            await Task.Delay(500);

            return existingElevator;
        }

        public async Task Remove(Guid id)
        {
            foreach (var elevator in _elevators)
            {
                if (elevator.Id == id)
                {
                    _elevators.Remove(elevator);
                }
            }

            await Task.Delay(500);
        }

        public async Task Update(ElevatorEntity elevator)
        {
            var existingElevator = await GetElevatorById(elevator.Id);
         
            existingElevator.Name = elevator.Name;
            existingElevator.Capacity = elevator.Capacity;
            existingElevator.CurrentPassengerCount = elevator.CurrentPassengerCount;
            existingElevator.CurrentFloor = elevator.CurrentFloor;
            existingElevator.IsAvailable = elevator.IsAvailable;

            await Task.Delay(500);
        }
    }
}