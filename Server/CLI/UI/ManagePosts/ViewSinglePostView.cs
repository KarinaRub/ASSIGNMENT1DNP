using System;
using Entities;
using RepositoryContracts;
namespace CLI.UI.Manage_Post;

public class ViewSinglePostView
{
    private PostInterface postinterface;

    public ViewSinglePostView(PostInterface postinterface)
    {
        this.postinterface = postinterface;
    }
    public async Task ShowAsync()
    {
        Console.WriteLine("Enter post Id: ");
        int commentIdInput = Convert.ToInt32(Console.ReadLine());
        Post? postId = await postinterface.GetSingleAsync(commentIdInput);
        if (postId != null)
        {
            System.Console.WriteLine($"ID: {postId.Id},Title :{postId.Title} Body: {postId.Body}, UserID: {postId.UserId}");
        }
        else
        {
            Console.WriteLine("Comment not found");
        }

    }
}

