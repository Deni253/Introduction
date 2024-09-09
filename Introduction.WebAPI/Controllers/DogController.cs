using Introduction.Model;
using Introduction.Service;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace Introduction.Repository.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DogController : ControllerBase
    {
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> PostDog(Dog dog)
        {
            DogService service = new DogService();
            var isSuccessful = await service.PostDog(dog);
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
            DogService service = new DogService();
            var isSuccessful = await service.DeleteDog(id);
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
            DogService service = new DogService();
            var isSuccessful =  await service.GetDog(id);//isSuccessful je ovdje objekt za razliku od ovih ostalih gdje je bool
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
            DogService service = new DogService();
            var isSuccessful = await service.UpdateDog(id);
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