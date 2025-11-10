using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ApiContracts.PostFolder;

namespace BlazorApp1.Services
{
    public class HttpPostService : IPostService
    {
        private readonly HttpClient client;

        public HttpPostService(HttpClient client)
        {
            this.client = client;
        }

        // Create a new post
        public async Task<PostDTO> CreatePostAsync(CreatePostDTO request)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("posts", request);
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error creating post: {content}");

            return JsonSerializer.Deserialize<PostDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }

        // Get all posts (for list view)
        public async Task<ICollection<PostDTO>> GetPostsAsync()
        {
            var posts = await client.GetFromJsonAsync<ICollection<PostDTO>>("posts");
            return posts ?? new List<PostDTO>();
        }

        // Get a single post by id (for details page)
        public async Task<PostDTO> GetPostByIdAsync(int id)
        {
            var post = await client.GetFromJsonAsync<PostDTO>($"posts/{id}");
            if (post == null)
                throw new Exception($"Post with id {id} not found");
            return post;
        }
    }
}
