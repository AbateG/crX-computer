# CR COMPUTER

A modular, scalable system for managing projects, tasks, and resources.

## Features
- RESTful API (ASP.NET Core)
- Clean architecture (Domain, Application, Infrastructure, API)
- Entity Framework Core migrations
- Dependency Injection
- Test projects (unit, integration, E2E)

## Getting Started

### Prerequisites
- .NET 8 SDK
- SQL Server (or update connection string in `appsettings.json`)

### Setup
1. Clone the repository:
   ```sh
   git clone <repo-url>
   cd "CR COMPUTER"
   ```
2. Restore dependencies:
   ```sh
   dotnet restore
   ```
3. Apply database migrations:
   ```sh
   dotnet ef database update --project src/Infrastructure/Infrastructure.csproj
   ```
4. Run the API:
   ```sh
   dotnet run --project src/Api/Api.csproj
   ```

### Running Tests
- Unit/Integration:
  ```sh
  dotnet test
  ```
- E2E (Cypress):
  ```sh
  cd src/Tests/E2E
  npx cypress open
  ```

## Deployment
- See `Build/Docs/deployment.md` for deployment steps.
- Example (Azure App Service):
  1. Publish the API project:
     ```sh
     dotnet publish src/Api/Api.csproj -c Release -o ./publish
     ```
  2. Deploy contents of `./publish` to your web server.

## Documentation
- API: `Build/Docs/api-docs.md`
- Architecture: `Build/Docs/architecture.md`
- Setup: `Build/Docs/setup.md`
- CI/CD: `Build/Docs/ci-cd.md`
- Migrations: `Build/Docs/migrations.md`
- Test Strategy: `Build/Docs/test-strategy.md`

## License
See [LICENSE](../LICENSE).

## Support
See [SUPPORT.md](../SUPPORT.md).

## Privacy
See [PRIVACY.md](../PRIVACY.md).
