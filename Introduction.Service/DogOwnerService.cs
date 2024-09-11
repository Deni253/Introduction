using Introduction.Common;
using Introduction.Model;
using Introduction.Repository.Common;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class DogOwnerService : IDogOwnerService
    {
        private IDogOwnerRepository _repository;

        public DogOwnerService(IDogOwnerRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> PostDogOwner(DogOwnerFilter ownerFilter)
        {
            return await _repository.Post(ownerFilter);
        }

        public async Task<bool> DeleteDogOwner(DogOwnerFilter ownerFilter)
        {
            return await _repository.Delete(ownerFilter);
        }

        public async Task<DogOwner> GetDogOwner(Guid id)
        {
            return await _repository.Get(id);
        }

        public async Task<List<DogOwner>> GetAll(DogOwnerFilter ownerFilter)
        {
            return await _repository.GetAll(ownerFilter);
        }

        public async Task<bool> UpdateDogOwner(Guid id, DogOwner dog)
        {
            return await _repository.Update(id, dog);
        }
    }
}