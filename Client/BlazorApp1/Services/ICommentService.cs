using System.Threading.Tasks;
using ApiContracts.CommentFolder;

namespace BlazorApp1.Services
{
    public interface ICommentService
    {
        Task<CommentDTO> AddCommentAsync(CreateCommentDTO request);
    }
}
