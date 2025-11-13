using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository userRepository;

    public AuthController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login([FromBody] LoginRequest request)
    {
        User? user = userRepository.GetMany().SingleOrDefault(u => u.Username == request.UserName);
        if (user == null)
        {
            return Unauthorized();
        }

        if (user.Password != request.Password)
        {
            return Unauthorized();
        }

        UserDTO userDTO = new UserDTO
        {
            Id = user.Id,
            Username = user.Username
        };


        return userDTO;
    }
}

public class UserDTO
{
}