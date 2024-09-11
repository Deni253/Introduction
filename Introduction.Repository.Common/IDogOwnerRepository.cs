using Introduction.Common;
using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IDogOwnerRepository
    {
        Task<bool> Post(DogOwnerFilter ownerFilter);

        Task<bool> Delete(DogOwnerFilter ownerFilter);

        Task<bool> Update(Guid id, DogOwner owner);

        Task<DogOwner> Get(Guid id);

        Task<List<DogOwner>> GetAll(DogOwnerFilter ownerFilter);
    }
}