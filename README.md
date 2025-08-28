# OfflineTicketingSystem

A simple Ticketing System built with ASP.NET Core, MediatR, and EF Core, featuring:

* Create, update, delete, and view tickets
* Pagination support for tickets
* JWT Authentication
* Role-based Authorization (Admin / Employee)
* CQRS pattern with MediatR

---

## Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
* Visual Studio 2022 or later
* SQLite (or other database)
* Postman or similar API testing tool

---

## Project Structure

```
OfflineTicketingSystem/
├─ src/
│  ├─ OfflineTicketing.API/          # API controllers, Program.cs
│  ├─ OfflineTicketing.Application/  # Commands, Queries, DTOs, Behaviors
│  ├─ OfflineTicketing.Domain/       # Entities, Enums
│  ├─ OfflineTicketing.Infrastructure/ # Repositories, DbContext
├─ tests/
│  ├─ OfflineTicketing.Application.Tests/
│  └─ OfflineTicketing.API.Tests/
├─ README.md
```

---

## How to Run the Project

1. Open the solution in Visual Studio or via terminal.
2. Restore NuGet packages:

   ```bash
   dotnet restore
   ```
3. Build the project:

   ```bash
   dotnet build
   ```
4. Run the API:

   ```bash
   dotnet run --project src/OfflineTicketing.API
   ```
5. Access Swagger UI at: [https://localhost:7099/swagger/index.html](https://localhost:7099/swagger/index.html)

---

## How to Seed the Database with Initial Data

The project automatically seeds the database with initial users and tickets using `DbSeeder`.

Seeded Users:

| Name          | Email                                               | Password     | Role     |
| ------------- | --------------------------------------------------- | ------------ | -------- |
| Admin User    | [admin@example.com](mailto:admin@example.com)       | Admin123!    | Admin    |
| Employee User | [employee@example.com](mailto:employee@example.com) | Employee123! | Employee |

Seeded Tickets:

* Sample tickets are created for the employees upon database initialization.

To manually trigger seeding:

1. Ensure database is created and migrated:

   ```bash
   dotnet ef database update --project src/OfflineTicketing.Infrastructure
   ```
2. Run the application; seeding happens on startup if users do not exist.

---

## Assumptions and Decisions

* SQLite is used for simplicity and local development.
* JWT Authentication is implemented for securing API endpoints.
* Role-based authorization distinguishes between Admin and Employee actions.
* Result<T> pattern is used in MediatR pipeline for consistent API responses.
* Pagination is applied to all queries returning lists of tickets.
* The API is designed with CQRS pattern separating commands and queries.

---

## Testing

* Use Postman or Swagger to test endpoints.
* Run unit tests:

```bash
dotnet test tests/OfflineTicketing.Application.Tests
```

---

## License

MIT License
