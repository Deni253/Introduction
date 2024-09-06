using Microsoft.AspNetCore.Mvc;
using Npgsql;
namespace Introduction.WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DogController : ControllerBase
    {
        string connectionString="Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";   

        [HttpGet]
        [Route("get/{id}")]
        public ActionResult Get(Guid id)
        {
            try
            {
                var dog = new Dog();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Dog\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    //while(read)
                    reader.Read();


                    dog.Id = Guid.Parse(reader[0].ToString());
                    dog.DogOwnerId = Guid.Parse(reader[1].ToString());
                    dog.Name = reader[2].ToString();
                    dog.BirthDate = Convert.ToDateTime(reader[3]);
                    dog.Age = Convert.ToInt32(reader[4]);
                    dog.FurColor = reader[5].ToString();
                    dog.Breed = reader[6].ToString();
                    dog.IsTrained = Convert.ToBoolean(reader[7]);
                  
                }
                if (dog == null)
                {
                    return NotFound();
                }
                return Ok(dog);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost]
        [Route("create")]
        public IActionResult PostNewDog(Dog dog)
        {
            DogOwner owner= new DogOwner();

            try
            {
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "INSERT INTO \"Dog\"VALUES( @id, @idDogOwner, @Name, @BirthDate, @Age,@FurColor,@Breed,@IsTrained);";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id",Guid.NewGuid());
                command.Parameters.AddWithValue("@idDogOwner",dog.DogOwnerId);
                command.Parameters.AddWithValue("@Name", dog.Name);
                command.Parameters.AddWithValue("@BirthDate", dog.BirthDate);
                command.Parameters.AddWithValue("@Age", dog.Age);
                command.Parameters.AddWithValue("@FurColor", dog.FurColor);
                command.Parameters.AddWithValue("@Breed", dog.Breed);
                command.Parameters.AddWithValue("@IsTrained", dog.IsTrained);

                connection.Open();

                var numberofcommits = command.ExecuteNonQuery();

                connection.Close();

                if (numberofcommits == 0)
                    return NotFound();

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("del/{id}")]
        public IActionResult DeleteDog(Guid id)
        {
            try {

                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "DELETE FROM \"Dog\"WHERE\"Id\"=@Id;";

                using var command = new NpgsqlCommand(commandText,connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                var numberofcommits = command.ExecuteNonQuery();

                connection.Close();

                if (numberofcommits == 0)
                    return NotFound();

                return Ok();

            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }               
        }

        //update nisam stigao :(

    }
}
