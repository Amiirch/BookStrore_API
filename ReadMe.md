# Book_Store Project

This is a .NET-based book store project that provides a RESTful API for managing an online store. The project includes authentication and authorization, uses PostgreSQL with Entity Framework Core for data persistence, Redis for caching to enhance data retrieval speed. The project uses Swagger for API testing and documentation.

## Table of Contents

1. [About the Project](#about-the-project)
2. [Features](#features)
3. [Technologies](#technologies)
4. [Getting Started](#getting-started)
    - [Prerequisites](#prerequisites)
    - [Installation](#installation-without-docker)
 
5. [Usage](#usage)

---

## About the Project

This project serves as the back-end for an book_store platform, offering the following features:

### Features

- User authentication and authorization (JWT)
- book catalog management
- Redis caching to improve data access speed
- PostgreSQL for data storage with Entity Framework Core
- Swagger for interactive API documentation and testing

---

## Technologies

- **.NET 8**: The core framework used to build the application.
- **Entity Framework Core**: ORM for database interactions with PostgreSQL.
- **PostgreSQL**: Relational database used for data storage.
- **Redis**: Caching layer to improve response times and reduce database load.
- **Swagger**: API documentation and testing tool.
- **JWT**: JSON Web Tokens for secure authentication and authorization.

---

## Getting Started

To set up and run the project locally, follow these instructions.

### Prerequisites

Ensure that you have the following installed:

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Redis](https://redis.io/download)

---

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/Amiirch/Book_Store_API.git
    ```

2. Navigate to the project directory:

    ```bash
    cd BookStoreApi
    ```

3. Configure the connection string in the `appsettings.json` file to point to your PostgreSQL instance.

4. Restore the dependencies:

    ```bash
    dotnet restore
    ```

5. Set up the PostgreSQL database using Entity Framework Core migrations:

    ```bash
    dotnet ef database update
    ```

6. Run the application:

    ```bash
    dotnet run
    ```
---

## Usage

Once the application is running, you can access the API via Swagger for testing and documentation at:
