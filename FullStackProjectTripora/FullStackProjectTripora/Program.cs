using FullStackProjectTripora.Components;
using Microsoft.EntityFrameworkCore;
using FullStackProjectTripora.Data; // change this if your DbContext namespace is different

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// ✅ Add DbContext BEFORE builder.Build()
builder.Services.AddDbContext<TriporaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TriporaDb")));

var app = builder.Build();

// Configure the HTTP request pipeline.
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
