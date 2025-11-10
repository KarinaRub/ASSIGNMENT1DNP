using BlazorApp1;
using BlazorApp1.Services;

var builder = WebApplication.CreateBuilder(args);

// Register HttpClient-based services (BaseAddress must match your Web API)
builder.Services.AddHttpClient<IUserService, HttpUserService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7005"); // <-- put your Web API HTTPS url
});

builder.Services.AddHttpClient<IPostService, HttpPostService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7005");
});

builder.Services.AddHttpClient<ICommentService, HttpCommentService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7005");
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
