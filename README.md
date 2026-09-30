🏢 Leave Management System
A robust, enterprise-grade web application engineered to streamline employee leave applications, quota tracking, and multi-tier organizational approval workflows.

📌 Table of Contents
Overview

System Architecture

Key Features

User Roles & Permissions

Tech Stack

Database Schema & ER Highlights

Getting Started

Prerequisites

Installation & Setup

Configuration

Project Structure

Contributing

License

🚀 Overview
The Leave Management System eliminates paper-based leave processing and decentralized email threads by providing a unified, auditable digital platform. Built using ASP.NET Core MVC and Microsoft SQL Server, the platform enforces strict business rules, real-time balance calculations, and multi-level approval hierarchies based on the Repository Pattern and clean architectural standards.

End-to-End Workflow:
[Employee Applies] ➔ [Validation & Quota Check] ➔ [Manager Review & Action] ➔ [Notification & Balance Deducted] ➔ [Admin Audit Log]

🏗 System Architecture
The project is structured according to clean code guidelines, separating responsibilities into clear logical layers:

Presentation Layer: ASP.NET Core MVC with Razor Views, Tag Helpers, and client-side validation.

Business/Service Layer: Core business rules (e.g., leave day calculations, weekend exclusions, validation checks).

Data Access Layer (DAL): Implemented via Repository Pattern and Unit of Work on top of Entity Framework Core.

Database Layer: Microsoft SQL Server with strict relational constraints and indexing.

✨ Key Features
🔐 Role-Based Access Control (RBAC): Granular authorization for Employee, Manager, and Admin roles using ASP.NET Core Identity.

📝 Automated Leave Tracking: Calculates business working days dynamically while skipping holidays and weekends.

🔄 Multi-Tier Approval Engine: Real-time state transitions (Pending, Approved, Rejected, Canceled).

📊 Real-Time Balances: Automatically computes and updates individual leave quotas (Casual, Sick, Annual, Maternity/Paternity).

🗄️ Full Audit Trail: Complete historical log of who requested, approved, or rejected each entry, with timestamps and review comments.

🎨 Responsive UI: Clean, intuitive UI built with Bootstrap 5 and Razor view templates.

👥 User Roles & Permissions
Capability

👨‍💼 Employee

👨‍‍💻 Manager

👑 Admin

Apply for Leave Requests

✅

✅

✅

View Personal Quota & Balances

✅

✅

✅

View Team Requests

❌

✅

✅

Approve / Reject Leave

❌

✅

✅

Add / Edit Leave Allocations

❌

❌

✅

Manage System Users & Roles

❌

❌

✅

System Configuration & Global Policies

❌

❌

✅

🛠 Tech Stack
Framework: ASP.NET Core MVC (.NET 8.0)

Language: C#

Database: Microsoft SQL Server (MSSQL)

ORM: Entity Framework Core (Code-First Migrations)

Architecture Pattern: Repository Pattern & Dependency Injection (DI)

Authentication & Security: ASP.NET Core Identity (Claims-based authorization)

Frontend UI: Razor Views, HTML5, CSS3, JavaScript, Bootstrap 5

🗄 Database Schema & ER Highlights
Key relational entities managed by EF Core:

LeaveTypes: Defines categories (e.g., Sick, Annual) and default annual quotas.

LeaveAllocations: Maps granted days of specific leave types to individual employees per financial period.

LeaveRequests: Tracks application records, date spans, request comments, statuses, and reviewer IDs.

AspNetUsers / Employees: Extended identity model storing hierarchy, department, and reporting manager relationships.

💻 Getting Started
Prerequisites
Ensure you have the following installed locally:

.NET 8.0 SDK

SQL Server (LocalDB, Express, or standard instance)

Visual Studio 2022 or VS Code

Installation & Setup
Clone the repository:

git clone https://github.com/your-username/leave-management-system.git
cd leave-management-system

Configure Database Connection:
Update the connection string in appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=LeaveManagementDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}

Apply EF Core Migrations:
Run migrations to set up the database and initial seed data:

dotnet ef database update

Build and Run the Application:

dotnet build
dotnet run

Open your browser and navigate to:

https://localhost:7001

⚙️ Configuration
Default seeded accounts for local testing (if configured in DbInitializer.cs):

Role

Username / Email

Password

Admin

admin@localhost.com

P@ssword123!

Manager

manager@localhost.com

P@ssword123!

Employee

employee@localhost.com

P@ssword123!

📁 Project Structure
├── Controllers/              # MVC Route & Action Controllers
├── Data/                     # DbContext, Identity setups, and Data Seeding
├── Contracts/                # Interface definitions for Repositories
├── Repositories/             # Concrete Repository Pattern implementations
├── Models/                   # Domain Entities & Database Models
├── ViewModels/               # ViewModels for data binding & client validation
├── Views/                    # Razor View templates (.cshtml)
├── wwwroot/                  # Static assets (CSS, JS, vendor libraries)
├── appsettings.json          # Environment configs and Connection Strings
└── Program.cs                # Dependency Injection and Middleware pipeline

🤝 Contributing
Contributions are welcome! Please follow these steps:

Fork the Project.

Create your Feature Branch (git checkout -b feature/AmazingFeature).

Commit your Changes (git commit -m 'Add some AmazingFeature').

Push to the Branch (git push origin feature/AmazingFeature).

Open a Pull Request.

📄 License
Distributed under the MIT License. See LICENSE for more information.
