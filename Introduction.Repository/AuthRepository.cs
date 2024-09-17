﻿using Introduction.Model;
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

                if (numberOfCommits > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
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


        public async Task<User> GetUserByUsernameAndPassword(string username, string password)
        {
            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                var commandText = "SELECT u.*, r.\"Name\" AS \"RoleName\" " +
                    "FROM \"User\" u " +
                    "JOIN \"Role\" r ON u.\"RoleId\" = r.\"Id\" " +
                    "WHERE u.\"Username\" = @username AND u.\"Password\" = @password";

                using var command = new NpgsqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", password);

                await connection.OpenAsync();
                using var reader = await command.ExecuteReaderAsync();

                if (reader.HasRows)
                {
                    await reader.ReadAsync();
                    var user = new User
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Username = reader["Username"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["Password"].ToString(),
                        Role = new Role
                        {
                            Name = reader["RoleName"].ToString() //  mapiranje
                        }
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
                return null;
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