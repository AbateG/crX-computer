# API Documentation

## Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration

## Projects
- `GET /api/projects` - List projects
- `GET /api/projects/{id}` - Get project by ID
- `POST /api/projects` - Create project
- `PUT /api/projects/{id}` - Update project
- `DELETE /api/projects/{id}` - Delete project

## Tasks
- `GET /api/projects/{projectId}/tasks` - List tasks for a project
- `POST /api/projects/{projectId}/tasks` - Create task

## Real-time
- SignalR hub: `/hubs/notifications` (for live updates)

## More endpoints...
- Expand as needed for your implementation
