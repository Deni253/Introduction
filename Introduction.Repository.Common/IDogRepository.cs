using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IDogRepository
    {
        bool Post(Dog owner);
        bool Delete(Guid id);
        bool Update(Dog owner);
        DogOwner Get(Guid id);
    }
}
