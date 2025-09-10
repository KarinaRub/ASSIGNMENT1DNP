using System;
using Entities;

namespace RepositoryContracts;

public interface CommentInterface
{
   Task<Post> AddAsync(Comment comment);
    Task UpdateAsync(Comment comment);
    Task DeleteAsync(int id);
    Task<Post> GetSingleAsync(int id);
    IQueryable<Post> GetManyAsync();
}
