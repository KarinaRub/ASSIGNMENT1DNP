using System;
using Entities;

namespace RepositoryContracts;

public interface UserInterface
{
   Task<Post> AddAsync(User user);
    Task UpdateAsyncU(User user);
    Task DeleteAsync(int id);
    Task<Post> GetSingleAsync(int id);
    IQueryable<Post> GetManyAsync();
}
