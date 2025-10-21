using CLI.UI;
using FileRepository;
using RepositoryContracts;


Console.WriteLine("Starting CLI app.....");
UserInterface userInterface = new UserFileRepository();
CommentInterface commentInterface = new CommentFileRepository();
PostInterface postinterface = new PostFileRepository();

CliApp cliApp = new CliApp(commentInterface, userInterface, postinterface);
await cliApp.StartAsync();