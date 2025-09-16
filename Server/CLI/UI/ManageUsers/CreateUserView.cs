using System;
using System.Threading.Tasks;
using RepositoryContracts;
using Entities;
using InMemoryRepositories;

namespace CLI.UI.ManageUsers
{
    public class CreateUserView
    {
        private readonly UserInterface userRepository;

        public CreateUserView(UserInterface userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddUser()
        {
            System.Console.WriteLine("write username here");
            string? UsernameInput = Console.ReadLine();
            System.Console.WriteLine("write your Password");
            string? PasswordnameInput = Console.ReadLine();
            if (!int.TryParse(PasswordnameInput, out int password))
            {
                Console.WriteLine("Invalid password. Please enter digits only.");
                return;
            }
            var user = new User
            {
                Username = UsernameInput,
                Password = password
            };
            await userRepository.AddAsync(user);
            System.Console.WriteLine("Created User");
        }

    }
}
