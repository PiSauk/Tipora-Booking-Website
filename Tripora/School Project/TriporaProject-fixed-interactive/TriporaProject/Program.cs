using Microsoft.EntityFrameworkCore;
using TriporaProject.Components;
using TriporaProject.Models;
using TriporaProject.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

var connStr = builder.Configuration.GetConnectionString("TriporaDb");
if (string.IsNullOrWhiteSpace(connStr))
{
    throw new InvalidOperationException("Connection string 'TriporaDb' not found in appsettings.json.");
}


builder.Services.AddDbContext<TriporaDbContext>(options =>
    options.UseSqlServer(connStr));

builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
