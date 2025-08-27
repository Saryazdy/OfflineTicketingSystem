Absolutely! 🌟
Here’s a professional **README.md** in **English** for your Offline Ticketing System project, formatted and ready to copy-paste. It covers architecture, setup, endpoints, JWT, and testing.

---

```markdown
# Offline Ticketing System

## 📝 Project Description
This project is an **internal ticketing system for an organization**, implemented using **ASP.NET Core 8 Web API** with **CQRS + MediatR**.  

Features include:

- User management with **Employee** and **Admin** roles
- Create, view, update, and delete tickets
- JWT-based authentication and role-based authorization
- Ticket statistics (Open, InProgress, Closed)
- Validation and Logging pipeline
- Clean Architecture (Jason Taylor Style)

---

## 🏗️ Project Structure

```

OfflineTicketingSystem
│
├── src
│   ├── OfflineTicketingSystem.Domain        # Entities and Enums
│   ├── OfflineTicketingSystem.Application   # Commands, Queries, DTOs, Validators, Interfaces
│   ├── OfflineTicketingSystem.Infrastructure # EF Core, JWT, Repositories
│   └── OfflineTicketingSystem.WebApi       # Controllers, Program.cs, Swagger
└── tests
└── OfflineTicketingSystem.Application.Tests # Unit Tests

````

---

## ⚙️ Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQLite (default local database)
- IDE like Visual Studio or VS Code

---

## 🏃 How to Run

1. Clone the repository:
```bash
git clone <repository-url>
cd OfflineTicketingSystem
````

2. Apply EF Core migrations and create SQLite database:

```bash
dotnet ef database update --project src/OfflineTicketingSystem.Infrastructure
```

3. Run the Web API:

```bash
cd src/OfflineTicketingSystem.WebApi
dotnet run
```

4. Open Swagger UI:

```
https://localhost:5001/swagger
```

---

## 🔐 JWT Authentication

* **Login**: `POST /auth/login`

Request:

```json
{
  "email": "admin@example.com",
  "password": "Admin123!"
}
```

Response:

```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "userId": "guid",
  "fullName": "Admin User",
  "role": "Admin"
}
```

Use **Authorization Header** for protected endpoints:

```
Authorization: Bearer <token>
```

---

## 🗂️ API Endpoints

| Method | URL            | Role           | Description                          |
| ------ | -------------- | -------------- | ------------------------------------ |
| POST   | /tickets       | Employee       | Create a new ticket                  |
| GET    | /tickets/my    | Employee       | List tickets created by current user |
| GET    | /tickets       | Admin          | List all tickets                     |
| GET    | /tickets/{id}  | Admin/Employee | Get ticket details                   |
| PUT    | /tickets/{id}  | Admin          | Update ticket status/assignment      |
| DELETE | /tickets/{id}  | Admin          | Delete a ticket                      |
| GET    | /tickets/stats | Admin          | Get ticket statistics                |

---

## 💾 Database

* **SQLite** local database: `offline_ticketing.db`
* Main tables:

  * Users
  * Tickets
  * TicketHistories

---

## 🧩 Architecture

* **Domain**: Entities and Enums
* **Application**: Commands, Queries, DTOs, Validators, Interfaces, Pipeline Behaviors
* **Infrastructure**: EF Core, Repositories, JWT Service
* **WebApi**: Controllers, Middleware, Swagger

**Benefits:**

* Loose coupling
* Fully testable (Unit/Integration)
* Clean Architecture (Jason Taylor Style)
* CQRS for separating read/write operations

---

## 🧪 Testing

Unit Tests use **Moq + xUnit** for CommandHandlers:

```bash
cd tests/OfflineTicketingSystem.Application.Tests
dotnet test
```

---

## ⚠️ Security Notes

* Passwords should be stored **hashed using BCrypt**.
* JWT Key must be **secure and confidential** in production.
* Endpoints are protected by **role-based authorization** (`Employee` / `Admin`).

---

## 🔗 References

* [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
* [MediatR Documentation](https://github.com/jbogard/MediatR)
* [Clean Architecture (Jason Taylor)](https://jasontaylor.dev/clean-architecture-getting-started/)

```

---

This README is **ready to copy-paste**. It explains architecture, setup, JWT, endpoints, DB, testing, and security.  

I can also provide a **Swagger JSON with JWT ready for testing all endpoints** so you can import it directly into Swagger UI—do you want me to prepare that?
```
