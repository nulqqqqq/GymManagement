# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

Single-project ASP.NET Core Web API (.NET 9, `GymManagement.Api/`) for managing gym clients, trainers and workout sessions. PostgreSQL via EF Core (Npgsql). There is no test project and no frontend in this repo.

## Commands

Run from the repo root (where `GymManagement.sln` lives):

```bash
dotnet build
dotnet run --project GymManagement.Api            # http://localhost:5230, Swagger UI at /swagger (Development only)
dotnet run --project GymManagement.Api --launch-profile https   # https://localhost:7141

# EF Core migrations (requires dotnet-ef tool)
dotnet ef migrations add <Name> --project GymManagement.Api
dotnet ef database update --project GymManagement.Api
```

Requires a local PostgreSQL matching `ConnectionStrings:DefaultConnection` in `appsettings.json` (database `gym_db`).

## Architecture

Request flow: `Controllers/` → `Interfaces/I*Service` → `Services/*Service` → `Data/AppDbContext`. Controllers never touch the DbContext directly; services return DTOs (never entities), mapping via AutoMapper.

- **DI registration is manual** in `Program.cs` — every new service must be added there with `AddScoped<IFoo, Foo>()`. AutoMapper profiles and FluentValidation validators are discovered by assembly scan.
- **DTOs** live in `Dtos/<Entity>/` as `Create*Dto`, `Update*Dto`, `*ResponseDto`, `*QueryDto`. Note that client DTOs use namespace `GymManagement.Api.Dtos` (not `.Dtos.Clients`), unlike trainers/workout sessions.
- **Mappings**: all AutoMapper maps are in the single `Mappings/MappingProfile.cs`. Updates use `_mapper.Map(updateDto, entity)` onto a tracked entity.
- **Validation**: FluentValidation auto-validation (`Validators/`) runs before controller actions and returns 400 automatically — no manual `ModelState` checks.
- **Listing/pagination**: `*QueryDto` inherits `Dtos/Shared/PaginationQueryDto` (PageNumber, PageSize, SortColumn, SortOrder). Services build an `IQueryable`, apply filters, then whitelist sort columns with explicit `if` branches, then `Skip/Take`.
- **Soft delete**: `Client` and `Trainer` have `IsDeleted`; delete endpoints set the flag instead of removing rows. Only `Client` has a global query filter (`HasQueryFilter` in `AppDbContext`) — Trainer queries must filter `IsDeleted` manually. Use `IgnoreQueryFilters()` to see deleted clients (see `StatsService`).
- **Errors**: `Middleware/ExceptionMiddleware` catches unhandled exceptions and returns a JSON 500. "Not found" is signalled by services returning `null`/`false`, and controllers translate that into 404.
- **External integrations**: `EmailService` (MailKit/SMTP, `EmailSettings`) sends a welcome email on client creation; `PhotoService` (Cloudinary, `CloudinarySettings`) handles avatar uploads, storing `PhotoUrl` + `PublicId` on the entity.
- **Auth (in progress)**: JWT bearer is configured in `Program.cs` from `JwtSettings`; `[Authorize]` is currently only on `StatsController`. `AuthController.Login` issues a hard-coded admin token. `TokenService` does not yet implement `ITokenService` and is not registered in DI, so `/api/auth/login` fails at runtime until that is fixed. The `User` model/table exists (BCrypt is referenced) but is not wired up yet.
- **Logging**: Serilog to console and rolling files in `GymManagement.Api/logs/`.
- Primary keys are `Guid` everywhere. XML doc comments on controller actions feed Swagger (`GenerateDocumentationFile` is on; warning 1591 suppressed).

## Conventions

- Code comments are often in Russian; keep that style when editing nearby code.
- Client `Plan` must be one of `Basic`, `Premium`, `Elite` (enforced in validators).
