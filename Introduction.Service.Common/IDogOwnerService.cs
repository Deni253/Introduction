using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IDogOwnerService
    {
        Task<bool> DeleteDogOwner(Guid id);
        Task<bool> PostDogOwner(DogOwner dogOwner);
        Task<DogOwner> GetDogOwner(Guid id);
        Task<bool> UpdateDogOwner(Guid id,DogOwner dog);

    }
}
