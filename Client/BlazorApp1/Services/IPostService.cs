using System.Collections.Generic;
using System.Threading.Tasks;
using ApiContracts.PostFolder;

namespace BlazorApp1.Services
{
    public interface IPostService
    {
        Task<PostDTO> CreatePostAsync(CreatePostDTO request);
        Task<ICollection<PostDTO>> GetPostsAsync();
        Task DeleatAsync(int id);
          public Task UpdatePostAsync(int id, CreatePostDTO request);

        Task<PostDTO> GetPostByIdAsync(int id);
        Task<PostDTO> AddPostAsync(CreatePostDTO request);
        public Task<List<PostDTO>> GetManyAsync();
           public Task<PostDTO> GetSingleAsync(int id);
    }
}

