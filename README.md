# 🏙️ Beşiktaş Guest Book API

A REST API guest book for the neighbourhoods of the Beşiktaş district. Users can register, log in, and then create, edit, and search notes tied to any neighbourhood in Beşiktaş.

---

## 🚀 Tech Stack

| Technology | Purpose |
|---|---|
| **ASP.NET Core 8** | Web API framework |
| **Entity Framework Core** | ORM |
| **PostgreSQL** | Database (Npgsql driver) |
| **JWT Bearer** | Authentication |
| **AutoMapper** | Object mapping |
| **FluentValidation** | Input validation |
| **Serilog** | Logging |
| **Swagger / OpenAPI** | API documentation (development) |
| **Docker** | Containerisation |

---

## 📁 Project Structure

The solution follows **Clean Architecture** and is split into 5 layers:

```
BesiktasGuestBook/
├── Domain/               # Core business models and entities
│   ├── Entities/         # User, Note, Mahalle (+ DTOs)
│   ├── Common/           # EntityBase (shared fields)
│   └── Request/          # API request models
│
├── Application/          # Business logic services
│   └── Service/          # IUserService, INoteService, IMahalleService + implementations
│
├── Infrustructure/       # Cross-cutting concerns
│   ├── Token/            # JWT generation (TokenService, JwtOptions)
│   ├── PasswordHash/     # Password hashing
│   ├── Mapper/           # AutoMapper profiles
│   ├── Validators/       # FluentValidation validators
│   ├── Middlewares/      # GlobalExceptionHandler
│   └── Exception/        # Custom exception types
│
├── Persistence/          # Data access layer
│   ├── Context/          # AppDbContext (EF Core)
│   └── Migrations/       # EF Core migration files
│
└── projemaksut/          # Web API entry point
    ├── Controller/       # AuthController, UserController, NoteController, MahalleController
    ├── Program.cs        # DI registration, middleware, auto-migration
    └── appsettings.json  # Application configuration
```

---

## 🔑 API Endpoints

### Auth
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/Auth/Login` | Log in and receive a JWT token | Public |

### User
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/User/Register` | Register a new user | Public |
| GET | `/api/User/users` | List all users | JWT |
| GET | `/api/User/active/users` | List active users | JWT |
| GET | `/api/User/filtre` | Filter users (id, name, surname, email) | JWT |
| PUT | `/api/User/update` | Update own profile | JWT |
| DELETE | `/api/User/delete/{id}` | Soft-delete a user | JWT |
| GET | `/api/User/adminUsers` | Full user details | JWT (Admin) |
| DELETE | `/api/User/adminHardDelete/{id}` | Permanently delete a user | JWT (Admin) |

### Note
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| POST | `/api/Note/CreateNote` | Create a note | JWT |
| GET | `/api/Note/GetUserNotesById` | Get notes for a user | JWT |
| PUT | `/api/Note/UpdateNote` | Update a note (own notes only) | JWT |
| DELETE | `/api/Note/DeleteNote` | Delete a note | JWT |
| GET | `/api/Note/FiltreNot` | Filter notes by text | JWT |

### Mahalle (Neighbourhood)
| Method | Endpoint | Description | Auth |
|--------|----------|-------------|------|
| GET | `/api/Mahalle/AllMahalles` | List all neighbourhoods | Public |
| GET | `/api/Mahalle/FiltreMahalle` | Filter by neighbourhood name | Public |

---

## ⚙️ Getting Started

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/)

### 1. Clone the repository

```bash
git clone https://github.com/maksutakg/BesiktasGuestBook.git
cd BesiktasGuestBook
```

### 2. Configure the application

Edit `projemaksut/appsettings.json` or set the following environment variables:

```bash
POSTGRES_CONN="Host=localhost;Username=postgres;Password=yourpassword;Database=guestbookdotnet"
JWT_KEY="your-secret-key"
```

### 3. Restore dependencies and run

```bash
dotnet restore
cd projemaksut
dotnet run
```

> Pending EF Core migrations and neighbourhood seed data are applied automatically on startup.

### 4. Swagger UI

In development mode, explore and test the API at:
```
http://localhost:8080/swagger
```

---

## 🐳 Running with Docker

```bash
docker build -t besiktas-guestbook .
docker run -p 8080:8080 \
  -e POSTGRES_CONN="Host=host.docker.internal;Username=postgres;Password=yourpassword;Database=guestbookdotnet" \
  -e JWT_KEY="your-secret-key" \
  besiktas-guestbook
```

---

## 🌍 Environment Variables

| Variable | Description | Default |
|---|---|---|
| `POSTGRES_CONN` | PostgreSQL connection string | Value in `appsettings.json` |
| `JWT_KEY` | JWT signing key | Value in `appsettings.json` |
| `PORT` | Listening port | `8080` |

---

## 🗺️ Data Model

```
User ─── (1:N) ──► Note ◄── (N:1) ─── Mahalle
```

- **User**: First name, last name, email (unique), hashed password
- **Note**: Text content, timestamp, relations to user and neighbourhood
- **Mahalle**: 23 neighbourhoods in Beşiktaş (loaded as seed data)

### Seeded Neighbourhoods
Abbasağa, Akat, Arnavutköy, Balmumcu, Bebek, Cihannüma, Dikilitaş, Etiler, Gayrettepe, Konaklar, Kuruçeşme, Kültür, Levazım, Levent, Mecidiye, Muradiye, Nispetiye, Ortaköy, Sinanpaşa, Türkali, Ulus, Vişnezade, Yıldız

---

## 🔐 Authentication

The API uses **JWT Bearer Token** authentication.

1. Send a request with your email and password to `/api/Auth/Login`
2. Use the returned token in the `Authorization: Bearer <token>` header for all protected requests

Token validity: **99 minutes**

---

## 📄 License

This project is licensed under the MIT License.
