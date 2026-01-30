# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

TaskManagementSystem is an ASP.NET Core 9.0 Web API project built for managing users and tasks. The system includes JWT authentication, CRUD operations, search/filtering capabilities, and Excel export functionality using OpenXML.

## Technology Stack

- **Framework**: ASP.NET Core 9.0 (Web API)
- **Target Framework**: .NET 9.0
- **Database**: Entity Framework Core (database provider to be configured)
- **Authentication**: JWT (JSON Web Tokens)
- **Excel Export**: OpenXML (DocumentFormat.OpenXml)
- **API Documentation**: OpenAPI/Swagger

## Project Structure

```
TaskManagementSystem/
├── Models/           # Entity models (User, Task)
├── Controllers/      # API controllers
├── Services/         # Business logic layer
├── Data/            # DbContext and database configuration
├── DTOs/            # Data Transfer Objects
├── Repositories/    # Data access layer (optional pattern)
├── Middleware/      # Custom middleware (auth, error handling)
└── Program.cs       # Application entry point
```

## Core Entities

### User Entity
- User authentication and management
- Properties: Id, Username, Email, Password (hashed), Role, etc.

### Task Entity
- Task management with CRUD operations
- Properties: Id, Title, Description, Status, Priority, AssignedUserId, CreatedDate, DueDate, etc.
- Relationship: Many tasks to one user (assigned user)

## Development Commands

### Build and Run
```bash
# Build the solution
dotnet build

# Run the application
dotnet run

# Run with hot reload (watch mode)
dotnet watch run

# Build for release
dotnet build -c Release
```

### Database Management
```bash
# Add a new migration
dotnet ef migrations add <MigrationName>

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove

# Drop database
dotnet ef database drop
```

### Package Management
```bash
# Add package
dotnet add package <PackageName>

# Restore packages
dotnet restore
```

### Testing
```bash
# Run all tests (when test project is added)
dotnet test

# Run specific test
dotnet test --filter "FullyQualifiedName~Namespace.ClassName.MethodName"
```

## Key Implementation Requirements

### Authentication & Authorization
- JWT token-based authentication
- Login endpoint returns access token
- Protected endpoints require valid JWT token
- Token validation middleware configured in Program.cs

### API Endpoints Structure
- **Auth**: `/api/auth/login`, `/api/auth/register`
- **Users**: `/api/users` - CRUD operations for users
- **Tasks**: `/api/tasks` - CRUD operations for tasks
- **Reports**: `/api/reports/export` - Excel export functionality

### Search & Filtering
- All search and filter operations must be performed at the database level (server-side) using LINQ/EF Core queries
- Avoid client-side filtering for performance and scalability
- Implement query parameters for filtering (status, priority, date ranges, assigned user, etc.)

### Excel Export
- Use DocumentFormat.OpenXml library for Excel generation
- Export tasks and related data to .xlsx format
- Implement in a dedicated service/controller for reporting

### Data Seeding
- Create seed data for development/demonstration purposes
- Include sample users and tasks
- Implement in DbContext.OnModelCreating or separate seeder class

## Architecture Patterns

### Layered Architecture
The application follows a layered architecture:
1. **Controllers**: Handle HTTP requests/responses, route to services
2. **Services**: Contain business logic, orchestrate operations
3. **Repositories**: Data access abstraction (if pattern is used)
4. **Data Layer**: EF Core DbContext and entity configurations

### Dependency Injection
- Register services in Program.cs using builder.Services
- Use constructor injection in controllers and services
- Prefer interfaces for testability (IUserService, ITaskService, etc.)

## Configuration

### appsettings.json Structure
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "..."
  },
  "JwtSettings": {
    "Secret": "...",
    "Issuer": "...",
    "Audience": "...",
    "ExpirationInMinutes": 60
  },
  "Logging": { ... },
  "AllowedHosts": "*"
}
```

## API Response Format

Standardize API responses:
```json
{
  "success": true/false,
  "data": { ... },
  "message": "...",
  "errors": [ ... ]
}
```

## Error Handling

- Implement global exception handling middleware
- Return consistent error responses
- Log errors appropriately
- Use appropriate HTTP status codes (200, 201, 400, 401, 404, 500)

## Security Considerations

- Password hashing using BCrypt or ASP.NET Core Identity password hasher
- Validate input data using Data Annotations or FluentValidation
- Implement authorization policies for role-based access
- Secure sensitive configuration (JWT secrets, connection strings)

## Solution File Location

The solution file is located at: `../TaskManagementSystem.sln` (parent directory)

## Working Directory

The main project directory is: `TaskManagementSystem/` (subdirectory of solution root)
