# Person API – CQRS .NET 8 Assignment

## 🧠 Overview

This project is a technical assignment for a Senior .NET Engineer role. It consists of an ASP.NET Core 8 Web API that implements a **CQRS (Command Query Responsibility Segregation)** architecture using **MediatR**, **FluentValidation**, and **Serilog**. The API manages a `Person` entity, supports versioning, and uses a local JSON file as its data store.

---

## 📦 Tech Stack

- **.NET 8**
- **ASP.NET Core Web API**
- **CQRS + MediatR**
- **FluentValidation**
- **Serilog (Console logging)**
- **In-file JSON storage**
- **Swagger (Swashbuckle)**

---

## 📁 Project Structure

```
PersonApi.sln
├── PersonApi.API             // API Layer (Controllers, Program.cs)
├── PersonApi.Application     // Commands, Queries, Validators (CQRS logic)
├── PersonApi.Domain          // Entities, Enums, Interfaces
├── PersonApi.Infrastructure  // Repository implementation (JSON-based)
└── PersonApi.Tests           // Unit tests for handlers, validators and repository
```
---

## 🚀 How to Run

1. Clone the repo.
2. Open the solution in Visual Studio 2022.
3. Restore NuGet packages.
4. Run the project (`F5` or `Ctrl + F5`).

> By default, Swagger UI is hosted at:  
> **https://localhost:\<port\>/** → auto-redirects to `/swagger/index.html`

---

## 📌 Core Functionality

### ✅ Commands

- **AddPerson**  
  `POST /api/person`  
  Creates a new person. Requires at least `GivenName` and a valid `Gender`.

- **RecordBirth**  
  `PUT /api/person/record-birth`  
  Updates an existing person's `BirthDate` and/or `BirthLocation`.  
  At least one must be provided.

### ✅ Queries

- **GetAllPeople**  
  `GET /api/person`  
  Returns all registered people.

- **GetPersonById**  
  `GET /api/person/{id}`  
  Returns a specific person by their ID.

- **GetPersonHistory**  
  `GET /api/person/{id}/history`  
  Returns the version history of a person.

---

## ✅ Validations

- `AddPersonCommand`:
  - `GivenName` is required
  - `Gender` must be one of: `M`, `F`, `Other`, `Unknown`

- `RecordBirthCommand`:
  - At least one of `BirthDate` or `BirthLocation` is required
  - `PersonId` must be greater than 0

---

## 📂 Data Storage

- `data/people.json`  
  Stores the current state of all people.
  
- `data/person_versions.json`  
  Stores previous versions of people (only when updates occur).

---

## 🔁 Versioning System (Bonus Feature)

- Every time a `Person` is updated (e.g., via `RecordBirth`), their **previous state is saved** to `person_versions.json`.
- This allows tracking changes over time.
- Access history via:  
  `GET /api/person/{id}/history`

---

## 🪵 Logging

- All commands and queries log a message when handled.
- Logging is done via **Serilog**, configured to output to console.

---

## 🧪 Testing

Unit and integration tests are not included but could be added using:
- `xUnit` or `NUnit`
- `Moq` for mocking

---

## 📝 Assumptions

- The system doesn't support persistence beyond JSON files.
- No authentication or authorization is implemented.
- All data is stored in memory or file for simplicity.

---

## 🛠 Future Improvements

- Add full unit test coverage (handlers + controller endpoints)
- Use a real database (e.g., SQLite, PostgreSQL)
- Add pagination and filtering to queries
- Add DELETE and UPDATE (beyond RecordBirth)
- Add **authentication and authorization** using JWT or OAuth2:
  - Secure endpoints (e.g., allow only authenticated users to add or update people)
  - Define user roles (e.g., read-only users vs admin users)
  - Use ASP.NET Core Identity or integrate with external providers (e.g., Azure AD, Auth0)

