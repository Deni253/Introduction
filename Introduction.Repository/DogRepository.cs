using Introduction.Common;
using Introduction.Model;
using Introduction.Repository.Common;
using Npgsql;
using System.Text;

//using Introduction.Repository.Common;

namespace Introduction.Repository
{
    public class DogRepository : IDogRepository //ne radi (sve crud metode rade osim što ovo nisam uspio naslijediti)
    {
        private string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "DELETE FROM \"Dog\"WHERE\"Id\"=@Id;";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                var numberofcommits = await command.ExecuteNonQueryAsync();

                connection.Close();

                if (numberofcommits == 0)
                    return false;
                return true;
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }

        public async Task<bool> Post(Dog dog)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "INSERT INTO \"Dog\"VALUES( @id, @idDogOwner, @Name, @BirthDate, @Age,@FurColor,@Breed,@IsTrained);";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", Guid.NewGuid());
                command.Parameters.AddWithValue("@idDogOwner", dog.DogOwnerId);
                command.Parameters.AddWithValue("@Name", dog.Name);
                command.Parameters.AddWithValue("@BirthDate", dog.BirthDate);
                command.Parameters.AddWithValue("@Age", dog.Age);
                command.Parameters.AddWithValue("@FurColor", dog.FurColor);
                command.Parameters.AddWithValue("@Breed", dog.Breed);
                command.Parameters.AddWithValue("@IsTrained", dog.IsTrained);

                connection.Open();

                var numberofcommits = await command.ExecuteNonQueryAsync();

                connection.Close();

                if (numberofcommits == 0)
                    return false;
                return true;
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }

        public async Task<Dog> Get(Guid id)
        {
            try
            {
                var dog = new Dog();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Dog\" WHERE \"Id\" = @id;";
                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

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
                return dog;
            }
            catch (NpgsqlException)
            {
                return null;
            }
        }

        public async Task<List<Dog>> GetAll(DogFilter filter,Sorting sorting)
        {
            try
            {
                if (filter == null)
                {
                    return new List<Dog>();
                }

                StringBuilder stringBuilder = new StringBuilder();
                List<Dog> dogs = new List<Dog>();
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT * FROM \"Dog\" WHERE 1=1";

                stringBuilder.Append(commandText);

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    stringBuilder.Append(" AND \"Name\"=@name");
                }

                if (!string.IsNullOrEmpty(filter.Breed))
                {
                    stringBuilder.Append(" AND \"Breed\"=@breed");
                }

                if (filter.Age > 0)
                {
                    stringBuilder.Append(" AND \"Age\"=@age"); //problem su bool i int
                }

                if (filter.IsTrained.HasValue)
                {
                    stringBuilder.Append(" AND \"IsTrained\"=@isTrained");
                }
                if (!string.IsNullOrEmpty(sorting.OrderBy))
                {
                    stringBuilder.Append(" ORDER BY \"Name\" ASC");
                }
                




                commandText = stringBuilder.ToString();

                using var command = new NpgsqlCommand(commandText, connection);

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    command.Parameters.AddWithValue("@name", filter.Name);
                }

                if (!string.IsNullOrEmpty(filter.Breed))
                {
                    command.Parameters.AddWithValue("@breed", filter.Breed);
                }

                if (filter.Age > 0)
                {
                    command.Parameters.AddWithValue("@age", filter.Age);
                }

                if (filter.IsTrained.HasValue)
                {
                    command.Parameters.AddWithValue("@isTrained", filter.IsTrained);
                }
                if (filter.IsTrained.HasValue)
                {
                    command.Parameters.AddWithValue("@isTrained", filter.IsTrained);
                }
                if (!string.IsNullOrEmpty(sorting.OrderBy))
                {
                    command.Parameters.AddWithValue("@isTrained", filter.IsTrained);
                }



                connection.Open();
                using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dog dog = new()
                        {
                            Id = Guid.Parse(reader[0].ToString()),
                            DogOwnerId = Guid.Parse(reader[1].ToString()),
                            Name = reader[2].ToString(),
                            BirthDate = Convert.ToDateTime(reader[3]),
                            Age = Convert.ToInt32(reader[4]),
                            FurColor = reader[5].ToString(),
                            Breed = reader[6].ToString(),
                            IsTrained = Convert.ToBoolean(reader[7]),
                        };

                        dogs.Add(dog);
                    }
                }

                connection.Close();
                return dogs;
            }
            catch (NpgsqlException )
            {          
                return null;
            }
        }

        public async Task<bool> Update(Guid id)
        {
            try
            {
                var dog = new Dog();
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "UPDATE \"Dog\" SET \"IsTrained\" = @IsTrained WHERE \"Id\" = @Id;";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@IsTrained", true);
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();

                var numberofcommits = await command.ExecuteNonQueryAsync();

                connection.Close();

                if (numberofcommits == 0)
                    return false;

                return true;
            }
            catch (NpgsqlException)
            {
                return false;
            }
        }
    }
}