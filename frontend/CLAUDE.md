# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Repository Structure

Monorepo with two independent projects:

- **`backend/TaskManagementSystem/TaskManagementSystem/`** — ASP.NET Core 9.0 Web API (C#)
- **`frontend/task-management-ui/`** — Next.js 16 app (React 19, JavaScript)

The frontend runs on port 3000, the backend on port 5271. They communicate via REST API with JWT authentication.

## Development Commands

### Frontend (`frontend/task-management-ui/`)
```
npm run dev       # Start dev server (port 3000)
npm run build     # Production build
npm run lint      # ESLint
```

### Backend (`backend/TaskManagementSystem/TaskManagementSystem/`)
```
dotnet run                    # Start API server (port 5271)
dotnet build                  # Build project
dotnet ef database update     # Apply EF Core migrations
dotnet ef migrations add <Name>  # Create new migration
```

## Architecture

### Backend

- **Controllers**: `AuthController`, `TaskController`, `UserController` — all under `/api/{controller}`
- **Models**: `User` and `TaskItem` entities with EF Core, SQL Server (LocalDB with Windows Auth)
- **DTOs**: Separate request/response objects in `DTOs/` folder (LoginDTO, UserDTO, TaskDTO, CreateUserDTO, TaskStatusDTO, LoginResponseDTO)
- **Context**: `AppDbContext` with seed data in `Context/Seed/` (2 admins, 10 users, 25 tasks)
- **Auth**: JWT Bearer with HS256. Config in `appsettings.json` under `JwtConfig`. BCrypt.Net is installed but password hashing is not yet fully implemented.
- **Roles**: `RoleId 0` = Admin, `RoleId 1` = User. Admin-only endpoints use `[Authorize(Roles = "Admin")]`.
- **CORS**: Configured for `http://localhost:3000` only.

### Frontend

- **Next.js App Router** with page-based routing under `src/app/`
- **Routing**: `/login` (public), `/admin-ui/*` (admin dashboard, tasks, users, settings), `/users-ui/*` (user dashboard, tasks)
- **Auth pattern**: JWT token and user object stored in `localStorage` (`"token"`, `"user"` keys). Role-based redirect after login (roleId 0 → admin, 1 → user).
- **API calls**: All use `fetch()` with `Authorization: Bearer ${token}` header. Base URL from `NEXT_PUBLIC_API_URL` env var (`.env.local`).
- **Styling**: Tailwind CSS v4 with PostCSS
- **Charts**: Chart.js + react-chartjs-2 for admin dashboard (pie chart for task distribution, bar chart for tasks per user)

### API Endpoints

| Method | Endpoint | Auth | Admin Only |
|--------|----------|------|-----------|
| POST | `/api/Auth/login` | No | No |
| GET/POST | `/api/Task` | Yes | POST only |
| GET/PUT/DELETE | `/api/Task/{id}` | Yes | PUT/DELETE only |
| PUT | `/api/Task/{id}/status` | Yes | No |
| GET/POST | `/api/User` | Yes | Yes |
| GET/PUT/DELETE | `/api/User/{id}` | Yes | PUT/DELETE only |

### Seed Credentials
- Admin: `admin@company.com` / `admin123`
- Users follow pattern: `{name}.{surname}@company.com` / `{name}123`
