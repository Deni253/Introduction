using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IDogService
    {
        Task<bool> DeleteDog(Guid id);
        Task<bool> PostDog(Dog dog);
        Task<Dog> GetDog(Guid id);

        Task<Dog> GetAll(Guid id);
        Task<bool> UpdateDog(Guid id);
    }
}
