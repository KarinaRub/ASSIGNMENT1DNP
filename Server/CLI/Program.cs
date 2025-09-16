using CLI.UI;
using InMemoryRepositories;
using InMemoryRepository;
using RepositoryContracts;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.WriteLine("Starting CLI app ...");
        UserInterface userRepository = new UserInMemoryRepository();
        CommentInterface commentRepository = new CommentInMemoryRepository();
        PostInterface postRepository = new PostInMemoryRepository();

        CliApp cliApp = new CliApp(commentRepository, userRepository, postRepository );
        await cliApp.StartAsync();
    }
}