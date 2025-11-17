# .NET 10 Upgrade Plan

## Execution Steps

Execute steps below sequentially one by one in the order they are listed.

1. Validate that an .NET 10 SDK required for this upgrade is installed on the machine and if not, help to get it installed.
2. Ensure that the SDK version specified in global.json files is compatible with the .NET 10 upgrade.
3. Upgrade BarBud\BarBud.csproj

## Settings

This section contains settings and data used by execution steps.

### Excluded projects

Table below contains projects that do belong to the dependency graph for selected projects and should not be included in the upgrade.

| Project name                                   | Description                 |
|:-----------------------------------------------|:---------------------------:|

### Aggregate NuGet packages modifications across all projects

NuGet packages used across all selected projects or their dependencies that need version update in projects that reference them.

| Package Name                        | Current Version | New Version | Description                                   |
|:------------------------------------|:---------------:|:-----------:|:----------------------------------------------|
| Microsoft.EntityFrameworkCore       |   9.0.0         |  10.0.0     | Recommended for .NET 10                       |
| Microsoft.EntityFrameworkCore.Sqlite|   9.0.0         |  10.0.0     | Recommended for .NET 10                       |
| Microsoft.EntityFrameworkCore.SqlServer |   9.0.0    |  10.0.0     | Recommended for .NET 10                       |

### Project upgrade details
This section contains details about each project upgrade and modifications that need to be done in the project.

#### BarBud\BarBud.csproj modifications

Project properties changes:
  - Target framework should be changed from `net9.0` to `net10.0`

NuGet packages changes:
  - Microsoft.EntityFrameworkCore should be updated from `9.0.0` to `10.0.0` (recommended for .NET 10)
  - Microsoft.EntityFrameworkCore.Sqlite should be updated from `9.0.0` to `10.0.0` (recommended for .NET 10)
  - Microsoft.EntityFrameworkCore.SqlServer should be updated from `9.0.0` to `10.0.0` (recommended for .NET 10)

Feature upgrades:
  - No specific feature upgrades detected.

Other changes:
  - None
