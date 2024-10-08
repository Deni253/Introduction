﻿using Introduction.Common;
using Introduction.Model;
using Npgsql;
//using Introduction.Repository.Common;


namespace Introduction.Repository
{
    public class DogRepository//:IDogRepository ne radi (sve crud metode rade osim što ovo nisam uspio naslijediti)
    {
        private string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=postgres";

        public bool Delete(Guid id)
        {
            try
            {

                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "DELETE FROM \"Dog\"WHERE\"Id\"=@Id;";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                var numberofcommits = command.ExecuteNonQuery();

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

        public bool Post(Dog dog)
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

                var numberofcommits = command.ExecuteNonQuery();

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
        public Dog Get(Guid id)
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
                return dog;
            }
            catch (NpgsqlException)
            {
                return null;
            }
        }
        public bool Update(Guid id)
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

                var numberofcommits = command.ExecuteNonQuery();

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
