using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EFCRepository;

public class EfcPostRepository : Postinterface
{
 private readonly AppContext ctx;

    public EfcPostRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Post> AddAsync(Post post)
    {
        await ctx.Posts.AddAsync(post);
        await ctx.SaveChangesAsync();
        return post;
    }

    public async Task UpdateAsync(Post post)
    {

       if (!await ctx.Posts.AnyAsync(p => p.Id == post.Id))
    {
        throw new System.Collections.Generic.KeyNotFoundException($"Post with id {post.Id} not found");
    }

    ctx.Posts.Update(post);
    await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Post? existing = await ctx.Posts.SingleOrDefaultAsync(p => p.Id == id); 
        if (existing == null) 
        {
            throw new System.Collections.Generic.KeyNotFoundException($"Post with id {id} not found");
        }

        ctx.Posts.Remove(existing); 
        await ctx.SaveChangesAsync();
    }

    public async Task<Post> GetSingleAsync(int id)
    {
      var post = await ctx.Posts.FindAsync(id);
      if (post == null)
      {
          throw new System.Collections.Generic.KeyNotFoundException($"Post with id {id} not found");
      }
      return post;
    }

    public IQueryable<Post> GetManyAsync()
    {
        return ctx.Posts.AsQueryable();
    }
}
