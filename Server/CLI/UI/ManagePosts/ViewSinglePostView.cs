using System;
using Entities;
using RepositoryContracts;
namespace CLI.UI.Manage_Post;

public class ViewSinglePostView
{
    private PostInterface postInterface;

    public ViewSinglePostView(PostInterface postinterface)
    {
        this.postInterface = postinterface;
    }
    public async Task ShowAsync()
    {
        Console.WriteLine("Enter post Id: ");
        int commentIdInput = Convert.ToInt32(Console.ReadLine());
        Post? postId = await postInterface.GetSingleAsync(commentIdInput);
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
