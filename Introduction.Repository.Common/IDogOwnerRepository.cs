using Introduction.Common;
using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IDogOwnerRepository
    {
        Task<bool> PostSync(DogOwner owner);

        Task<bool> DeleteSync(Guid id, DogOwnerFilter ownerFilter);

        Task<bool> UpdateSync(Guid id, DogOwner owner);

        Task<DogOwner> GetSync(Guid id);

        Task<List<DogOwner>> GetAllSync(DogOwnerFilter ownerFilter,Sorting sorting,Paging paging);
    }
}