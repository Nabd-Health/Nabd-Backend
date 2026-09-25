# 🩺 NABD (نبض) - Smart Medical EHR & Diagnostic Platform 🚀

[![.NET 8](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![Clean Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-2563eb?style=for-the-badge&logo=diagram&logoColor=white)](#architecture)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?style=for-the-badge&logo=c-sharp&logoColor=white)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![EF Core](https://img.shields.io/badge/EF%20Core-ORM-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](#infrastructure)
[![Mistral AI](https://img.shields.io/badge/AI-Mistral%20LLM-FF7000?style=for-the-badge&logo=openai&logoColor=white)](#ai-integration)
[![Live Portal](https://img.shields.io/badge/Live%20Demo-nabdhealth.me-059669?style=for-the-badge&logo=firefox&logoColor=white)](https://www.nabdhealth.me/)

An enterprise-grade, highly scalable backend Web API powering the **NABD Smart Healthcare Platform**. Designed and engineered with **Clean Architecture**, **SOLID Principles**, and **Domain-Driven Design (DDD)** to manage unified Electronic Health Records (EHR), clinic scheduling, and real-time AI medical diagnostics.

---

## 🌟 Key Features & Capabilities

- 🩺 **Unified Electronic Health Records (EHR)**: Centralized, secure storage for patient medical histories, prescriptions, lab test reports, and doctor consultation notes.
- 🤖 **Mistral AI Medical Symptom Extraction**: Natural language symptom extraction and diagnostic recommendations via integrated AI services.
- 👨‍⚕️ **Doctor & Clinic Management**: Multi-tenant appointment scheduling, queue management, and doctor availability slots.
- 📱 **Mobile & Web API Architecture**: Fully documented Mobile API ([MOBILE_API_DOCUMENTATION.md](./MOBILE_API_DOCUMENTATION.md)) and Web API endpoints.
- 🔐 **Enterprise Security & Auth**: JWT-based authentication, Role-Based Access Control (RBAC), and HIPAA-compliant data encryption.
- ⚡ **High-Performance Data Access**: Optimized EF Core queries with Repository & Unit of Work patterns.

---

## 🏛️ Clean Architecture Layers

```
src/
 ├── 🏢 Nabd.Core            # Domain Entities, Interfaces & Domain Logic
 ├── ⚙️ Nabd.Application     # Business Logic, DTOs, Mapping & Service Contracts
 ├── 🛠️ Nabd.Infrastructure  # EF Core DbContext, Migrations & External Integrations
 ├── 🤝 Nabd.Shared          # Constants, Helpers, Exception Models & Utilities
 └── 🚀 Nabd.API             # ASP.NET Core Web API Controllers, Auth & Middleware
```

---

## 🛠️ Tech Stack & Dependencies

- **Framework**: ASP.NET Core 8.0 / C# 12
- **Architecture**: Clean Architecture / Layered Architecture
- **AI Services**: Mistral AI Integration (`docs/AI_SYSTEM_DOCUMENTATION.md`)
- **Data Access**: Entity Framework Core, SQL Server / PostgreSQL
- **Security**: JWT Authentication, ASP.NET Core Identity
- **Documentation**: Swagger / OpenAPI Specification & Mobile API Documentation

---

## 👨‍💻 System Architect & Lead Engineer

- **Seif Elden Mohamed** - *Team Leader & Backend Architect*
- **Role**: Led a team of 6 software engineers, designed solution architecture, database models, Core & Infrastructure layers, and RESTful API endpoints.
- **GitHub Profile**: [@seif-elmuselmani](https://github.com/seif-elmuselmani)
- **LinkedIn**: [in/seif-elmuselmani](https://www.linkedin.com/in/seif-elmuselmani/)

---

<div align="center">
  <sub>Built with ❤️ & Clean Architecture for Nabd Healthcare Platform</sub>
</div>
