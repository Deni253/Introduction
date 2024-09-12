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

        public async Task<bool> PostDogOwnerSync(DogOwner owner)
        {
            return await _repository.PostSync(owner);
        }

        public async Task<bool> DeleteDogOwnerSync(Guid id,DogOwnerFilter filter)
        {
            return await _repository.DeleteSync(id,filter);
        }

        public async Task<DogOwner> GetDogOwnerSync(Guid id)
        {
            return await _repository.GetSync(id);
        }

        public async Task<List<DogOwner>> GetAllSync(DogOwnerFilter ownerFilter,Sorting sorting,Paging paging)
        {
            return await _repository.GetAllSync(ownerFilter,sorting,paging);
        }

        public async Task<bool> UpdateDogOwnerSync(Guid id, DogOwner dog)
        {
            return await _repository.UpdateSync(id, dog);
        }
    }
}