using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using ApiContracts.UserFolder;
using Microsoft.AspNetCore.Components.Authorization;
using ApiContracts;
using Microsoft.JSInterop;

namespace BlazorApp.Auth;

public class SimpleAuthProvider : AuthenticationStateProvider
{
    private readonly HttpClient httpClient;
    private readonly IJSRuntime jSRuntime;
    private ClaimsPrincipal? currentClaimsPrincipal;
    public SimpleAuthProvider(HttpClient httpClient, IJSRuntime jSRuntime)
    {
        this.httpClient = httpClient;
        this.jSRuntime = jSRuntime;
         }

    public string errorMessage { get; private set; }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        string userAsJson = "";
        try
        {
            userAsJson = await jSRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
        }
        catch (InvalidOperationException e)
        {
            return new AuthenticationState(new());
        }
        if (string.IsNullOrEmpty(userAsJson))
        {
            return new AuthenticationState(new());
        }
        UserDto userDto = JsonSerializer.Deserialize<UserDto>(userAsJson)!; 
                   List<Claim> claims = new List<Claim>()
                   {
                    new Claim(ClaimTypes.Name, userDto.Username),
                    new Claim(ClaimTypes.NameIdentifier, userDto.Id.ToString()), }; ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth"); ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity); return new AuthenticationState(claimsPrincipal);

        }

    public async Task Login(string userName, int password)
    {
       HttpResponseMessage response = await httpClient.PostAsJsonAsync(
            "Auth",
            new LoginRequest(userName, password));
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
             var body = await response.Content.ReadAsStringAsync();
            errorMessage = $"Login failed: {(int)response.StatusCode} {response.StatusCode} - {body}";
            return;
        }
        UserDto userDto = JsonSerializer.Deserialize<UserDto>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
        string serialisedData = JsonSerializer.Serialize(userDto); await jSRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, userDto.Username),
            new Claim("Id", userDto.Id.ToString()),
        };
        ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth");

        currentClaimsPrincipal = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(currentClaimsPrincipal)));

    }
 public async Task Logout()
    {
        await jSRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", ""); 
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new())));
    }
    } 
    