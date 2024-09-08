using Introduction.Model;
using Introduction.Repository;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class DogService:IDogService
    {
        public bool PostDog(Dog dog)
        {
            DogRepository repository = new DogRepository();
            return repository.Post(dog);
        }
        public bool DeleteDog(Guid id)
        {
            DogRepository repository = new DogRepository();
            return repository.Delete(id);
        }
        public Dog GetDog(Guid id)
        {
            DogRepository repository = new DogRepository();
            return repository.Get(id);
        }

        public bool UpdateDog(Guid id)
        {
            DogRepository repository = new DogRepository();
            return repository.Update(id);
        }
    }
}