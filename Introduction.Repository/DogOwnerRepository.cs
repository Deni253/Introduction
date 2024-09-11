using Introduction.Common;
using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System.Text;

namespace Introduction.Repository
{
    public class DogOwnerRepository : IDogOwnerRepository //ne radi (sve crud metode rade osim što ovo nisam uspio naslijediti)
    {
        private string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

        public async Task<bool> Delete(DogOwnerFilter ownerFilter)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "DELETE FROM \"DogOwner\"WHERE\"FirstName\"=@firstName;";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@firstName", $"{ownerFilter.FirstName}");

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

        public async Task<bool> Post(DogOwnerFilter ownerFilter)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "INSERT INTO \"DogOwner\"VALUES(@Id,@FirstName,@LastName,@PhoneNumber,@Email);";
                sb.Append(commandText);

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", Guid.NewGuid());
                command.Parameters.AddWithValue("@FirstName", $"{ ownerFilter.FirstName }");
                command.Parameters.AddWithValue("@LastName", $"{ownerFilter.LastName}");
                command.Parameters.AddWithValue("@PhoneNumber", $"{ownerFilter.PhoneNumber}");
                command.Parameters.AddWithValue("@Email", $"{ownerFilter.Email}");

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
                return false;
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

                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

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

        public async Task<List<DogOwner>> GetAll(DogOwnerFilter ownerFilter)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                List<DogOwner> dogOwners = new List<DogOwner>();
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "SELECT * FROM \"DogOwner\" WHERE 1=1";

                stringBuilder.Append(commandText);

                if (!string.IsNullOrEmpty(ownerFilter.FirstName))
                {
                    stringBuilder.Append(" AND \"FirstName\"=@firstName");
                }

                if (!string.IsNullOrEmpty(ownerFilter.LastName))
                {
                    stringBuilder.Append(" AND \"LastName\"=@lastName");
                }

                if (!string.IsNullOrEmpty(ownerFilter.PhoneNumber))
                {
                    stringBuilder.Append(" AND \"PhoneNumber\"=@phoneNumber");
                }

                if (!string.IsNullOrEmpty(ownerFilter.Email))
                {
                    stringBuilder.Append(" AND \"Email\"=@email");
                }

                //var commandText = "SELECT * FROM \"Dog\" FULL OUTER JOIN \"DogOwner\"ON\"Dog.DogOwnerID\"=DogOwner.Id;";

                commandText = stringBuilder.ToString();

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@firstName", $"{ownerFilter.FirstName}");
                command.Parameters.AddWithValue("@lastName", $"{ownerFilter.LastName}");
                command.Parameters.AddWithValue("@phoneNumber", $"{ownerFilter.PhoneNumber}");
                command.Parameters.AddWithValue("@email", $"{ownerFilter.Email}");

                //command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DogOwner dogOwner = new()
                        {
                            Id = Guid.Parse(reader[0].ToString()),
                            FirstName = reader[1].ToString(),
                            LastName = reader[2].ToString(),
                            PhoneNumber = reader[3].ToString(),
                            Email = reader[4].ToString(),
                        };

                        dogOwners.Add(dogOwner);
                    }
                }
                connection.Close();
                return dogOwners;
            }
            catch (NpgsqlException)
            {
                return null;
            }
        }

        public async Task<bool> Update(Guid id, DogOwner dogOwner)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "UPDATE\"DogOwner\"SET\"Email\"=@Email WHERE\"Id\"=@Id;";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Email", dogOwner.Email); //pošaljemo kroz body postmana(glumi front-end)

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