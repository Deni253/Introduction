using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;

namespace Introduction.Repository
{
    public class DogOwnerRepository: IDogOwnerRepository //ne radi (sve crud metode rade osim što ovo nisam uspio naslijediti)
    {
        private string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";
        
            public async Task<bool> Delete(Guid id)
            {
            try
            {
                    using var connection = new NpgsqlConnection(connectionString);

                    var commandText = "DELETE FROM \"DogOwner\"WHERE\"Id\"=@Id;";

                    using var command = new NpgsqlCommand(commandText, connection);

                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();//ne staviti kao asinkrono jer onda nećemo bit sigurni jel ona otvorena prije nego ostalo krenemo radit a treba nam

                    var numberOfCommits = await command.ExecuteNonQueryAsync();

                    connection.Close();

                    if (numberOfCommits == 0)
                    {
                        return false;
                    }
                    return true;
            }
                catch (NpgsqlException)
                {              
                    return false;
                }
            }

        public async Task<bool> Post(DogOwner dogOwner)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "INSERT INTO \"DogOwner\"VALUES(@Id,@FirstName,@LastName,@PhoneNumber,@Email);";

                using var command = new NpgsqlCommand(commandText, connection);
                
                command.Parameters.AddWithValue("@id", Guid.NewGuid());
                command.Parameters.AddWithValue("@FirstName", dogOwner.FirstName);
                command.Parameters.AddWithValue("@LastName", dogOwner.LastName);
                command.Parameters.AddWithValue("@PhoneNumber", dogOwner.PhoneNumber);
                command.Parameters.AddWithValue("@Email", dogOwner.Email);

                connection.Open();
                
                var numberOfCommits = await command.ExecuteNonQueryAsync(); // promijenimo korištenje metode
                                                                            
                connection.Close();
                //awaitanje Taska će vratit int 
                if (numberOfCommits == 0)
                {
                    return false;
                }
                return true;
            }
            catch (NpgsqlException ex)
            {
                return false ;
            }
        }
        public async Task<DogOwner> Get(Guid id)
        {
            try
            {
                var dogOwner = new DogOwner();
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "SELECT * FROM\"DogOwner\"WHERE \"Id\" = @Id;";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                using NpgsqlDataReader reader =await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    
                    reader.Read();

                    dogOwner.Id = Guid.Parse(reader[0].ToString());
                    dogOwner.FirstName = reader[1].ToString();
                    dogOwner.LastName = reader[2].ToString();
                    dogOwner.PhoneNumber = reader[3].ToString();
                    dogOwner.Email = reader[4].ToString();
                    
                }
                connection.Close();
                return dogOwner;
               
            }
            catch (NpgsqlException)
            {
                return null;
            }
        }
        public async Task<bool> Update(Guid id,DogOwner dogOwner)
        {
            try
            {
                
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "UPDATE\"DogOwner\"SET\"Email\"=@Email WHERE\"Id\"=@Id;";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@Id", id);                
                command.Parameters.AddWithValue("@Email",dogOwner.Email); //pošaljemo kroz body postmana(glumi front-end)

                connection.Open();

                var numberOfCommits = await command.ExecuteNonQueryAsync();

                connection.Close();

                if (numberOfCommits == 0)
                {
                    return false;
                }
                return true;
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }
    }
}
