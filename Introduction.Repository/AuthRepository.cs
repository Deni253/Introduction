using Introduction.Model;
using Introduction.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Introduction.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private string connectionString = "Host=localhost;Port=5432;Database=Users;Username=postgres;Password=postgres";

        //public async Task<bool> LoginUser(Login login)
        //{
        //    try
        //    {
        //        using var connection = new NpgsqlConnection(connectionString);

        //        var commandText = "SELECT COUNT(*) FROM \"User\" WHERE \"Username\"=@username AND \"Password\"=@password";

        //        TokenRequest token = new TokenRequest();
        //        using var command = new NpgsqlCommand(commandText, connection);
        //        command.Parameters.AddWithValue("@username", login.Username);
        //        command.Parameters.AddWithValue("@password", login.Password);

        //        await connection.OpenAsync();

        //                                                                 // dohvaćamo broj usera s tim username-om može i sa executeReaderom ako moramo izvuć dodatne info
        //        using var reader = await command.ExecuteReaderAsync();
        //        if (await reader.ReadAsync())
        //        {
        //            // Create and populate the User object
        //            var user = new User
        //            {
        //                Username = reader.GetString(reader.GetOrdinal("Username")),
        //                Email = reader.GetString(reader.GetOrdinal("Email")),
        //                Role = reader.GetString(reader.GetOrdinal("Role"))
        //            };
        //            return user;
        //        }
        //        else
        //        {
        //            return null; // No user found
        //        }




        //        if (count == 0)
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex.Message}");
        //        return false;
        //    }
        //}

        public async Task<User> LoginUser(Login login)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);

                
                var commandText = "SELECT \"Username\", \"Email\", \"Role\" FROM \"User\" WHERE \"Email\" = @email AND \"Password\" = @password";

                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@email", login.Email);
                command.Parameters.AddWithValue("@password", login.Password);

                await connection.OpenAsync();

                
                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    
                    var user = new User
                    {
                        Id = reader.GetGuid(reader.GetOrdinal("Id")),
                        RoleId = reader.GetGuid(reader.GetOrdinal("RoleId")),
                        Username = reader.GetString(reader.GetOrdinal("Username")),
                        Password = reader.GetString(reader.GetOrdinal("Password")),
                        Email = reader.GetString(reader.GetOrdinal("Email")),
                    };
                    return user;
                }
                else
                {
                    return null; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null; // Or handle the error as needed
            }
        }


        public async Task<bool> RegisterUser(User user)
        {
            try
            {
                var userExists = await CheckIfUserExists(user.Username, user.Email);
                if (userExists)
                {
                    Console.WriteLine("User already exists");
                    return false;
                }

                using var connection = new NpgsqlConnection(connectionString);


                var commandText = " INSERT INTO \"User\"VALUES(@id,@username,@password,@firstname,@lastname,@phonenumber,@email);";

                using var command = new NpgsqlCommand(commandText, connection);

                command.Parameters.AddWithValue("@id", Guid.NewGuid());
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@firstname", user.FirstName);
                command.Parameters.AddWithValue("@lastname", user.LastName);
                command.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);
                await connection.OpenAsync();


                var numberOfCommits = await command.ExecuteNonQueryAsync();

                return numberOfCommits > 0;
            }
            catch (PostgresException ex)
            {
                Console.WriteLine($"Database error: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> CheckIfUserExists(string username, string email)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "SELECT COUNT(*) FROM \"User\" WHERE \"Username\"=@username OR \"Email\"=@email";//Broji redke u kojim nam je zadovoljen uvjet COUNT(* ili 1) koji se nalazi u where KORISTI OR A NE AND JER AKO SAMO JEDAN OGOVARA NE MOŽE IĆ U BAZU


                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@email", email);

                await connection.OpenAsync();

                var count = (long)await command.ExecuteScalarAsync(); // dohvaćamo broj usera s tim username-om može i sa executeReaderom ako moramo izvuć dodatne info

                if (count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateUser(Guid id, User user)
        {
            try
            {
                var userExist = await CheckIfUserExists(user.Username, user.Email);
                if (userExist)
                {
                    return false;
                }

               
                using var connection = new NpgsqlConnection(connectionString);

                var commandText = "UPDATE \"User\" SET \"Username\"=@username, \"Password\"=@password, \"Email\"=@email, \"PhoneNumber\"=@phonenumber WHERE \"Id\" = @id"; //Broji redke u kojim nam je zadovoljen uvjet COUNT(* ili 1) koji se nalazi u where KORISTI OR A NE AND JER AKO SAMO JEDAN OGOVARA NE MOŽE IĆ U BAZU


                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@Id",id);
                command.Parameters.AddWithValue("@username", user.Username);
                command.Parameters.AddWithValue("@email", user.Email);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@phonenumber", user.PhoneNumber);

                await connection.OpenAsync();

                var numberOfCommits = await command.ExecuteNonQueryAsync(); // dohvaćamo broj usera s tim username-om može i sa executeReaderom ako moramo izvuć dodatne info

                if (numberOfCommits == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}