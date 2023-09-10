using Elevator.Domain;

namespace Elevator.Application.Common
{
    public interface IRepository
    {
        Task<List<ElevatorEntity>> GetAll();

        ElevatorEntity GetElevatorById(Guid id);
        
        Task Add(ElevatorEntity elevator);
        
        void Remove(Guid id);

        void Update(ElevatorEntity elevator);
    }
}
