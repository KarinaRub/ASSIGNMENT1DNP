using System.Collections.Generic;
using System.Threading.Tasks;
using ApiContracts.PostFolder;

namespace BlazorApp1.Services
{
    public interface IPostService
    {
        Task<PostDTO> CreatePostAsync(CreatePostDTO request);
        Task<ICollection<PostDTO>> GetPostsAsync();
        Task<PostDTO> GetPostByIdAsync(int id);
    }
}

