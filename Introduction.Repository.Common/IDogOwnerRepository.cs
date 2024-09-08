using Introduction.Model;


namespace Introduction.Repository.Common
{
    public interface IDogOwnerRepository
    {
        bool Post(DogOwner owner);
        bool Delete(Guid id);
        bool Update(DogOwner owner);
        DogOwner Get(Guid id, DogOwner owner);
    }
}
