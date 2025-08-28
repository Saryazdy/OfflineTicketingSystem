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

## Build & Database Setup

1. Open the solution in Visual Studio or via terminal.
2. Navigate to `src/OfflineTicketing.Infrastructure`.
3. Install EF Core CLI if needed:

   ```bash
   dotnet tool install --global dotnet-ef
   ```
4. Add migration and update the database:

   ```bash
   dotnet ef migrations add InitialCreate --project OfflineTicketing.Infrastructure
   dotnet ef database update --project OfflineTicketing.Infrastructure
   ```

> For SQLite, the database file will be created automatically.

---

## Run the API

```bash
dotnet run --project src/OfflineTicketing.API
```

Swagger UI available at: `https://localhost:5001/swagger/index.html`

---

## Seeded Users

| Name          | Email                                                 | Password     | Role     |
| ------------- | ----------------------------------------------------- | ------------ | -------- |
| Admin User    | [admin@example.com](mailto:admin@example.com)         | Admin123!    | Admin    |
| Employee User | [employee@example.com](mailto:employee@example.com)   | Employee123! | Employee |
| Employee2     | [employee2@example.com](mailto:employee2@example.com) | Employee123! | Employee |

* Sample tickets are automatically created on database initialization.

---

## API Endpoints

| Method | Route         | Description                  | Roles           |
| ------ | ------------- | ---------------------------- | --------------- |
| POST   | /tickets      | Create a ticket              | Employee        |
| GET    | /tickets/my   | View user's tickets          | Admin, Employee |
| GET    | /tickets      | View all tickets (Paginated) | Admin           |
| GET    | /tickets/{id} | View ticket by ID            | Admin, Employee |
| PUT    | /tickets/{id} | Update ticket                | Admin           |
| DELETE | /tickets/{id} | Delete ticket                | Admin           |
| POST   | /auth/login   | Login and get JWT            | All             |

---

## Testing

* Use Postman or Swagger.
* Run unit tests:

```bash
dotnet test tests/OfflineTicketing.Application.Tests
```

---

## License

MIT License
