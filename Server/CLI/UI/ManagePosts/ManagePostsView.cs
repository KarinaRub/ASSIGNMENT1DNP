using System;
using CLI.UI.ManagePosts;
using RepositoryContracts;
namespace CLI.UI.Manage_Post;


public class ManagerPostView
{
    private PostInterface postInterface;
    private CreatePostView createPostView;
    private ListPostsView ListPostsView;
    private ViewSinglePostView viewSinglePostView;

    public ManagerPostView(PostInterface postInterface)
    {
        this.postInterface = postInterface;
        this.createPostView = new CreatePostView(postInterface);
        this.ListPostsView = new ListPostsView(postInterface);
        this.viewSinglePostView = new ViewSinglePostView(postInterface);
    }
       
    public async Task ShowMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Manage Post ===");
            Console.WriteLine("1. Create Post");
            Console.WriteLine("2. List Posts");
            Console.WriteLine("3. View single Post by Id: ");
            Console.WriteLine("0. Back");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await createPostView.ShowAsync();
                    break;
                case "2":
                    await ListPostsView.ShowAsync();
                    break;
                case "3":
                    await viewSinglePostView.ShowAsync();
                    break;
                case "0":
                    return;
            }
        }
    }
}
