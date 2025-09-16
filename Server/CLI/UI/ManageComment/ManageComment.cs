using System;
using CLI.UI.ManageUser;
using RepositoryContracts;
namespace CLI.UI.ManagerComment;

public class ManageCommentView
{
    private CommentInterface CommentInterface;

    private ViewSingleComment ViewSingleComment;

    private CreateCommentView CreateCommentView;

    public ManageCommentView(CommentInterface CommentInterface)
    {
        this.CommentInterface = CommentInterface;
        this.CreateCommentView = new CreateCommentView(CommentInterface);
        this.ViewSingleComment = new ViewSingleComment(CommentInterface);
    }
    public async Task ShowMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Manage Comment ===");
            Console.WriteLine("1. Create Comment");
            Console.WriteLine("2. View Single Comment Using Id");
            Console.WriteLine("0. Back");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await CreateCommentView.ShowAsync();
                    break;
                case "2":
                    await ViewSingleComment.ShowAsync();
                    break;
                case "0":
                    return;
            }
        }
    }
}