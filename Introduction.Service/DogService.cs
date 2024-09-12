using Introduction.Common;
using Introduction.Model;
using Introduction.Repository.Common;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class DogService : IDogService
    {
        private IDogRepository _repository;

        public DogService(IDogRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> PostDogSync(Dog dog)
        {
            return await _repository.PostSync(dog);
        }

        public async Task<bool> DeleteDogSync(Guid id)
        {
            return await _repository.DeleteSync(id);
        }

        public async Task<Dog> GetDogSync(Guid id)
        {
            return await _repository.GetSync(id);
        }

        public async Task<List<Dog>> GetAllSync(DogFilter filter,Sorting sorting,Paging paging)
        {
            return await _repository.GetAllSync(filter,sorting,paging);
        }

        public async Task<bool> UpdateDogSync(Guid id)
        {
            return await _repository.UpdateSync(id);
        }
    }
}