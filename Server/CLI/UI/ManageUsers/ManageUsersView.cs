using System;
using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI.ManageUser;

public class ManageUserView
{
    private UserInterface InMemoryUserRepository;
    private CreateUserView createUserView;
    private ListUsersView listUserView;

    public ManageUserView(UserInterface InMemoryUserRepository)
    {
        this.InMemoryUserRepository = InMemoryUserRepository;
        this.createUserView = new CreateUserView(InMemoryUserRepository);
        this.listUserView = new ListUsersView(InMemoryUserRepository);
    }


    public async Task ShowMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Manage User ===");
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. List Users");
            Console.WriteLine("0. Back");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    await createUserView.AddUser();
                    break;
                case "2":
                    await listUserView.ShowAsync();
                    break;
                case "0":
                    return;
            }
        }
    }


}
