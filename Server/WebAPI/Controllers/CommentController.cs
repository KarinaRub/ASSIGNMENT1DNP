using ApiContracts.CommentFolder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryContracts;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly CommentInterface commentInterface;

        public CommentController(CommentInterface commentInterface)
        {
            this.commentInterface = commentInterface;
        }
        // Post / create c
        [HttpPost]
        public async Task<ActionResult<CommentDTO>> Create([FromBody] CreateCommentDTO request)
        {
            var c = new Comment
            {
                Body = request.Body,
                PostId = request.PostId,
                UserId = request.AuthorUserId
            };
            var created = await commentInterface.AddAsync(c);
           if (created.Id is null || created.PostId is null || created.UserId is null || created.Body is null)
                return Problem("Repository returned a comment with null fields after creation.", statusCode: 500);

                var result = new CommentDTO
                {
                 Id = created.Id.Value,
                 Body = created.Body,
                PostId = created.PostId.Value,
                AuthorUserId = created.UserId.Value
                };
        return Created($"/comments/{result.Id}", result);
        }
        // Get / read r comment with id
        [HttpGet("{Id:int}")]
        public async Task<ActionResult<CommentDTO>> GetById(int id)
        {
            Comment c;
            try
            {
                c = await commentInterface.GetSingleAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            if (c is null) return NotFound();
            if (c.Id is null || c.PostId is null || c.UserId is null || c.Body is null)
                return Problem("Stored comment has null fields.", statusCode: 500);

                 return new CommentDTO
                 {
                 Id = c.Id.Value,
                 Body = c.Body,
                  PostId = c.PostId.Value,
                AuthorUserId = c.UserId.Value
                };
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<CommentDTO>> GetMany([FromQuery] int? postId, [FromQuery] int? authorUserId)
        {
            var q = commentInterface.GetManyAsync();
            if (postId is not null) q = q.Where(c => c.PostId == postId.Value);
            if (authorUserId is not null) q = q.Where(c => c.UserId == authorUserId.Value);

            var list = q.Select(c => new CommentDTO
            {
                 Id = c.Id.GetValueOrDefault(0),
                Body = c.Body ?? string.Empty,
                PostId = c.PostId.GetValueOrDefault(0),
                AuthorUserId = c.UserId.GetValueOrDefault(0)
            })
                .ToList();

            return list;
        }
        // Put / update u 
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateCommentDTO request)
        {
            var c = new Comment
            {
                Id = id,
                Body = request.Body,
                PostId = request.PostId,
                UserId = request.AuthorUserId
            };
            try
            {
                await commentInterface.UpdateAsync(c);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        // Delete / d with id
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await commentInterface.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
