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
        public IActionResult PostDogOwner(DogOwner dogOwner)
        {
            DogOwnerService service = new DogOwnerService();
            var isSuccessful = service.PostDogOwner(dogOwner);
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
        public IActionResult DeleteDogOwner(Guid id)
        {
            DogOwnerService service = new DogOwnerService();
            var isSuccessful = service.DeleteDogOwner(id);
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
        public IActionResult GetDogOwner(Guid id)
        {
            DogOwnerService service = new DogOwnerService();
            var isSuccessful = service.GetDogOwner(id);//isSuccessful je ovdje objekt za razliku od ovih ostalih gdje je bool
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
        public IActionResult UpdateDogOwner(Guid id,DogOwner dogOwner)
        {
            DogOwnerService service = new DogOwnerService();
            var isSuccessful = service.UpdateDogOwner(id,dogOwner);
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
