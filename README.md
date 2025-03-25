# pcms-backend
# Pension Contribution Management System (EPS+)

## 📌 Overview
The **Pension Contribution Management System (EPS+)** is a backend solution designed to manage pension contributions, benefit eligibility, and background processing efficiently. The system is built using **.NET Core 7**, **Entity Framework Core**, and **Hangfire** for scheduled tasks.

## 🚀 Features
✅ **Member Management** → Register, update, retrieve, and soft-delete members.  
✅ **Contribution Processing** → Handle Monthly & Voluntary Contributions, calculate totals.  
✅ **Benefit Eligibility** → Auto-update eligibility after the required period.  
✅ **Background Jobs (Hangfire)** → Validate contributions, calculate interest, handle failed transactions.  
✅ **Security & Validation** → JWT authentication, proper input validation, and error handling.  
✅ **API Documentation** → Available via **Swagger** and **Postman Collection**.  

## 🛠 Tech Stack
- **Language:** C#  
- **Framework:** .NET Core 7, ASP.NET Web API  
- **Database:** SQL Server, Entity Framework Core  
- **Background Jobs:** Hangfire  
- **Logging:** Serilog  
- **Unit Testing:** xUnit, Moq  

## 📌 Setup & Installation

### 1️⃣ Clone the Repository  
```sh
git clone https://github.com/AbassAdebayo/pcms-backend.git
cd pcms-backend
```

### 2️⃣ Configure Database  
- Update `appsettings.json` with your **SQL Server connection string**.  
- Run **Entity Framework migrations**:  
```sh
dotnet ef database update
```

### 3️⃣ Build & Run the Project  
```sh
dotnet build
dotnet run
```

### 4️⃣ Access API Documentation (Swagger)  
- Open your browser and go to:  
  📌 `http://localhost:5000/swagger`  

## 📌 API Endpoints
| Method | Endpoint | Description |
|--------|---------|-------------|
| POST | `/api/members/register` | Register a new member |
| GET | `/api/members/{id}` | Get member details |
| POST | `/api/contributions/add` | Add a contribution |
| GET | `/api/contributions/{memberId}/total` | Get total contributions |
| GET | `/api/eligibility/{memberId}` | Check eligibility status |

## 📌 Background Jobs (Hangfire)
Hangfire dashboard available at:  
📌 `http://localhost:5000/hangfire`  

## 📌 Contribution & License
Feel free to contribute!  
📌 License: **MIT License**  

---

**Author:** Abass Adebayo A.
📧 Contact: greatmoh007@gmail.com   
