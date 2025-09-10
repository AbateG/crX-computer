# Database Migrations (EF Core)

## Setup
- Ensure Entity Framework Core tools are installed: `dotnet tool install --global dotnet-ef`
- Add EF Core packages to your project if not already present.

## Creating a Migration
```
dotnet ef migrations add InitialCreate --project src/Infrastructure --startup-project src/Api
```

## Applying Migrations
```
dotnet ef database update --project src/Infrastructure --startup-project src/Api
```

## Notes
- Migrations are stored in `src/Infrastructure/Migrations`.
- Update your connection string in `appsettings.Development.json` as needed.
