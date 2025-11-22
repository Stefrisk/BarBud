using BarBud.Components;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using BarBud;

using Microsoft.EntityFrameworkCore.SqlServer;
using BarBud.Models;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add DbContext for Azure SQL
builder.Services.AddDbContext<BarBudDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BarBudDb")));

// Add Identity service and set Identity information stores

builder.Services.AddDefaultIdentity<BarBud.Models.User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<BarBudDbContext>();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapRazorPages();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
