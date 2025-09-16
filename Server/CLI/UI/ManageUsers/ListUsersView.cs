using System;
using RepositoryContracts;

namespace CLI.UI.ManageUser;

public class ListUserView
{
    private UserInterface userInterface;

    public ListUserView(UserInterface userInterface)
    {
        this.userInterface = userInterface;
    }
     public async Task ShowAsync()
    {
        var users = userInterface.GetManyAsync();
        Console.WriteLine("\n=== All Users ===");
        foreach (var user in users)
        {
            Console.WriteLine($"ID: {user.Id}, Username: {user.Username}");
        }
    }
}
