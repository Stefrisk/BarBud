# Database Setup Guide

## Overview
This project uses **Entity Framework Core** with **SQLite** for local development. The database includes:
- **Identity tables** (AspNetUsers, AspNetRoles, etc.) for authentication
- **Cocktail-related tables** (Cocktails, Recipes, Ingredients)

## Prerequisites
- .NET SDK installed
- `dotnet-ef` tool (installed locally in the project)

## For New Team Members

### 1. Clone the Repository
```bash
git clone <your-repo-url>
cd BarBud
```

### 2. Restore Tools
The project uses a local `dotnet-ef` tool. Restore it:
```bash
cd BarBud
dotnet tool restore
```

### 3. Create Your Local Database
Run the migrations to create your local `barbud.db` file:
```bash
dotnet tool run dotnet-ef database update --context BarBudDbContext
```

This will create the SQLite database file at `BarBud/barbud.db` with all tables.

### 4. Run the Application
```bash
cd ..
dotnet run --project BarBud/BarBud.csproj
```

## Database File Location
- **Path**: `BarBud/barbud.db`
- **⚠️ Important**: This file is in `.gitignore` and should **NOT** be committed to git
- Each developer creates their own local database by running migrations

## Making Database Changes

### Add a New Migration
When you modify entity models (add/remove properties, new tables, etc.):

```bash
cd BarBud
dotnet tool run dotnet-ef migrations add YourMigrationName --context BarBudDbContext
```

### Apply Migrations
```bash
dotnet tool run dotnet-ef database update --context BarBudDbContext
```

### Remove Last Migration (if not yet applied)
```bash
dotnet tool run dotnet-ef migrations remove --context BarBudDbContext
```

## About DesignTimeDbContextFactory
The `DesignTimeDbContextFactory.cs` file is **necessary** for EF Core tools to work. It tells `dotnet-ef` how to create your DbContext when running migrations. **Don't delete it.**

## Sharing Changes with the Team

### ✅ DO Commit:
- Migration files in `BarBud/Migrations/`
- Changes to entity models in `BarBud/Models/`
- Changes to `BarBudDbContext.cs`
- This README and setup documentation

### ❌ DON'T Commit:
- `BarBud/barbud.db` (the actual database file)
- `BarBud/barbud.db-shm`
- `BarBud/barbud.db-wal`

These are already in `.gitignore`.

## Troubleshooting

### "Could not execute dotnet-ef"
Run `dotnet tool restore` in the `BarBud/` directory.

### "No DbContext named 'BarBudDbContext' was found"
Make sure you're running commands from the `BarBud/` directory (the one with `BarBud.csproj`).

### Database is locked or corrupted
1. Stop the running application
2. Delete `BarBud/barbud.db` (and .db-shm, .db-wal if they exist)
3. Run `dotnet tool run dotnet-ef database update --context BarBudDbContext` to recreate it

### Migrations are out of sync
If migrations conflict with your database:
1. Back up any important data
2. Delete `BarBud/barbud.db`
3. Run `dotnet tool run dotnet-ef database update --context BarBudDbContext`

## Current Schema

**Identity System:**
- Users (AspNetUsers)
- Roles (AspNetRoles)
- User Claims, Logins, Tokens, etc.

**Cocktail System:**
- **Cocktails** (Id, Name, Description)
- **Recipes** (Id, Name, Instructions) - One-to-one with Cocktails
- **Ingredients** (Id, Name, Description, Amount, RecipeId) - Many-to-one with Recipes

