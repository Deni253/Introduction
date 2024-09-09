using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class DogService:IDogService
    {
        public async Task<bool> PostDog(Dog dog)
        {
            DogRepository repository = new DogRepository();
            return await repository.Post(dog);
        }
        public async Task<bool> DeleteDog(Guid id)
        {
            DogRepository repository = new DogRepository();
            return await repository.Delete(id);
        }
        public async Task<Dog> GetDog(Guid id)
        {
            DogRepository repository = new DogRepository();
            return await repository.Get(id);
        }

        public async Task<bool> UpdateDog(Guid id)
        {
            DogRepository repository = new DogRepository();
            return await repository.Update(id);
        }
    }
}