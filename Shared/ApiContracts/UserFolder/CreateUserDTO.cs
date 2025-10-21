using System;

namespace ApiContracts.UserFolder;

public class CreateUserDTO
{
    public required string Username { get; set; }
    public required int Password { get; set; }
}
