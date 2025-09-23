using System;
using System.Runtime.InteropServices.Marshalling;
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepository;

public class CommentFileRepository : CommentInterface

{
    private readonly string filePath = "comments.json";

    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    public async Task<Comment> AddAsync(Comment comment)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        //If there are no comments, this will start IDs at 2 (because you set 1 then +1).
        int? maxID = comments.Count > 0 ? comments.Max(c => c.Id??0) : 0;
        comment.Id = maxID + 1;
        comments.Add(comment);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
        return comment;
    }

 

    public async Task DeleteAsync(int id)
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        Comment? commentToRemove = comments.SingleOrDefault(c => c.Id == id);
        if (commentToRemove == null)
        {
            throw new InvalidOperationException($"Comment with {id} not found");

        }
        comments.Remove(commentToRemove);
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }

    public IQueryable<Comment> GetManyAsync()
    {
        string commentsAsJson = File.ReadAllTextAsync(filePath).Result;
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }

    public async Task<Comment?> GetSingleAsync(int id)
    {
        string commentsAsJson = File.ReadAllText(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.SingleOrDefault(c => c.Id == id);
    }

    public async Task UpdateAsync(Comment comment)
    {
        string commentsAsJson = File.ReadAllText(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        int index = comments.FindIndex(c => c.Id == comment.Id);
        if (index == -1)
        {
            throw new InvalidOperationException($"Comment with Id {comment.Id} not found");
        }
        comments[index] = comment;
        commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }



    IQueryable<Comment> CommentInterface.GetManyAsync()
    {
        throw new NotImplementedException();
    }

    Task<Comment> CommentInterface.GetSingleAsync(int id)
    {
        throw new NotImplementedException();
    }
}
