using Introduction.Common;
using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IDogService
    {
        Task<bool> DeleteDog(Guid id);

        Task<bool> PostDog(Dog dog);

        Task<Dog> GetDog(Guid id);

        Task<List<Dog>> GetAll(DogFilter filter,Sorting sorting);

        Task<bool> UpdateDog(Guid id);
    }
}