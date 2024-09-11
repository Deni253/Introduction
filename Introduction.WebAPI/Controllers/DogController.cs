﻿using Introduction.Common;
using Introduction.Model;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Repository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogController : ControllerBase
    {
        private IDogService _service;

        public DogController(IDogService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> PostDog(Dog dog)
        {
            var isSuccessful = await _service.PostDog(dog);
            if (isSuccessful == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpDelete]
        [Route("del/{id}")]
        public async Task<IActionResult> DeleteDog(Guid id)
        {
            var isSuccessful = await _service.DeleteDog(id);
            if (isSuccessful == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }

        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> GetDog(Guid id)
        {
            var isSuccessful = await _service.GetDog(id);//isSuccessful je ovdje objekt za razliku od ovih ostalih gdje je bool
            if (isSuccessful == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(isSuccessful);
            }
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAll(Guid id, string? Name, bool? isTrained, int age, string? breed)
        {
            DogFilter filter = new DogFilter();
            Sorting sorting = new Sorting();
            filter.Id = id;
            filter.Name = Name;
            filter.IsTrained = isTrained;
            filter.Age = age;
            filter.Breed = breed;

            var isSuccessful = await _service.GetAll(filter,sorting);
            if (isSuccessful == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(isSuccessful);
            }
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateDog(Guid id)
        {
            var isSuccessful = await _service.UpdateDog(id);
            if (isSuccessful == false)
            {
                return BadRequest();
            }
            else
            {
                return Ok();
            }
        }
    }
}