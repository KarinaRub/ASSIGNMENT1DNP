using System;
using Entities;
using RepositoryContracts;
namespace CLI.UI.ManagerComment;

public class ViewSingleComment
{
    private CommentInterface CommentInterface;

    public ViewSingleComment(CommentInterface CommentInterface)
    {
        this.CommentInterface = CommentInterface;
    }
    public async Task ShowAsync()
    {
        Console.WriteLine("Enter comment Id: ");
        int commentIdInput = Convert.ToInt32(Console.ReadLine());
        Comment? comment = await CommentInterface.GetSingleAsync(commentIdInput);
        if (comment != null)
        {
            System.Console.WriteLine($"ID: {comment.Id}, Body: {comment.Body}, PostID: {comment.PostId}, UserID: {comment.UserId}");
        }
        else
        {
            Console.WriteLine("Comment not found");
        }

    }
}
