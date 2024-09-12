using Introduction.Common;
using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IDogService
    {
        Task<bool> DeleteDogSync(Guid id);

        Task<bool> PostDogSync(Dog dog);

        Task<Dog> GetDogSync(Guid id);

        Task<List<Dog>> GetAllSync(DogFilter filter,Sorting sorting, Paging paging);

        Task<bool> UpdateDogSync(Guid id);
    }
}