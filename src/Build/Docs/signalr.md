# SignalR Real-time Notifications

## Overview
SignalR enables real-time web functionality for live notifications (e.g., project/task updates).

## Backend Setup
- Add Microsoft.AspNetCore.SignalR NuGet package.
- Create a NotificationHub class.
- Register SignalR in Startup/Program:
  ```csharp
  builder.Services.AddSignalR();
  app.MapHub<NotificationHub>("/hubs/notifications");
  ```
- Inject IHubContext<NotificationHub> to send notifications from services/handlers.

## Frontend Setup
- Use @microsoft/signalr JavaScript client.
- Connect to `/hubs/notifications` and handle events.

## Example Notification
```csharp
await _hubContext.Clients.All.SendAsync("ProjectUpdated", projectId);
```
