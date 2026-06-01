# Hydra Ecommerce

Hydra Ecommerce is a modular monolith ecommerce platform built with ASP.NET Core and Clean Architecture ideas. The project is organized around business modules such as authentication, CMS, CRM, product catalog, inventory, shopping cart, orders, payments, and file storage.

The goal of Hydra is to provide a practical backend foundation for modern ecommerce products: modular enough to grow, simple enough to run as one application, and clear enough for contributors to add new features without learning a distributed system first.

## Highlights

- **Modular monolith architecture** with independent API/Core projects per business capability.
- **ASP.NET Core Minimal APIs** with module-based endpoint discovery.
- **Clean separation of concerns** between web host, infrastructure, module APIs, module contracts, domain models, and shared kernel.
- **Entity Framework Core + PostgreSQL** with automatic entity configuration discovery from module assemblies.
- **ASP.NET Core Identity + JWT bearer authentication** for users, roles, claims, and token-based access.
- **Dynamic permission checks** through endpoint-level permission requirements.
- **Second-level EF caching** with in-memory or Redis providers.
- **Serilog logging** with configurable sinks.
- **Localization support** through shared resources.
- **Background scheduling** infrastructure powered by Hangfire packages.
- **Central package management** through `Directory.Packages.props`.

## Table of Contents

