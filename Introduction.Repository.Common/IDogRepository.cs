using Introduction.Common;
using Introduction.Model;

namespace Introduction.Repository.Common
{
    public interface IDogRepository
    {
        Task<bool> PostSync(Dog dog);

        Task<bool> DeleteSync(Guid id);

        Task<bool> UpdateSync(Guid id);

        Task<Dog> GetSync(Guid id);

        Task<List<Dog>> GetAllSync(DogFilter filter, Sorting sorting, Paging paging);
    }
}