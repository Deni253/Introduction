using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IDogRepository
    {
        Task<bool> Post(Dog dog);

        Task<bool> Delete(Guid id);

        Task<bool> Update(Guid id);

        Task<Dog> Get(Guid id);

        Task<List<Dog>> GetAll();
    }
}