- [Architecture](#architecture)
- [Project Structure](#project-structure)
- [Modules](#modules)
- [Request Flow](#request-flow)
- [Frontend Project](#frontend-project)
- [Prerequisites](#prerequisites)
- [Configuration](#configuration)
- [Run Locally](#run-locally)
- [Database and Migrations](#database-and-migrations)
- [API Documentation](#api-documentation)
- [How to Add a Feature](#how-to-add-a-feature)
- [How to Add a New Module](#how-to-add-a-new-module)
- [Development Guidelines](#development-guidelines)
- [Contributing](#contributing)
- [Roadmap Ideas](#roadmap-ideas)

## Architecture

Hydra uses a modular monolith design. Everything runs in one ASP.NET Core host, but business capabilities are isolated into separate projects.

```text
Client / Frontend
       |
       v
Hydra.Web
  - ASP.NET Core host
  - application startup
  - middleware pipeline
  - Swagger
       |
       v
Hydra.Infrastructure
  - EF Core DbContext
  - Identity/JWT
  - module discovery
  - repositories
  - caching
  - logging
  - localization
  - email/SMS/payment integrations
       |
       v
Feature Modules
  Hydra.Auth.Api          Hydra.Auth.Core
  Hydra.Cms.Api           Hydra.Cms.Core
  Hydra.Crm.Api           Hydra.Crm.Core
  Hydra.Product.Api       Hydra.Product.Core
  Hydra.Inventory.Api     Hydra.Inventory.Core
  Hydra.ShoppingCart.Api  Hydra.ShoppingCart.Core
  Hydra.Order.Api         Hydra.Order.Core
  Hydra.Payment.Api       Hydra.Payment.Core
  Hydra.FileStorage.Api   Hydra.FileStorage.Core
  Hydra.Common.Api        Hydra.Common.Core
       |
       v
Hydra.Kernel / Hydra.Ecommerce.Core
  - shared abstractions
  - shared ecommerce entities
  - common models
  - repository contracts
```

### Key Concepts

- `Hydra.Web` is the executable application.
- `Hydra.Infrastructure` wires cross-cutting concerns and discovers modules.
- `Hydra.Infrastructure.ModuleExtension.IModule` is the contract every API module implements.
- `Hydra.*.Api` projects register module services and map Minimal API endpoints.
- `Hydra.*.Core` projects contain contracts, models, domain types, entity configurations, constants, and permissions.
- `ApplicationDbContext` applies EF Core configurations from all `Hydra*Core` assemblies automatically.
- `Hydra.Web` references API modules so they are loaded and discoverable at runtime.

## Project Structure

```text
.
├── Hydra.Web/                  # ASP.NET Core entry point
├── Hydra.Infrastructure/       # Data, auth, modules, caching, logging, integrations
├── Hydra.Kernel/               # Shared primitives, contracts, helpers, common models
├── Hydra.Ecommerce.Core/       # Shared ecommerce domain entities and permissions
├── Hydra.Auth.Api/             # Auth endpoints and API handlers
├── Hydra.Auth.Core/            # Auth domain, models, interfaces, services
├── Hydra.Cms.Api/              # CMS endpoints and services
├── Hydra.Cms.Core/             # CMS domain, models, interfaces, EF configurations
├── Hydra.Crm.Api/              # CRM endpoints and services
├── Hydra.Crm.Core/             # CRM domain, models, interfaces, EF configurations
├── Hydra.Common.Api/           # Common ecommerce endpoints
├── Hydra.Common.Core/          # Common ecommerce models/interfaces
├── Hydra.Product.Api/          # Product/catalog endpoints and services
├── Hydra.Product.Core/         # Product/catalog interfaces and models
├── Hydra.Inventory.Api/        # Inventory endpoints and services
├── Hydra.Inventory.Core/       # Inventory interfaces and models
├── Hydra.ShoppingCart.Api/     # Shopping cart endpoints and services
├── Hydra.ShoppingCart.Core/    # Shopping cart interfaces and models
├── Hydra.Order.Api/            # Order endpoints and services
├── Hydra.Order.Core/           # Order interfaces and models
├── Hydra.Payment.Api/          # Payment endpoints and services
├── Hydra.Payment.Core/         # Payment interfaces and models
├── Hydra.FileStorage.Api/      # File upload/storage endpoints and services
├── Hydra.FileStorage.Core/     # File storage models, interfaces, domain
├── Directory.Packages.props    # Central NuGet package versions
└── Hydra.slnx                  # Solution file
```

## Modules

Current business modules include:

| Module | Purpose |
| --- | --- |
| Auth | Users, roles, permissions, JWT tokens, account operations |
| CMS | Articles, pages, menus, tags, topics, slideshows, site settings |
| CRM | Messages, email inbox/outbox, subscriptions, ticket/chat domain models |
| Common | Addresses, countries, currencies, discounts, taxes, delivery dates |
| Product | Products, categories, manufacturers, product attributes, tags, reviews |
| Inventory | Product stock and inventory operations |
| ShoppingCart | Cart and cart item operations |
| Order | Orders, order items, notes, order lifecycle features |
| Payment | Payment records and payment-related endpoints |
| FileStorage | File upload, file metadata, thumbnails, and storage rules |

## Request Flow

1. A request reaches `Hydra.Web`.
2. The middleware pipeline applies HTTPS, static files, CORS, authentication, authorization, permissions, localization, and error handling.
3. `MapModulesEndpoints()` maps endpoints from every discovered `IModule`.
4. The module endpoint calls a handler.
5. The handler delegates business work to a module service.
6. Services use shared repositories and `ApplicationDbContext`.
7. Results are returned as standard Minimal API responses.

## Frontend Project

Hydra also has a companion frontend project built with Next.js. The frontend consumes these APIs and provides the user-facing ecommerce experience on top of this backend.

The Next.js frontend is also open source and ready for contributions. Contributors can help on both sides of the platform:

- Backend features, APIs, modules, permissions, integrations, and database improvements in this repository.
- Frontend pages, components, UX, API integrations, and ecommerce flows in the Next.js project.
- End-to-end improvements that connect frontend features with backend modules.

Add the frontend repository link here when publishing:

```text
Frontend: https://github.com/info-aliahmadi/Ecommerce.Next
```

## Prerequisites

- [.NET SDK 9](https://dotnet.microsoft.com/)
- PostgreSQL 12 or newer
- Optional: Redis if you want Redis-backed caching
- Optional: Docker if you want to containerize the app

## Configuration

Main configuration files are in `Hydra.Web`:

- `Hydra.Web/appsettings.json`
- `Hydra.Web/appsettings.Development.json`
- `Hydra.Web/appsettings.Production.json`

Important settings:

```jsonc
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Username=postgres;Password=1;Database=HydraEcommerce"
  },
  "Authentication": {
    "Schemes": {
      "Bearer": {
        "Secret": "replace-this-with-a-secure-secret",
        "ExpirationMinutes": 30
      }
    }
  },
  "CoreOrigin": {
    "Url": "http://localhost:3000"
  },
  "CacheProvider": "inmemory"
}
```

> Do not commit real production secrets. Use user secrets, environment variables, or your deployment platform secret store.

For local development, you can override sensitive values with user secrets:

```bash
cd Hydra.Web
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=HydraEcommerce"
dotnet user-secrets set "Authentication:Schemes:Bearer:Secret" "your-long-local-development-secret"
```

## Run Locally

Clone the repository:

```bash
git clone https://github.com/info-aliahmadi/Ecommerce
cd <your-repo>
```

Restore packages:

```bash
dotnet restore Hydra.slnx
```

Create the PostgreSQL database if it does not exist:

```bash
createdb HydraEcommerce
```

Apply migrations:

```bash
dotnet ef database update --project Hydra.Infrastructure --startup-project Hydra.Web
```

Run the API:

```bash
dotnet run --project Hydra.Web
```

Open Swagger in development:

```text
https://localhost:<port>/swagger
```

The exact port is shown in the terminal when the app starts.

## Database and Migrations

Hydra uses one database for the modular monolith.

- `Hydra.Infrastructure/Data/ApplicationDbContext.cs` is the EF Core DbContext.
- Module entity configurations live in each module's `EntityConfiguration` folder.
- `ApplicationDbContext` automatically loads configurations from `Hydra*Core` assemblies.
- Existing migrations are located in `Hydra.Infrastructure/Migrations`.

Create a migration:

```bash
dotnet ef migrations add <MigrationName> --project Hydra.Infrastructure --startup-project Hydra.Web
```

Update the database:

```bash
dotnet ef database update --project Hydra.Infrastructure --startup-project Hydra.Web
```

In production, the app currently attempts automatic migration during startup.

## API Documentation

Swagger is enabled in development. Start the app and browse to:

```text
/swagger
```

Most module endpoints require a bearer token because modules are mapped through an authorized route group. Use the Auth module to sign in/register and then authorize Swagger with:

```text
Bearer <your-jwt-token>
```

## How to Add a Feature

Use the existing module patterns as the guide. A typical feature touches these layers:

1. **Domain/entity**: Add or update entities in a Core project, usually under `Domain`.
2. **EF configuration**: Add an `IEntityTypeConfiguration<T>` class under `EntityConfiguration`.
3. **Model/DTO**: Add request/response models under `Models`.
4. **Interface**: Add service contracts under `Interfaces`.
5. **Service**: Implement business logic in the related API project's `Services` folder.
6. **Handler**: Add Minimal API handler methods in the API project's `Handler` folder.
7. **Endpoint mapping**: Register routes in the module class under `Endpoints`.
8. **Permissions**: Add constants and protect routes with `RequirePermission(...)` when needed.
9. **Migration**: Create and apply an EF Core migration if the database schema changes.

Example locations for a product catalog feature:

```text
Hydra.Product.Core/Models/
Hydra.Product.Core/Interfaces/
Hydra.Ecommerce.Core/Domain/
Hydra.Product.Api/Services/
Hydra.Product.Api/Handler/
Hydra.Product.Api/Endpoints/ProductModule.cs
```

## How to Add a New Module

Create two projects:

```text
Hydra.YourModule.Core
Hydra.YourModule.Api
```

Recommended structure:

```text
Hydra.YourModule.Core/
├── Constants/
├── Domain/
├── EntityConfiguration/
├── Interfaces/
└── Models/

Hydra.YourModule.Api/
├── Endpoints/
├── Handler/
└── Services/
```

Add project references:

- `Hydra.YourModule.Core` should reference `Hydra.Kernel` and any shared domain project it needs.
- `Hydra.YourModule.Api` should reference `Hydra.Infrastructure` and `Hydra.YourModule.Core`.
- `Hydra.Web` must reference `Hydra.YourModule.Api` so the module assembly is loaded.

Implement `IModule` in the API project:

```csharp
public class YourModule : IModule
{
    private const string API_SCHEMA = "/YourModule";

    public IServiceCollection RegisterModules(IServiceCollection services)
    {
        services.AddScoped<IYourService, YourService>();
        return services;
    }

    public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet(API_SCHEMA + "/GetItems", YourHandler.GetItems);
        return endpoints;
    }
}
```

The infrastructure discovers classes implementing `IModule` in loaded `Hydra*Api` assemblies and calls both:

- `RegisterModules(...)`
- `MapEndpoints(...)`

## Development Guidelines

- Keep modules focused on one business capability.
- Put shared, cross-cutting code in `Hydra.Infrastructure` only when it is truly infrastructure.
- Put reusable primitives and contracts in `Hydra.Kernel`.
- Avoid direct dependencies from one module API to another module API.
- Prefer interfaces in Core projects and implementations in API projects.
- Keep endpoint handlers thin; business rules belong in services.
- Add EF configurations beside the owning module/domain.
- Protect sensitive endpoints with permissions.
- Keep package versions centralized in `Directory.Packages.props`.
- Do not commit generated build output such as `bin`, `obj`, or IDE metadata.

## Useful Commands

```bash
# Restore dependencies
dotnet restore Hydra.slnx

# Build the solution
dotnet build Hydra.slnx

# Run the web application
dotnet run --project Hydra.Web

# Add an EF migration
dotnet ef migrations add <MigrationName> --project Hydra.Infrastructure --startup-project Hydra.Web

# Apply migrations
dotnet ef database update --project Hydra.Infrastructure --startup-project Hydra.Web
```

## Contributing

Contributions are welcome. The best way to help is to keep changes focused, documented, and easy to review.

1. Fork the repository.
2. Create a feature branch:

   ```bash
   git checkout -b feature/your-feature-name
   ```

3. Make your changes.
4. Build and test locally:

   ```bash
   dotnet build Hydra.slnx
   ```

5. Update documentation when behavior, configuration, endpoints, or architecture changes.
6. Open a pull request with a clear description:
   - What changed?
   - Why is it needed?
   - How was it tested?
   - Are there migrations or breaking changes?

### Pull Request Checklist

- [ ] The solution builds.
- [ ] Database migrations are included when schema changes.
- [ ] New endpoints are documented or visible through Swagger.
- [ ] Permissions are added for protected operations.
- [ ] No secrets, credentials, or local machine paths are committed.
- [ ] Generated files such as `bin`, `obj`, and `.vs` are not included.

## Roadmap Ideas

These improvements can make the project more useful and attractive for open-source users:

- Add automated tests for services and endpoint handlers.
- Add GitHub Actions for build, test, and code quality checks.
- Add Docker Compose for PostgreSQL, Redis, and the API.
- Add seed data for demo users, roles, permissions, products, and categories.
- Add OpenAPI examples for common flows such as login, product management, cart checkout, and payment.
- Add frontend sample integration.
- Add module-level documentation under a `docs/` directory.
- Add issue templates, PR templates, Code of Conduct, and Security policy.
- Add badges for build status, license, .NET version, and contributors.

## License

Add a `LICENSE` file before publishing the project as open source. MIT or Apache-2.0 are common choices for developer-friendly open-source projects.
