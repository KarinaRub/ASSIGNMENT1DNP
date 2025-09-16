using System;
using System.Security.Cryptography.X509Certificates;
using Entities;
using RepositoryContracts;
namespace CLI.UI.Manage_Post;

public class CreatePostView
{
    private PostInterface postinterface;

    public CreatePostView(PostInterface postinterface)
    {
        this.postinterface = postinterface;
    }
    public async Task ShowAsync()
    {
            Console.Write("Enter post title: ");
        string? title = Console.ReadLine();

        Console.Write("Enter post body: ");
        string? body = Console.ReadLine();

        Console.Write("Enter User ID for this post: ");
        string? input = Console.ReadLine();

        if (!int.TryParse(input, out int userId))
        {
            Console.WriteLine("Invalid user ID. Please enter a number.");
            return;
        }

        var post = new Post
        {
            Title = title,
            Body = body,
            UserId = userId
        };

        await postinterface.AddAsync(post);
        Console.WriteLine("Post created successfully!");
        
}
}


