using Introduction.Common;
using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IDogOwnerService
    {
        Task<bool> DeleteDogOwnerSync(Guid id, DogOwnerFilter ownerFilter);

        Task<bool> PostDogOwnerSync(DogOwner owner);

        Task<DogOwner> GetDogOwnerSync(Guid id);

        //Task<List<DogOwner>> GetAll();
        Task<List<DogOwner>> GetAllSync(DogOwnerFilter ownerFilter,Sorting sorting, Paging paging);

        Task<bool> UpdateDogOwnerSync(Guid id, DogOwner dog);
    }
}