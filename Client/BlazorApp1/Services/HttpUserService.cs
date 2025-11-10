using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using ApiContracts.UserFolder;

namespace BlazorApp1.Services
{
    public class HttpUserService : IUserService
    {
        private readonly HttpClient client;

        public HttpUserService(HttpClient client)
        {
            this.client = client;
        }

        // Create a new user
        public async Task<UserDTO> AddUserAsync(CreateUserDTO request)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("users", request);
            string content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                throw new Exception($"Error creating user: {content}");

            return JsonSerializer.Deserialize<UserDTO>(content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        }

        // Update existing user
        public async Task UpdateUserAsync(int id, UpdateUserDTO request)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync($"users/{id}", request);
            if (!response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error updating user: {content}");
            }
        }

        // Get user by id
        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await client.GetFromJsonAsync<UserDTO>($"users/{id}");
            if (user == null)
                throw new Exception($"User with id {id} not found");
            return user;
        }
    }
}
