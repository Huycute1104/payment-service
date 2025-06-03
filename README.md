# Payment Service

A modular and scalable payment service built with **C# (.NET)** following **Clean Architecture** principles. This service handles payment operations and integrates with multiple payment gateways such as **VNPay**, **PayOS**, etc.

## 🔧 Tech Stack
 
- **Language:** C# (.NET 7/8)
- **Architecture:** Clean Architecture
- **Framework:** ASP.NET Core Web API
- **Database:** SQL Server / PostgreSQL (Configurable)
- **Payment Gateways:** VNPay, PayOS (extensible)
- **Authentication:** JWT Bearer
- **Dependency Injection:** Built-in .NET DI
- **Unit Testing:** xUnit / Moq
- **Logging:** Serilog

## 📁 Project Structure


## 🚀 Getting Started

### Prerequisites

- .NET 8 SDK or later
- Docker (for DB or external services, optional)
- SQL Server or PostgreSQL

### Setup

```bash
git clone https://github.com/your-org/payment-service.git
cd payment-service
dotnet restore
dotnet build
dotnet run --project src/WebApi
