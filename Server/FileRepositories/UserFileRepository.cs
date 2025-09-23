using System;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepository;

public class NewBaseType
{
    public Task<User> AddAsync(User user) => throw new NotImplementedException();

    public Task<Post> AddAsync(Post post)
    {
        throw new NotImplementedException();
    }
}

public class UserFileRepository : NewBaseType, UserInterface
{
    private readonly string filePath = "Users.json";

    public UserFileRepository()
    {
        if (!File.Exists(filePath) || string.IsNullOrWhiteSpace(File.ReadAllText(filePath)))
        {
            File.WriteAllText(filePath, "[]");
        }
    }

    public async Task<User> AddAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User>? users;
        try
        {
            users = JsonSerializer.Deserialize<List<User>>(usersAsJson);
        }
        catch
        {
            users = new List<User>();
        }
        if (users == null)
        {
            users = new List<User>();
        }
        int maxId = (int)(users.Count > 0 ? users.Max(u => u.Id) : 0);
        user.Id = maxId + 1;
        users.Add(user);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        User? userToRemove = users.SingleOrDefault(u => u.Id == id);
        if (userToRemove == null)
            throw new InvalidOperationException($"User with ID {id} not found.");
        users.Remove(userToRemove);
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }

    public IQueryable<User> GetManyAsync()
    {
        string usersAsJson = File.ReadAllText(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.AsQueryable();
    }

    public async Task<User> GetSingleAsync(int id)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;
        return users.SingleOrDefault(u => u.Id == id)!;
    }

    public async Task UpdateAsync(User user)
    {
        string usersAsJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersAsJson)!;

        int index = users.FindIndex(u => u.Id == user.Id);
        if (index == -1)
            throw new InvalidOperationException($"User with ID {user.Id} not found.");

        users[index] = user;
        usersAsJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersAsJson);
    }


    IQueryable<User> UserInterface.GetManyAsync()
    {
        throw new NotImplementedException();
    }

    Task<User> UserInterface.GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }
}
