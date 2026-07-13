# 🏥 MedPrecision HMS — Next-Gen Hospital Management System

**MedPrecision HMS** is a modern, enterprise-grade, full-stack health tech solution engineered to streamline healthcare workflows, clinician scheduling, and institutional resource organization. Built with a highly secure and optimized **.NET Web API** backend and a reactive, cutting-edge **Angular 21** frontend.

---

## 🚀 Key Features

- **Advanced Clinician & Doctor Extension Matrix:** A unified architecture linking standard system employees seamlessly to specialized clinical records with precise mapping of Medical Degrees, License Numbers, and Consultation Fees.
- **Dynamic Medical Specialization Ecosystem:** Complete dynamic management of Core Medical Specializations and complex Sub-Specialties, perfectly synchronized from database layer to multi-level cascading UI dropdown selectors.
- **Reactive State Management:** Zero-lag user experience powered entirely by **Angular 21 Signals** and **NgRx SignalStore**, driving instant state updates, data pagination, and reactive filtering.
- **Form Architecture & File Streams:** Robust multi-part Signal-driven forms handling seamless employee/doctor creation alongside active file streaming for profile avatars using `[FromForm]` payload binding.
- **Granular Security Framework:** Secure token-based authentication (JWT) backed by strict role-based and permission-based routing directives on both the client and server layers.

---

## 🛠️ Tech Stack & Architecture

### Frontend Architecture
- **Framework:** Angular 21 (Signal-driven components & reactive paradigms)
- **State Management:** NgRx SignalStore
- **Styling UI:** Tailwind CSS v4, PrimeNG UI Suite, & custom SCSS layouts
- **Icons:** Lucide Angular

### Backend Infrastructure
- **Engine:** ASP.NET Core 9.0 / 8.0 Web API
- **ORM / Database:** Entity Framework Core with SQL Server
- **Patterns:** Repository & Service Layers, Data Transfer Object (DTO) Flattening
- **Transaction Safety:** Explicit DB Transactions enforcing strict integrity across nested User-Role-Employee table creations.

---

## ⚙️ Installation & Setup

Since you already have the configured `.gitignore` handling environment binaries, follow these quick steps to spin up the full-stack architecture locally:

### 1. Backend Setup (.NET API)
```bash
# Navigate to the API directory
cd HospitalManagementSystem.API

# Restore dependencies
dotnet restore

# Update database with migrations
dotnet ef database update

# Run the backend server
dotnet run