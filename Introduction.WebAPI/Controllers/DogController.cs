using Introduction.Common;
using Introduction.Model;
using Introduction.Service.Common;
using Introduction.WebAPI.RestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Introduction.WebAPI.Controllers;

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
        public async Task<IActionResult> PostDogSync(Dog dog)
        {
            var isSuccessful = await _service.PostDogSync(dog);
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
        //[Route("del/{id}")]
        [Route("del")]
        public async Task<IActionResult> DeleteDogSync(Guid id)
        {
            var isSuccessful = await _service.DeleteDogSync(id);
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
        public async Task<IActionResult> GetDogSync(Guid id)
        {
            var dog = await _service.GetDogSync(id);
            
            if (dog == null)
            {
                return BadRequest();  
            }
            
            var dogRest = new DogRest
            {
                Id = dog.Id,
                Name = dog.Name,
                Age = dog.Age,
                Breed = dog.Breed,
                IsTrained = dog.IsTrained
            };
          
            return Ok(dogRest);
        }


        
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllSync(Guid id, string? Name, bool? isTrained, int age, string? breed,string orderby="Name",string sortDirection="ASC",int pageSize=10,int pageNumber= 1)
        {
            DogFilter filter = new DogFilter();
            Sorting sorting = new Sorting();
            Paging paging = new Paging();

            filter.Id = id;
            filter.Name = Name;
            filter.IsTrained = isTrained;
            filter.Age = age;
            filter.Breed = breed;


            sorting.OrderBy = orderby;
            sorting.SortDirection = sortDirection;

            paging.PageNumber= pageNumber;
            paging.PageSize = pageSize;

            var isSuccessful = await _service.GetAllSync(filter,sorting,paging);
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
        public async Task<IActionResult> UpdateDogSync(Guid id)
        {
            var isSuccessful = await _service.UpdateDogSync(id);
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