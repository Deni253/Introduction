using Introduction.Model;
using Introduction.Service;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.Repository.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DogOwnerController : ControllerBase
    {
        

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> PostDogOwner(DogOwner dogOwner)
        {
            DogOwnerService service = new DogOwnerService();
            var isSuccessful = await service.PostDogOwner(dogOwner);
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
            DogOwnerService service = new DogOwnerService();
            var isSuccessful = await service.DeleteDogOwner(id);
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
            DogOwnerService service = new DogOwnerService();
            var isSuccessful = await service.GetDogOwner(id);//isSuccessful je ovdje objekt za razliku od ovih ostalih gdje je bool
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
            DogOwnerService service = new DogOwnerService();
            bool isSuccessful = await service.UpdateDogOwner(id,dogOwner);
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
