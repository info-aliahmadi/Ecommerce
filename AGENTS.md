# AGENTS.md

## Build & run

```bash
dotnet restore Hydra.slnx
dotnet build Hydra.slnx
dotnet run --project Hydra.Web
```

Swagger available at `/swagger` in development. App requires PostgreSQL (default: `localhost:5432`, database `HydraEcommerce`).

## Solution structure

Solution file is `Hydra.slnx` (XML format, not traditional `.sln`). Central package management in `Directory.Packages.props`.

```
Hydra.Web/                 ← ASP.NET Core host (entrypoint, references all module Api projects)
Hydra.Infrastructure/      ← DbContext, Identity/JWT, module discovery, caching, logging, localization
Hydra.Kernel/              ← Shared primitives, helpers, claim types, base entities
Hydra.Ecommerce.Core/      ← Shared ecommerce domain (permissions, addresses, currencies, taxes)
Hydra.*.Api/               ← Module endpoints (Minimal API handlers + IModule implementation)
Hydra.*.Core/              ← Module domain (entities, interfaces, models, EF configurations)
```

## Module system

Modules are auto-discovered via assembly scanning. Two independent discovery paths:

1. **Endpoints**: `ModuleExtensions.DiscoverModules()` scans DLLs matching `Hydra*Api` for `IModule` implementations. Each module registers DI services (`RegisterModules`) and maps routes (`MapEndpoints`).

2. **EF configurations**: `ApplicationDbContext.OnModelCreating` scans `Hydra*Core` assemblies for `IEntityTypeConfiguration<T>` implementations.

**To add a new module**: Create `Hydra.YourModule.Api` and `Hydra.YourModule.Core` projects, implement `IModule` in the Api project, and add a `<ProjectReference>` from `Hydra.Web` to `Hydra.YourModule.Api`. Without the Web reference, the module won't be discovered.

## All endpoints require auth by default

`MapModulesEndpoints()` wraps every module in `.RequireAuthorization()`. Public endpoints must explicitly call `.AllowAnonymous()`:

```csharp
endpoints.MapGet(API_SCHEMA + "/PublicRoute", Handler.Method).AllowAnonymous();
endpoints.MapGet(API_SCHEMA + "/AdminRoute", Handler.Method).RequirePermission(EcommercePermissionTypes.SOME_PERM);
```

## Handler pattern

Handlers are **static classes with static methods**. Services and `ClaimsPrincipal` are injected via method parameters:

```csharp
public static async Task<IResult> GetItem(IService service, int id) { ... }
public static async Task<IResult> CreateItem(ClaimsPrincipal user, IService service, [FromBody] Model model) { ... }
```

Get the current user ID via `userClaim.GetUserId()` (from `Hydra.Kernel`).

## Key commands

```bash
# EF migrations (always specify both projects)
dotnet ef migrations add <Name> --project Hydra.Infrastructure --startup-project Hydra.Web
dotnet ef database update --project Hydra.Infrastructure --startup-project Hydra.Web

# User secrets for local dev
cd Hydra.Web
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=HydraEcommerce"
```

## Gotchas

- **Dockerfile is stale**: References .NET 7, `Hydra.sln` (not `.slnx`), and non-existent `Hydra.Migrations` project. Do not use as-is.
- **ShoppingCart module not wired**: `Hydra.ShoppingCart.Api` exists on disk but has no `<ProjectReference>` in `Hydra.Web.csproj`. It won't load at runtime.
- **No tests**: No test projects or CI workflows exist. Build verification is `dotnet build Hydra.slnx` only.
- **Permission constants** live in `Hydra.Ecommerce.Core/Constants/SalePermissionTypes.cs` and module-specific `Seed/*PermissionConfiguration.cs` files.
- **appsettings.Development.json** contains hardcoded SMTP/SMS test credentials — do not treat as real secrets.
