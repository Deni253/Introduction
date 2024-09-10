﻿using Introduction.Model;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service.Common;

namespace Introduction.Service
{
    public class DogService:IDogService
    {
        private IDogRepository _repository;
        public DogService(IDogRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> PostDog(Dog dog)
        {            
            return await _repository.Post(dog);
        }
        public async Task<bool> DeleteDog(Guid id)
        {
            
            return await _repository.Delete(id);
        }
        public async Task<Dog> GetDog(Guid id)
        {           
            return await _repository.Get(id);
        }

        public async Task<Dog> GetAll(Guid id)
        {
            return await _repository.GetAll(id);
        }

        public async Task<bool> UpdateDog(Guid id)
        {
            
            return await _repository.Update(id);
        }
    }
}