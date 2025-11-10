using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ApiContracts.CommentFolder;

namespace BlazorApp1.Services
{
    public class HttpCommentService : ICommentService
    {
        private readonly HttpClient client;

        public HttpCommentService(HttpClient client)
        {
            this.client = client;
        }

        // Add a new comment
        public async Task<CommentDTO> AddCommentAsync(CreateCommentDTO request)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("comments", request);
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error adding comment: {content}");

            return JsonSerializer.Deserialize<CommentDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }
    }
}
