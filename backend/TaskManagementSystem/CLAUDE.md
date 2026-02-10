# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

ASP.NET Core 9.0 Web API for task management with JWT auth, role-based access (Admin/User), CRUD operations, and document export (Excel/Word/PDF). Frontend is a Next.js app at `../../frontend/task-management-ui/` running on `http://localhost:3000`.

## Build and Run

All commands run from the project directory (`TaskManagementSystem/TaskManagementSystem/`):

```bash
dotnet build                              # Build
dotnet run                                # Run (HTTP: localhost:5271, HTTPS: localhost:7129)
dotnet watch run                          # Run with hot reload
```

Solution file is one level up: `../TaskManagementSystem.sln`

### Database (SQL Server)

Connection: `Server=.;Database=TaskManagementDb;Trusted_Connection=True;TrustServerCertificate=True`

```bash
dotnet ef migrations add <Name>           # New migration
dotnet ef database update                 # Apply migrations
```

Data is seeded automatically via `Seeds/UserSeed.cs` (12 users) and `Seeds/TaskSeed.cs` (40 tasks).

## Architecture

Controllers → Services → DbContext (no repository pattern). Services are registered in `Program.cs` via DI.

### Project Layout

```
TaskManagementSystem/
├── Controllers/      # AuthController, UserController, TaskController, ReportController
├── Context/          # TaskManagementDbContext (EF Core, SQL Server)
├── Models/           # User, Task, FisData entities
├── DTOs/             # UserDTO, CreateUserDTO, TaskDTO, TaskStatusDTO, LoginDTO, LoginResponseDTO
├── Services/         # ExcelService, WordService, PdfService
├── Seeds/            # UserSeed, TaskSeed (test data)
├── Helpers/          # ExcelHelpers (cell parsing utilities)
├── Fonts/            # arial.ttf (used by PdfService)
├── Migrations/       # EF Core migrations
└── Program.cs        # Entry point, JWT config, CORS, DI setup
```

### Entities

- **User**: `UserId`, `RoleId` (0=Admin, 1=User), `Name`, `Surname`, `Email`, `PasswordHash`, `UserCreatedAt`
- **Task**: `TaskId`, `Title`, `Description`, `Status` (ToDo/InProgress/Testing/Done/OnHold), `Priority` (Low/Medium/High/Critical), `AssignedUserId`, `CreatedByAdminId`, `DueDate`, `TaskCreatedAt`
- **FisData**: Financial/accounting records with `FisNo`, `FisTarih`, `HesapKodu`, `HesapAdi`, `Aciklama`, `FaturaNo`, `Borc`, `Alacak`

### API Routes

| Route | Auth | Description |
|-------|------|-------------|
| `POST /api/auth/login` | None | Returns JWT token + user info |
| `GET/POST/PUT/DELETE /api/users` | Admin | User CRUD (GET by id allows any authenticated user) |
| `GET/POST/PUT/DELETE /api/tasks` | Admin for CUD, any auth for Read | Task CRUD |
| `PUT /api/tasks/{id}/status` | Any auth | Update task status only |
| `POST /api/report/upload` | Admin | Upload Excel file (FisData) |
| `GET /api/report/exportFisData` | Admin | Export FisData as .xlsx |
| `GET /api/report/exportFisDataAsWord` | Admin | Export FisData as .docx |
| `GET /api/report/exportFisDataAsPdf` | Admin | Export FisData as .pdf |
| `GET /api/report/listFisData` | Admin | Paginated FisData list (query: page, pageSize) |
| `GET /api/report/export` | Admin | Export tasks as .xlsx with optional charts (query: statusChart, priorityChart, taskByUserChart) |

### JWT Configuration

Claims: `NameIdentifier` (UserId), `Email`, `Name` (full name), `Role` ("Admin" or "User"). Token expiry: 1440 minutes (24h). CORS allows `http://localhost:3000`.

### Document Export Services

- **ExcelService**: Generic `GenerateReport<T>()` using OpenXML with optional doughnut/bar charts via `ChartRequest`
- **WordService**: Generates .docx tables from FisData using OpenXML
- **PdfService**: Generates PDF using Syncfusion.Pdf (requires `Fonts/arial.ttf` for Turkish character support)
- **ExcelReadFisData**: Reads uploaded Excel files into FisData objects
- **ExcelTableComponent/ExcelChartComponent**: Static helpers for OpenXML table and chart generation

### Key Dependencies

- `Microsoft.EntityFrameworkCore.SqlServer` - Database
- `Microsoft.AspNetCore.Authentication.JwtBearer` - Auth
- `DocumentFormat.OpenXml` - Excel/Word generation
- `Syncfusion.Pdf.Net.Core` - PDF generation (license key set in Program.cs)
- `BCrypt.Net-Next` - Available but not currently used for password hashing

### Design Constraints

- All search/filter operations must use server-side LINQ/EF Core queries, not client-side filtering
- Task default status is "ToDo", default priority is "Medium"
- Foreign keys on Task use `DeleteBehavior.Restrict`
- `UserCreatedAt` defaults via SQL `GETDATE()`
