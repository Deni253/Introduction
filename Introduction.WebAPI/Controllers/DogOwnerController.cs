using Introduction.Common;
using Introduction.Model;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Introduction.Repository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogOwnerController : ControllerBase
    {
        private IDogOwnerService _service;

        public DogOwnerController(IDogOwnerService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> PostDogOwner(Guid? Id, string? firstName, string? lastName, string? phoneNumber, string? Email)
        {
            DogOwnerFilter filter = new DogOwnerFilter();
            filter.FirstName = firstName;
            filter.LastName = lastName;
            filter.PhoneNumber = phoneNumber;
            filter.Email = Email;

            /*
            Paging paging = new Paging();
            paging.PageSize = 10;

            Sorting sorting = new Sorting();
            */

            var isSuccessful = await _service.PostDogOwner(filter);
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
        public async Task<IActionResult> DeleteDogOwner(Guid? Id, string? firstName, string? lastName, string? phoneNumber, string? Email)
        {
            DogOwnerFilter filter = new DogOwnerFilter();
            filter.FirstName = firstName;
            filter.LastName = lastName;
            filter.PhoneNumber = phoneNumber;
            filter.Email = Email;

            var isSuccessful = await _service.DeleteDogOwner(filter);
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
        public async Task<IActionResult> GetDogOwner(Guid id)
        {
            var isSuccessful = await _service.GetDogOwner(id);//isSuccessful je ovdje objekt za razliku od ovih ostalih gdje je bool
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
        public async Task<IActionResult> GetAll(Guid? Id, string? firstName, string? lastName, string? phoneNumber, string? Email)
        {
            DogOwnerFilter filter = new DogOwnerFilter();// Modeli nisu skupi to može bit problem je ako imamo service s puno drugih stvari tako da ova instanca nije skupa i ne predstavlja problem

            filter.FirstName = firstName;
            filter.LastName = lastName;
            filter.PhoneNumber = phoneNumber;
            filter.Email = Email;

            var isSuccessful = await _service.GetAll(filter);//isSuccessful je ovdje objekt za razliku od ovih ostalih gdje je bool
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
        public async Task<IActionResult> UpdateDogOwner(Guid id, DogOwner dogOwner)
        {
            bool isSuccessful = await _service.UpdateDogOwner(id, dogOwner);
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