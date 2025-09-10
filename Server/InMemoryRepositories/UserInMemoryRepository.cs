using System;
using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class InMemoryUser : UserInterface
{
    public List<User>? users { get; set; }
    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any()
        ? users.Max(p => p.Id) + 1
        : 1; users.Add(user);
        return Task.FromResult(user);
    }

    Task<Post> UserInterface.AddAsync(User user)
    {
        throw new NotImplementedException();
    }

    Task UserInterface.DeleteAsync(int id)
    {
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove is null) { throw new InvalidOperationException($"Post with ID '{id}' not found"); } users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public IQueryable<User> GetManyAsync()
    {
        return users.AsQueryable();
    }

    public Task<User> GetSingleAsync(int id)
    {
           User? user = users.SingleOrDefault(u => u.Id == id);
    if (user is null)
    {
        throw new InvalidOperationException($"User with ID '{id}' not found");
    }
    return Task.FromResult(user);
    }

    Task UserInterface.UpdateAsyncU(User user)
    {
        User? existingUser = users.SingleOrDefault(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new InvalidOperationException($"Post with ID '{user.Id}' not found");
        }
        users.Remove(existingUser);

        users.Add(user); return Task.CompletedTask;
    }

    public Task UpdateAsyncU(User user)
    {
        throw new NotImplementedException();
    }

    IQueryable<Post> UserInterface.GetManyAsync()
    {
        throw new NotImplementedException();
    }

    Task<Post> UserInterface.GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }
}