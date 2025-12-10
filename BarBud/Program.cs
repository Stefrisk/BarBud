using BarBud.Components;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using BarBud.Db;
using BarBud.Models;
using BarBud.Components.Account;
using BarBud.Services;
using BarBud.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

builder.Services.AddDbContext<BarBudDbContext>(options =>
    options.UseSqlite("Data Source=barbud.db"));

// Add services to the container.
builder.Services.AddScoped<IIngredientServices, IngredientFunctions>();
builder.Services.AddScoped<IDrinkServices, DrinkFunctions>();
builder.Services.AddScoped<IRecipeServices, RecipeFunctions>();
builder.Services.AddScoped<IRecipeBuilder, RecipeBuilder>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRazorPages();


var app = builder.Build();

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


// Apply migrations and seed data
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BarBudDbContext>();
    db.Database.Migrate();
    DbInitializer.Seed(db);
}

app.Run();