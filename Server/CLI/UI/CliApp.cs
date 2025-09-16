using System;
using System.Reflection.Metadata;
using CLI.UI.ManagePosts;
using CLI.UI.ManageUser;
using CLI.UI.ManageUsers;
using CLI.UI.ManageUsers;
using InMemoryRepository;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    public CommentInterface InMemoryCommentRepository { get; set; }
    public UserInterface InMemoryUserRepository { get; set; }
    public PostInterface InMemoryPostRepository { get; set; }

    private ManageUserView manageUserView;
    private ManagePostsView ManagePostsView;

    public CliApp(CommentInterface commentInterface, UserInterface userInterface, PostInterface postInterface)
    {
        this.InMemoryCommentRepository = commentInterface;
        this.InMemoryUserRepository = userInterface;
        this.InMemoryPostRepository = postInterface;
        this.manageUserView = new ManageUserView(userInterface);
        this.ManagePostsView = new ManagePostsView(InMemoryPostRepository);
    }

    internal async Task StartAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Main Menu ===");
            Console.WriteLine("1. Manage User");
            Console.WriteLine("2. Manage Post");
            Console.WriteLine("3. Manage Comment");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await manageUserView.ShowMenuAsync();
                    break;
                case "2":
                    await ManagePostsView.RunAsync();
                    break;
            }
        }
    }
}
