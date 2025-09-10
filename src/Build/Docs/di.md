# Dependency Injection Setup

## Register Services
- Register all services, repositories, and API clients in the DI container (e.g., in `Program.cs` or `Startup.cs`).

## Example
```csharp
// Services
services.AddScoped<IProjectRepository, ProjectRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IMappingApiClient, MappingApiClient>();
services.AddScoped<ISchedulingApiClient, SchedulingApiClient>();
// ...other services
```

- Use appropriate lifetimes (Scoped, Singleton, Transient) as needed.
