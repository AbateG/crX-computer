# Setup Guide

## Prerequisites
- .NET 8 SDK
- Node.js & npm (for WebApp and Cypress)
- SQL Server
- Azure account (for deployment)

## Local Development
1. Clone the repository.
2. Restore NuGet packages: `dotnet restore`
3. Set up SQL Server and update connection string in `appsettings.Development.json`.
4. Run EF Core migrations: `dotnet ef database update`
5. Configure SMTP, JWT secrets, and API keys in `appsettings.Development.json`.
6. Start the backend: `dotnet run --project src/Api`
7. Start the frontend: `npm install && npm start` in `src/WebApp`.

## Testing
- Run unit/integration tests: `dotnet test`
- Run E2E tests: `npx cypress open` in `src/Tests/E2E`

## Deployment
- See `deployment.md` for Azure deployment steps.
