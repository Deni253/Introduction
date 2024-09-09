using Introduction.Model;


namespace Introduction.Repository.Common
{
    public interface IDogOwnerRepository
    {
        Task<bool> Post(DogOwner owner);
        Task<bool> Delete(Guid id);
        Task<bool> Update(Guid id,DogOwner owner);
        Task<DogOwner> Get(Guid id);
    }
}
