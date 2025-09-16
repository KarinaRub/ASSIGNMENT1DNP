using System;
using Entities;
using RepositoryContracts;

namespace CLI.UI.ManagerComment;

public class CreateCommentView
{
    private CommentInterface CommentInterface;

    public CreateCommentView(CommentInterface CommentInterface)
    {
        this.CommentInterface = CommentInterface;
    }

    internal async Task ShowAsync()
    {
        Console.WriteLine("Enter comment body: ");
        string? body = Console.ReadLine();

        Console.WriteLine("Enter post Id: ");
        int? postId = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Please enter user Id: ");
        int? userId = Convert.ToInt32(Console.ReadLine());

        var comment = new Comment
        {
            Body = body,
            PostId = postId,
            UserId = userId
        };
        Comment comment1 = await CommentInterface.AddAsync(comment);
        Console.WriteLine("Commented created sucefully");
            }
}
