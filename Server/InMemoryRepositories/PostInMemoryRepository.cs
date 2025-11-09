using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private readonly List<Post> posts = [];

    public PostInMemoryRepository()
    {
        // .GetAwaiter() blocks the async call since constructors cannot be async (ideally we should seed somewhere else, but this keeps things simple)
        SeedDataAsync().GetAwaiter();
    }

    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Count != 0 ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task DeleteAsync(int id)
    {
        Post? postToRemove = posts.SingleOrDefault(p => p.Id == id) ?? throw new InvalidOperationException($"Post with ID '{id}' not found.");
        posts.Remove(postToRemove);
        return Task.CompletedTask;
    }

    public IQueryable<Post> GetMany()
    {
        return posts.AsQueryable();
    }

    public Task<Post> GetSingleAsync(int id)
    {
        Post? post = posts.SingleOrDefault(p => p.Id == id) ?? throw new InvalidOperationException($"Post with ID '{id}' not found.");
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        Post? existingPost = posts.SingleOrDefault(p => p.Id == post.Id) ?? throw new InvalidOperationException($"Post with ID '{post.Id}' not found.");
        posts.Remove(existingPost);
        posts.Add(post);
        return Task.CompletedTask;
    }

    private async Task SeedDataAsync()
    {
        Post post1 = new()
        {
            Id = 1,
            Title = "post1title",
            Body = "post1",
            UserId = 1,
        };
        Post post2 = new()
        {
            Id = 2,
            Title = "post2title",
            Body = "post2",
            UserId = 2,
        };
        Post post3 = new()
        {
            Id = 3,
            Title = "post3title",
            Body = "post3",
            UserId = 1,
        };
        await AddAsync(post1);
        await AddAsync(post2);
        await AddAsync(post3);
    }
}
