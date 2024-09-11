using Introduction.Common;
using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IDogOwnerService
    {
        Task<bool> DeleteDogOwner(DogOwnerFilter ownerFilter);

        Task<bool> PostDogOwner(DogOwnerFilter ownerFilter);

        Task<DogOwner> GetDogOwner(Guid id);

        //Task<List<DogOwner>> GetAll();
        Task<List<DogOwner>> GetAll(DogOwnerFilter ownerFilter);

        Task<bool> UpdateDogOwner(Guid id, DogOwner dog);
    }
}