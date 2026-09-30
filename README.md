# 🏢 Leave Management System

A modern, role-based **Leave Management System** built with **ASP.NET Core MVC, C#, Entity Framework Core, and Microsoft SQL Server** to digitize and streamline employee leave management, approval workflows, leave allocation, and administrative operations.

The system provides a centralized platform where employees can submit leave requests, managers can review and approve requests, and administrators can manage users, leave policies, allocations, and system-level configurations.

---

## 📌 Table of Contents

* [Overview](#-overview)
* [Key Features](#-key-features)
* [Application Workflow](#-application-workflow)
* [User Roles & Permissions](#-user-roles--permissions)
* [System Architecture](#-system-architecture)
* [Technology Stack](#-technology-stack)
* [Database Design](#-database-design)
* [Project Structure](#-project-structure)
* [Getting Started](#-getting-started)
* [Configuration](#-configuration)
* [Database Setup](#-database-setup)
* [Running the Application](#-running-the-application)
* [Security](#-security)
* [Development Practices](#-development-practices)
* [Future Enhancements](#-future-enhancements)
* [Contributing](#-contributing)
* [License](#-license)

---

# 🚀 Overview

The **Leave Management System** is designed to replace manual leave-processing workflows with a centralized and auditable web-based solution.

Employees can submit leave applications and monitor their leave balances, while managers can review team requests and perform approval or rejection actions. Administrators have complete control over users, roles, leave allocations, leave types, and system policies.

The application follows a layered architecture with clear separation of responsibilities between the presentation, business, and data-access components.

### Core Workflow

```text
Employee
   │
   ▼
Submit Leave Request
   │
   ▼
Request Validation
   │
   ├── Check Leave Balance
   ├── Validate Dates
   ├── Exclude Weekends/Holidays
   └── Validate Business Rules
   │
   ▼
Manager Review
   │
   ├── Approve
   └── Reject
   │
   ▼
Leave Balance Update
   │
   ▼
Audit & Tracking
```

---

# ✨ Key Features

## 🔐 Role-Based Access Control

The application implements role-based authorization using **ASP.NET Core Identity**.

Supported roles include:

* 👨‍💼 Employee
* 👨‍💻 Manager
* 👑 Administrator

Each role receives permissions according to its responsibilities.

---

## 📝 Leave Request Management

Employees can:

* Submit leave applications
* Select appropriate leave types
* Specify leave start and end dates
* Add request comments
* View request status
* Track previous leave applications
* Monitor available leave balances

The system validates requests before submission to prevent invalid or inconsistent leave records.

---

## 📅 Intelligent Leave-Day Calculation

The application dynamically calculates applicable leave days while handling business rules such as:

* Weekends
* Holidays
* Start and end dates
* Leave duration
* Available leave balance

This ensures that leave balances are calculated according to the configured business rules rather than relying solely on calendar-day calculations.

---

## 🔄 Multi-Level Approval Workflow

Leave requests move through clearly defined states:

```text
Pending
   ↓
Manager Review
   ↓
Approved / Rejected
```

Additional request states can include:

* Pending
* Approved
* Rejected
* Canceled

Each state transition is tracked to maintain consistency and accountability.

---

## 📊 Leave Balance Management

The system maintains leave allocations for different leave categories, such as:

* Casual Leave
* Sick Leave
* Annual Leave
* Maternity Leave
* Paternity Leave

Available balances are calculated based on employee allocations and approved leave requests.

---

## 🗂️ Audit Trail

The application maintains a historical record of important leave-related activities, including:

* Request creation
* Approval actions
* Rejection actions
* Reviewer information
* Review comments
* Status changes
* Timestamps

This provides better visibility and accountability across the leave-management process.

---

## 👥 Team Management

Managers can:

* View requests submitted by their team members
* Review pending requests
* Approve or reject requests
* Add review comments
* Monitor team leave activity

Administrators can manage users, roles, allocations, and system-level settings.

---

## 🎨 Responsive User Interface

The frontend is built using:

* Razor Views
* HTML5
* CSS3
* JavaScript
* Bootstrap 5

The interface is designed to provide a clean and responsive experience across desktop and supported mobile screen sizes.

---

# 🔄 Application Workflow

### Employee Workflow

```text
Login
  ↓
Dashboard
  ↓
View Leave Balance
  ↓
Create Leave Request
  ↓
System Validation
  ↓
Submit Request
  ↓
Manager Review
  ↓
Approved / Rejected
```

### Manager Workflow

```text
Login
  ↓
Manager Dashboard
  ↓
View Team Requests
  ↓
Review Request
  ↓
Approve / Reject
  ↓
Add Review Comment
  ↓
System Updates Request Status
```

### Administrator Workflow

```text
Login
  ↓
Admin Dashboard
  ↓
Manage Users & Roles
  ↓
Manage Leave Types
  ↓
Manage Leave Allocations
  ↓
Configure System Policies
  ↓
Monitor Leave Activity
```

---

# 👤 User Roles & Permissions

| Capability                  | Employee | Manager | Admin |
| --------------------------- | :------: | :-----: | :---: |
| Submit Leave Request        |     ✅    |    ✅    |   ✅   |
| View Personal Leave Balance |     ✅    |    ✅    |   ✅   |
| View Personal Leave History |     ✅    |    ✅    |   ✅   |
| View Team Requests          |     ❌    |    ✅    |   ✅   |
| Approve / Reject Requests   |     ❌    |    ✅    |   ✅   |
| Manage Leave Allocations    |     ❌    |    ❌    |   ✅   |
| Manage Leave Types          |     ❌    |    ❌    |   ✅   |
| Manage Users & Roles        |     ❌    |    ❌    |   ✅   |
| Configure System Policies   |     ❌    |    ❌    |   ✅   |
| View Audit Information      |     ❌    |    ✅    |   ✅   |

---

# 🏗️ System Architecture

The application follows a layered architecture to maintain separation of concerns, improve maintainability, and keep business logic independent from presentation and database-access code.

### Architecture Overview

```text
┌─────────────────────────────────────┐
│          Presentation Layer         │
│ ASP.NET Core MVC + Razor Views      │
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│       Business / Service Layer      │
│ Validation • Business Rules         │
│ Leave Calculations • Workflows      │
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│         Data Access Layer           │
│ Repository Pattern • Unit of Work   │
│ Entity Framework Core               │
└──────────────────┬──────────────────┘
                   │
                   ▼
┌─────────────────────────────────────┐
│          Database Layer             │
│        Microsoft SQL Server         │
└─────────────────────────────────────┘
```

### Architectural Principles

The project emphasizes:

* Separation of concerns
* Dependency Injection
* Repository Pattern
* Unit of Work
* SOLID principles
* Reusable services
* Centralized validation
* Maintainable domain models
* Secure authentication and authorization

---

# 🛠️ Technology Stack

### Backend

* **C#**
* **ASP.NET Core MVC**
* **.NET 8**
* **Entity Framework Core**
* **ASP.NET Core Identity**

### Database

* **Microsoft SQL Server**
* Entity Framework Core Code-First
* EF Core Migrations
* Relational database constraints
* Database indexing

### Frontend

* **Razor Views**
* **HTML5**
* **CSS3**
* **JavaScript**
* **Bootstrap 5**
* Client-side validation

### Architecture & Development

* Repository Pattern
* Unit of Work
* Dependency Injection
* Layered Architecture
* Claims/Role-Based Authorization

### Development Tools

* Visual Studio 2022 / VS Code
* Git
* GitHub
* SQL Server Management Studio
* Postman

---

# 🗄️ Database Design

The application uses **Microsoft SQL Server** with **Entity Framework Core Code-First** for database management.

### Core Entities

#### `LeaveTypes`

Stores available leave categories and their default allocation rules.

Example:

```text
LeaveType
├── Id
├── Name
├── Description
└── DefaultQuota
```

---

#### `LeaveAllocations`

Stores leave allocations assigned to individual employees for a specific period.

```text
LeaveAllocation
├── Id
├── EmployeeId
├── LeaveTypeId
├── NumberOfDays
└── Period
```

---

#### `LeaveRequests`

Stores employee leave applications and their approval information.

```text
LeaveRequest
├── Id
├── EmployeeId
├── LeaveTypeId
├── StartDate
├── EndDate
├── Comments
├── Status
├── ReviewerId
├── ReviewComments
└── CreatedAt
```

---

#### `AspNetUsers / Employees`

ASP.NET Core Identity is extended to support application-specific employee information, including organizational hierarchy and reporting relationships.

---

# 📁 Project Structure

```text
LeaveManagementSystem/
│
├── Controllers/
│   └── MVC Controllers
│
├── Data/
│   ├── ApplicationDbContext
│   ├── Identity Configuration
│   └── Database Seeding
│
├── Contracts/
│   └── Repository & Service Interfaces
│
├── Repositories/
│   └── Repository Implementations
│
├── Models/
│   └── Domain & Database Entities
│
├── ViewModels/
│   └── UI & Form-specific Models
│
├── Views/
│   ├── Shared
│   ├── Account
│   ├── Leave
│   ├── Manager
│   └── Admin
│
├── wwwroot/
│   ├── css
│   ├── js
│   └── libraries
│
├── Migrations/
│   └── EF Core Database Migrations
│
├── appsettings.json
├── Program.cs
└── README.md
```

---

# 💻 Getting Started

Follow the steps below to run the application locally.

## Prerequisites

Make sure the following software is installed:

* [.NET 8 SDK](https://dotnet.microsoft.com/)
* Microsoft SQL Server
* Visual Studio 2022 or VS Code
* Git
* SQL Server Management Studio *(optional)*

---

# 📥 Installation

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/leave-management-system.git
```

Navigate to the project directory:

```bash
cd leave-management-system
```

---

# ⚙️ Configuration

Update the database connection string in `appsettings.json`.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=LeaveManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
  }
}
```

Update the connection string according to your local SQL Server configuration.

---

# 🗃️ Database Setup

If Entity Framework Core CLI is not already installed:

```bash
dotnet tool install --global dotnet-ef
```

Apply the existing migrations:

```bash
dotnet ef database update
```

This will create the required database schema and apply the configured migrations.

---

# ▶️ Running the Application

Restore dependencies:

```bash
dotnet restore
```

Build the application:

```bash
dotnet build
```

Run the application:

```bash
dotnet run
```

Then open the application using the HTTPS URL displayed in the terminal, for example:

```text
https://localhost:7001
```

---

# 🔑 Development Seed Accounts

If database seeding is enabled through `DbInitializer.cs`, development accounts can be configured for testing.

| Role     | Email                    | Password       |
| -------- | ------------------------ | -------------- |
| Admin    | `admin@localhost.com`    | `P@ssword123!` |
| Manager  | `manager@localhost.com`  | `P@ssword123!` |
| Employee | `employee@localhost.com` | `P@ssword123!` |

> ⚠️ These credentials are intended only for local development/testing. Never use default credentials in a production environment.

---

# 🔒 Security

Security is implemented using ASP.NET Core Identity and authorization mechanisms.

The application supports:

* Authentication
* Role-based authorization
* Claims-based authorization
* Secure password hashing through ASP.NET Core Identity
* Protected application routes
* Server-side validation
* Client-side validation
* User-role management
* Controlled access to administrative functionality

---

# 🧠 Development Practices

The project demonstrates several backend development practices commonly used in real-world .NET applications:

### Clean Separation of Responsibilities

Controllers are kept focused on handling HTTP requests and coordinating application operations rather than containing extensive business logic.

### Dependency Injection

Dependencies such as repositories and services are injected through the ASP.NET Core built-in dependency injection container.

### Repository Pattern

Data-access operations are abstracted behind repository interfaces, reducing direct coupling between application logic and Entity Framework Core.

### Entity Framework Core

EF Core is used for:

* Entity modeling
* Database relationships
* CRUD operations
* LINQ queries
* Code-First migrations
* Database schema management

### Validation

Leave requests are validated before being processed to ensure that invalid dates, insufficient balances, and other business-rule violations are handled appropriately.

---

# 🚀 Future Enhancements

Potential future improvements include:

* 📧 Email notifications for leave status changes
* 📱 Mobile-friendly employee dashboard
* 📊 Advanced HR analytics and reporting
* 📅 Calendar-based leave visualization
* 🔔 Real-time notifications
* 📄 Leave report export to PDF/Excel
* 🏢 Department-level leave policies
* 🌐 REST API integration
* ☁️ Cloud deployment
* 🔐 Enhanced audit and security monitoring

---

# 🤝 Contributing

Contributions are welcome and appreciated.

To contribute:

### 1. Fork the repository

### 2. Create a feature branch

```bash
git checkout -b feature/AmazingFeature
```

### 3. Commit your changes

```bash
git commit -m "Add AmazingFeature"
```

### 4. Push the branch

```bash
git push origin feature/AmazingFeature
```

### 5. Open a Pull Request

Please ensure that your contribution follows the existing project structure and coding conventions.

---

# 📄 License

This project is distributed under the **MIT License**.

See the `LICENSE` file for more information.

---

# ⭐ Project Highlights

This project demonstrates practical experience with:

* **ASP.NET Core MVC**
* **C#**
* **Entity Framework Core**
* **Microsoft SQL Server**
* **ASP.NET Core Identity**
* **Repository Pattern**
* **Unit of Work**
* **Dependency Injection**
* **Role-Based Authorization**
* **CRUD Operations**
* **Business Logic Implementation**
* **Database Design**
* **Code-First Migrations**
* **MVC Architecture**
* **Responsive Web Development**

---

## 👨‍💻 Author

**Vickey Yadav**

Built as a full-stack .NET application to demonstrate practical implementation of enterprise-style leave management workflows using modern ASP.NET Core technologies.
