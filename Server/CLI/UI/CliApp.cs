using System;
using System.Reflection.Metadata;
using CLI.UI.Manage_Post;
using CLI.UI.ManagerComment;
using CLI.UI.ManageUser;

using RepositoryContracts;

namespace CLI.UI;

public class CliApp
{
    public CommentInterface commentInterface { get; set; }
    public UserInterface userInterface { get; set; }
    public PostInterface postinterface { get; set; }

    public ManageUserView manageUserView;
    public ManagerPostView managerPostView;

    public ManageCommentView manageCommentView;
    private IUserRepository userRepository;
    private IPostRepository postRepository;
    private ICommentRepository commentRepository;

    public CliApp(CommentInterface commentInterface, UserInterface userInterface, PostInterface postinterface)
    {
        this.commentInterface = commentInterface;
        this.userInterface = userInterface;
        this.postinterface = postinterface;
        this.manageUserView = new ManageUserView(userInterface);
        this.managerPostView = new ManagerPostView(postinterface);
        this.manageCommentView = new ManageCommentView(commentInterface);
    }

    public CliApp(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository)
    {
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
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
                    await managerPostView.ShowMenuAsync();
                    break;
                case "3":
                    await manageCommentView.ShowMenuAsync();
                    break;
            }
        }
    }
}
