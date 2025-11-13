using System;

namespace ApiContracts;

public class LoginRequest
{
    public LoginRequest(string UserName, int password)
    {
        this.UserName = UserName;
        this.Password = password;
    }

    public string? UserName { get; set; }
    public int Password { get; set; }
    
}
