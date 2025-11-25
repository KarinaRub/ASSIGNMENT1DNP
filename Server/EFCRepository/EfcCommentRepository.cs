using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace EFCRepository;

public class EfcCommentRepository : CommentInterface
{
 private readonly AppContext ctx;

    public EfcCommentRepository(AppContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        await ctx.Comments.AddAsync(comment);
        await ctx.SaveChangesAsync();
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {

       if (!await ctx.Posts.AnyAsync(c => c.Id == comment.Id))
    {
        throw new System.Collections.Generic.KeyNotFoundException($"Comment with id {comment.Id} not found");
    }

    ctx.Comments.Update(comment);
    await ctx.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Comment? existing = await ctx.Comments.SingleOrDefaultAsync(c => c.Id == id); 
        if (existing == null) 
        {
            throw new System.Collections.Generic.KeyNotFoundException($"Comment with id {id} not found");
        }

        ctx.Comments.Remove(existing); 
        await ctx.SaveChangesAsync();
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
      var comment = await ctx.Comments.FindAsync(id);
      if (comment == null)
      {
          throw new System.Collections.Generic.KeyNotFoundException($"Comment with id {id} not found");
      }
      return comment;
    }

    public IQueryable<Comment> GetManyAsync()
    {
        return ctx.Comments.AsQueryable();
    }
}
