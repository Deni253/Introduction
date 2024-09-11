using Introduction.Common;
using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IDogOwnerService
    {
        Task<bool> DeleteDogOwner(Guid id);

        Task<bool> PostDogOwner(DogOwner owner);

        Task<DogOwner> GetDogOwner(Guid id);

        //Task<List<DogOwner>> GetAll();
        Task<List<DogOwner>> GetAll(DogOwnerFilter ownerFilter);

        Task<bool> UpdateDogOwner(Guid id, DogOwner dog);
    }
}