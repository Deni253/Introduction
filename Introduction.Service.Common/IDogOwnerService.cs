using Introduction.Model;

namespace Introduction.Service.Common
{
    public interface IDogOwnerService
    {
        bool DeleteDogOwner(Guid id);
        bool PostDogOwner(DogOwner dogOwner);
        DogOwner GetDogOwner(Guid id);
        bool UpdateDogOwner(Guid id,DogOwner dog);

    }
}
