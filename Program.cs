using System;
using Blog.Models;
using Blog.Repositories;
using Dapper.Contrib.Extensions;
using Microsoft.Data.SqlClient;

namespace Blog
{
    class Program
    {
        private const string CONNECTION_STRING = "Server=localhost,1433;Database=Blog;TrustServerCertificate=True;User ID=sa;Password=1q2w3e4r@#$";
        static void Main(string[] args)
        {
            var connection = new SqlConnection(CONNECTION_STRING);
            connection.Open();
            //ReadUser();
            //CreateUser();
            //UpdateeUser();
            //DeleteUser();
            //CreateUser(connection);
            //ReadUsers(connection);
            // InsertRole(connection);
            // ReadRoles(connection);
            ReadUsersWithRole(connection);
            //ReadUsers(connection);
            connection.Close();
        }
        public static void ReadUsers(SqlConnection connection)
        {
            var repository = new Repository<User>(connection);
            var users = repository.Get();

            foreach (var user in users)
            {
                Console.WriteLine($"{user.Name} - {user.Email}");
            }
        }

        public static void CreateUser(SqlConnection connection)
        {
            var user = new User()
            {
                Name = "Laís",
                Email = "laaisff98@gmail.com",
                PasswordHash = "Hash",
                Bio = "Professora Laís",
                Image = "https://",
                Slug = "professora-laisff"
            };

            connection.Insert<User>(user);
            Console.WriteLine($"Usuário cadastrado com sucesso - {user.Name}");
        }

        public static void ReadUsersWithRole(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var items = repository.GetWithRoles();
            foreach (var item in items)
            {
                Console.WriteLine(item.Name);
                foreach (var role in item.Roles)
                {
                    Console.WriteLine($" - {role.Name}");
                }
            }
        }

    }
}
