using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class DogOwnerService:IDogOwnerService
    {
        public bool PostDogOwner(DogOwner dogOwner)
        {
            DogOwnerRepository repository = new DogOwnerRepository();
            return repository.Post(dogOwner);
        }
        public bool DeleteDogOwner(Guid id)
        {
            DogOwnerRepository repository = new DogOwnerRepository();
            return repository.Delete(id);
        }
        public DogOwner GetDogOwner(Guid id)
        {
            DogOwnerRepository repository = new DogOwnerRepository();
            return repository.Get(id);
        }

        public bool UpdateDogOwner(Guid id,DogOwner dog)
        {
            DogOwnerRepository repository = new DogOwnerRepository();
            return repository.Update(id,dog);
        }
    }
}

