# .NET 10 Upgrade Report

## Project target framework modifications

| Project name           | Old Target Framework | New Target Framework | Commits                   |
|:-----------------------|:--------------------:|:--------------------:|---------------------------|
| BarBud\BarBud.csproj   | net9.0               | net10.0              | 1aa678d3, cad88275        |

## NuGet Packages

| Package Name                            | Old Version | New Version | Commit Id |
|:----------------------------------------|:-----------:|:-----------:|:----------|
| Microsoft.EntityFrameworkCore           |  9.0.0      | 10.0.0      | 36a998b2  |
| Microsoft.EntityFrameworkCore.Sqlite    |  9.0.0      | 10.0.0      | 36a998b2  |
| Microsoft.EntityFrameworkCore.SqlServer |  9.0.0      | 10.0.0      | 36a998b2  |

## All commits

| Commit ID | Description                                  |
|:----------|:---------------------------------------------|
| 1aa678d3  | Commit upgrade plan                           |
| cad88275  | Update BarBud.csproj to target .NET 10.0      |
| 36a998b2  | Update EF Core packages to v10.0.0 in BarBud.csproj |

## Next steps

- Build and run the solution to verify runtime behavior.
- Validate Blazor MudBlazor UI with .NET 10 SDK locally.
- If you have deployment pipelines, update the SDK/runner images to .NET 10.

