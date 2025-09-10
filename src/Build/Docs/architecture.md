# Architecture Overview

This document provides an overview of the CR COMPUTER system architecture.

## Layers
- **Api**: Handles HTTP requests, authentication, and routing.
- **Application**: Contains business logic, commands, queries, and handlers.
- **Domain**: Core entities, value objects, enums, and domain events.
- **Infrastructure**: Data access, external services, background jobs, and API clients.
- **WebApp**: Frontend web application (MVC or SPA).
- **MobileApp**: Mobile application components.

## Key Technologies
- ASP.NET Core, Entity Framework Core, SignalR, Azure, SQL Server, Cypress, GitHub Actions.

## Communication
- RESTful APIs
- SignalR for real-time updates

## Testing
- Unit, Integration, and E2E tests

## Deployment
- Azure App Service, SQL Database, Key Vault
