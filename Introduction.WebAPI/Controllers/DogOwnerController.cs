using Introduction.Model;
using Introduction.Service;
using Introduction.Service.Common;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

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
        public async Task<IActionResult> PostDogOwner(DogOwner dogOwner)
        {
            var isSuccessful = await _service.PostDogOwner(dogOwner);
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
        public async Task<IActionResult> DeleteDogOwner(Guid id)
        {
            var isSuccessful = await _service.DeleteDogOwner(id);
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

        


        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateDogOwner(Guid id,DogOwner dogOwner)
        {
            bool isSuccessful = await _service.UpdateDogOwner(id,dogOwner);
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
