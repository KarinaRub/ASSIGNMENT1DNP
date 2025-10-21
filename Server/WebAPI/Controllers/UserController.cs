using ApiContracts.UserFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserInterface userInterface;

        public UserController(UserInterface userInterface)
        {
            this.userInterface = userInterface;
        }
        // POST /users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] CreateUserDTO request)
        {
            var user = new User { Username = request.Username, Passsword = request.Password };
            var created = await userInterface.AddAsync(user);

           
            if (created.Username is null)
                return Problem("User was created with null Username.", statusCode: 500);

            var result = new UserDTO { Id = created.Id, Username = created.Username };
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        // GET /users/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserDTO>> GetById(int id)
        {
            User? u;
            try
            {
                u = await userInterface.GetSingleAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            if (u is null) return NotFound();

            return new UserDTO
            {
                Id = u.Id,
                Username = u.Username ?? string.Empty
            };
        }

        // GET 
        [HttpGet]
        public ActionResult<IEnumerable<UserDTO>> GetMany([FromQuery] string? usernameContains)
        {
            var q = userInterface.GetManyAsync(); 

            if (!string.IsNullOrWhiteSpace(usernameContains))
            {
                // EF-friendly case-insensitive contains
                var needle = usernameContains.ToLower();
                q = q.Where(u => u.Username != null && u.Username.ToLower().Contains(needle));
            }

            var list = q
                .Select(u => new UserDTO
                {
                    Id = u.Id,
                    Username = u.Username ?? string.Empty
                })
                .ToList();

            return list;
        }

        // PUT /
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateUserDTO dto)
        {
            var user = new User { Id = id, Username = dto.Username, Passsword = dto.Password };
            try
            {
                await userInterface.UpdateAsync(user);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE /users/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await userInterface.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
