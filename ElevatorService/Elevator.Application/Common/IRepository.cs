using Elevator.Domain;

namespace Elevator.Application.Common
{
    public interface IRepository
    {
        Task<List<ElevatorEntity>> GetAll();

        Task<ElevatorEntity> GetElevatorById(Guid id);
        
        Task Add(ElevatorEntity elevator);
        
        Task Remove(Guid id);

        Task Update(ElevatorEntity elevator);
    }
}
