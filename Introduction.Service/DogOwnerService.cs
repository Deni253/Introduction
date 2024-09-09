using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class DogOwnerService:IDogOwnerService
    {
        public async Task<bool> PostDogOwner(DogOwner dogOwner)
        {
            DogOwnerRepository repository = new DogOwnerRepository();
            return await repository.Post(dogOwner);
        }
        public async Task<bool> DeleteDogOwner(Guid id)
        {
            DogOwnerRepository repository = new DogOwnerRepository();
            return await repository.Delete(id);
        }
        public async Task<DogOwner> GetDogOwner(Guid id)
        {
            DogOwnerRepository repository = new DogOwnerRepository();
            return await repository.Get(id);
        }

        public async Task<bool> UpdateDogOwner(Guid id,DogOwner dog)
        {
            DogOwnerRepository repository = new DogOwnerRepository();
            return await repository.Update(id,dog);
        }
    }
}

