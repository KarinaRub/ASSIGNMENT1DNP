using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users = [];

    public UserInMemoryRepository()
    {
        // .GetAwaiter() blocks the async call since constructors cannot be async (ideally we should seed somewhere else, but this keeps things simple)
        SeedDataAsync().GetAwaiter();
    }

    public Task<User> AddAsync(User user)
    {
        user.Id = users.Count != 0 ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.Id == id) ?? throw new InvalidOperationException($"User with ID '{id}' not found.");
        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public IQueryable<User> GetMany()
    {
        return users.AsQueryable();
    }

    public Task<User> GetSingleAsync(int id)
    {
        User? user = users.SingleOrDefault(u => u.Id == id) ?? throw new InvalidOperationException($"User with ID '{id}' not found.");
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id) ?? throw new InvalidOperationException($"User with ID '{user.Id}' not found.");
        users.Remove(existingUser);
        users.Add(user);
        return Task.CompletedTask;
    }

    private async Task SeedDataAsync()
    {
        User user1 = new()
        {
            Id = 1,
            Username = "User1",
            Password = "123",
        };
        User user2 = new()
        {
            Id = 2,
            Username = "User2",
            Password = "abc",
        };
        await AddAsync(user1);
        await AddAsync(user2);

    }
}

 