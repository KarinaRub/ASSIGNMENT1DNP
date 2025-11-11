using System.Threading.Tasks;
using ApiContracts.CommentFolder;

namespace BlazorApp1.Services
{
    public interface ICommentService
    {
        Task<CommentDTO> AddCommentAsync(CreateCommentDTO request);
            public Task UpdateCommentAsync(int id, CreateCommentDTO request);
    public Task DeleteAsync(int id);
    public Task<CommentDTO> GetSingleAsync(int id);
    public Task<List<CommentDTO>> GetManyAsync();
   
   Task<List<CommentDTO>> GetByPostIdAsync(int postId);
    }
}
