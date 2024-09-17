using Introduction.Common;
using Introduction.Model;
using Introduction.Service.Common;
using Introduction.WebAPI.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Introduction.Repository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogOwnerController : ControllerBase
    {
        private IDogOwnerService _service;
        private readonly IConfiguration _config;

        public DogOwnerController(IDogOwnerService service, IConfiguration config)
        {
            _service = service;
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> PostDogOwnerSync(DogOwner dogOwner)
        {
            var isSuccessful = await _service.PostDogOwnerSync(dogOwner);
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
        public async Task<IActionResult> DeleteDogOwnerSync(Guid id,string? firstName)
        {
            DogOwnerFilter ownerfilter = new DogOwnerFilter();
            ownerfilter.FirstName = firstName;
            var isSuccessful = await _service.DeleteDogOwnerSync(id,ownerfilter);
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
        public async Task<IActionResult> GetDogOwnerSync(Guid id)
        {
            var isSuccessful = await _service.GetDogOwnerSync(id);//isSuccessful je ovdje objekt za razliku od ovih ostalih gdje je bool
            if (isSuccessful == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(isSuccessful);
            }
        }

        [Authorize]
        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAllSync(Guid? Id, string? firstName, string? lastName, string? phoneNumber, string? Email,string orderBy="",string sortDirection="", int pageNumber=6,int pageSize=1)
        {
            DogOwnerFilter filter = new DogOwnerFilter();// Modeli nisu skupi to može bit problem je ako imamo service s puno drugih stvari tako da ova instanca nije skupa i ne predstavlja problem
            Sorting sorting = new Sorting();
            Paging paging = new Paging();

            filter.FirstName = firstName;
            filter.LastName = lastName;
            filter.PhoneNumber = phoneNumber;
            filter.Email = Email;

            sorting.OrderBy = orderBy;
            sorting.SortDirection = sortDirection;

            paging.PageNumber = pageNumber;
            paging.PageSize = pageSize;

            var dogs = await _service.GetAllSync(filter,sorting, paging);//isSuccessful je ovdje objekt za razliku od ovih ostalih gdje je bool
            if (dogs == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(dogs);
            }
        }
        [Authorize(Policy = IdentityData.AdminUserPolicyName)]
        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateDogOwnerSync(Guid id, DogOwner dogOwner)
        {
            bool isSuccessful = await _service.UpdateDogOwnerSync(id, dogOwner);
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