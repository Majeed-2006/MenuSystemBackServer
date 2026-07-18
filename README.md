# MenuSystemBackServer
 #  3-Tier Web API System (Backend)

The dedicated backend service for our system. Built for independent development, testing, and API management, completely decoupled from the frontend.

## Architecture (3-Tier)
This project is structured using the **3-Tier Architecture** to ensure separation of concerns, maintainability, and scalable testing:
*   **Presentation Layer (API):** Handles HTTP requests, controllers, and API configurations.
*   **Business Logic Layer (BLL):** Contains the core business logic, validation rules, and services.
*   **Data Access Layer (DAL):** Manages database context, migrations, and repositories (EF Core).

## Tech Stack & Prerequisites
*   **.NET 8.0 Web API** 
*   **Entity Framework Core**
*   **SQL Server / PostgreSQL**
*   **Visual Studio Community 2022**

