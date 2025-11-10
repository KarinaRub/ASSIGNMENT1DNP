using System.Threading.Tasks;
using ApiContracts.UserFolder;

namespace BlazorApp1.Services
{
    public interface IUserService
    {
        Task<UserDTO> AddUserAsync(CreateUserDTO request);
        Task UpdateUserAsync(int id, UpdateUserDTO request);
        Task<UserDTO> GetUserByIdAsync(int id);
    }
}
