using System;
using System.Threading.Tasks;
using RepositoryContracts;

namespace CLI.UI.ManageUsers
{
    public class ListUsersView
    {
        private readonly UserInterface _userRepository;

        public ListUsersView(UserInterface userRepository)
        {
            _userRepository = userRepository;
        }

        public object Id { get; private set; }
        public object Username { get; private set; }

        public async Task RunAsync()
        {
            var users = await _userRepository.GetAllAsync();
            Console.WriteLine("All Users:");
            foreach (var user in users)
            {
                Console.WriteLine($"[{Id}] {Username}");
            }
        }

        internal async Task ShowAsync()
        {
            throw new NotImplementedException();
        }
    }
}
