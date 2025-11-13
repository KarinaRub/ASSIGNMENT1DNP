using ApiContracts;
using ApiContracts.User;
using Entities;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository userRepo;

    public UsersController(IUserRepository userRepo)
    {
        this.userRepo = userRepo;
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> AddUser([FromBody] CreateUserDto request)
    {
        await VerifyUserNameIsAvailableAsync(request.UserName);

        User user = new(request.UserName, request.Password);
        User created = await userRepo.AddAsync(user);
        UserDto dto = new()
        {
            Id = created.Id,
            UserName = created.UserName
        };
        return Created($"/Users/{dto.Id}", dto); 
    }

     private async Task VerifyUserNameIsAvailableAsync(string userName)
    {
        bool usernameIsTaken = userRepo.GetMany()
            .Any(u => u.UserName.ToLower().Equals(userName.ToLower()));
        if (usernameIsTaken)
        {
            throw new ValidationException($"Username '{userName}' is already taken");
        }
    }

    [HttpPut("{id:int}")] 
        public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UpdateUserDto request)
    {
        User existing = await userRepo.GetSingleAsync(id);

        existing.UserName = request.UserName;
        existing.Password = request.Password;
        await userRepo.UpdateAsync(existing);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteUser([FromRoute] int id)
    {
        await userRepo.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<User>> GetUser([FromRoute] int id)
    {
        User user = await userRepo.GetSingleAsync(id);
        return Ok(user);
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> GetUsers([FromQuery] string? userNameContains = null)
    {
        
        IQueryable<User> users = userRepo.GetMany()
            .Where(
                u => userNameContains == null ||
                     u.UserName.ToLower().Contains(userNameContains.ToLower())
            );

        return Ok(users);
    }
    
   
    [HttpGet("{userId:int}/posts")]
    public async Task<ActionResult<IEnumerable<Post>>> GetPostsForUser(
        [FromRoute] int userId,
        [FromServices] IPostRepository postRepo)
    {
        List<Post> posts = postRepo.GetMany()
            .Where(p => p.UserId == userId)
            .ToList();
        return Ok(posts);
    }

    [HttpGet("{userId:int}/comments")]
    public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForUser(
        [FromRoute] int userId,
        [FromServices] ICommentRepository commentRepo)
    {
        List<Comment> comments = commentRepo.GetMany()
            .Where(c => c.UserId == userId)
            .ToList();
        return Ok(comments);
    }
}