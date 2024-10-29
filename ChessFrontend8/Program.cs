using ChessFrontend8.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Set the application to listen on port 8080
builder.WebHost.UseUrls("http://*:8080"); // This line binds the application to port 8080

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register HttpClient with a BaseAddress
builder.Services.AddHttpClient("ApiClient", client =>
{
    client.BaseAddress = new Uri("https://chessbackend-1-zc6m.onrender.com/"); // Replace with your actual backend URL
});

// Registering HttpClient as the default instance
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("ApiClient"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
