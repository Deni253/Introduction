using Introduction.Model;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class DogOwnerService:IDogOwnerService
    {
        private IDogOwnerRepository _repository;
        public DogOwnerService(IDogOwnerRepository repository) 
        {
            _repository = repository;
        }
        public async Task<bool> PostDogOwner(DogOwner dogOwner)
        {
            return await _repository.Post(dogOwner);
        }
        public async Task<bool> DeleteDogOwner(Guid id)
        {
            return await _repository.Delete(id);
        }
        public async Task<DogOwner> GetDogOwner(Guid id)
        {
            return await _repository.Get(id);
        }

        public async Task<DogOwner> GetAll(Guid id)
        {
            return await _repository.Get(id);
        }

        public async Task<bool> UpdateDogOwner(Guid id,DogOwner dog)
        {
            return await _repository.Update(id,dog);
        }
    }
}

