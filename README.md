# 🎮 alSati Store
### Your Next Gaming Destination.

<p align="center">
  <strong>A modern desktop game store built with C# Windows Forms and Oracle Database.</strong>
</p>

<p align="center">
  <img src="https://img.shields.io/badge/C%23-.NET%2010-68217A?style=for-the-badge&logo=csharp&logoColor=white" alt="C#">
  <img src="https://img.shields.io/badge/Windows%20Forms-Desktop%20App-512BD4?style=for-the-badge&logo=windows&logoColor=white" alt="Windows Forms">
  <img src="https://img.shields.io/badge/Database-Oracle-F80000?style=for-the-badge&logo=oracle&logoColor=white" alt="Oracle">
  <img src="https://img.shields.io/badge/Status-In%20Development-orange?style=for-the-badge" alt="In Development">
</p>

---

## ✨ About The Project

**alSati Store** is a desktop-based digital game store inspired by modern gaming platforms such as Steam.

The project aims to provide a convenient environment for managing digital games, user accounts, purchases, and game libraries through a clean and intuitive interface.

Built with **C# Windows Forms** and powered by an **Oracle Database**, alSati Store combines desktop application development with relational database management to deliver a practical gaming-store experience.

This project is being developed as a learning and software engineering project, with a focus on database integration, application architecture, and user experience.

## 🚀 Features

### 🔐 Authentication & User Accounts
- User registration and login.
- Secure password hashing and verification.
- Remember Me functionality for email addresses.
- Password visibility toggle.
- Input focus visual effects.

### 🎮 Game Management
- Add new games to the store.
- Edit existing game information.
- Manage game cover images and icons.
- Add and manage game screenshots.
- Organize games using categories.
- Soft deletion to preserve database records.

### 🗄️ Database Integration
- Oracle Database integration.
- Structured relational database design.
- Parameterized SQL queries.
- Data management through repositories.

### 🎨 User Interface
- Dark gaming-inspired theme.
- Modern purple visual identity.
- Custom authentication screen.
- Interactive interface elements.

## 🛠️ Built With

| Technology | Purpose |
|---|---|
| C# | Main programming language |
| .NET 10 | Application framework |
| Windows Forms | Desktop user interface |
| Oracle Database | Data storage and management |
| Oracle Managed Data Access | Database connectivity |
| Visual Studio | Development environment |

## 🏗️ Project Architecture

The application is organized around several core components:

- **Forms:** Handle the application's user interface.
- **Repositories:** Manage database operations.
- **Database Layer:** Provides Oracle database connectivity.
- **Authentication:** Handles account registration and login.
- **PasswordHasher:** Handles password hashing and verification.

## 📋 Database Overview

The database includes tables designed to support the store's main operations:

| Table | Responsibility |
|---|---|
| `USERS` | User accounts and authentication data |
| `GAMES` | Game information and status |
| `GAME_IMAGES` | Game images and screenshots |
| `CATEGORIES` | Game categories |
| `GAME_CATEGORIES` | Game-to-category relationships |
| `FAVORITES` | User favorite games |
| `PURCHASES` | Purchase records |
| `LIBRARY` | User game libraries |
| `REVIEWS` | Game reviews |

## ⚙️ Getting Started

### Prerequisites

Before running the project, make sure you have:

- Windows operating system.
- Visual Studio with .NET 10 development support.
- .NET 10 SDK.
- Oracle Database.
- Oracle Managed Data Access package.

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/YOUR_USERNAME/alSati_store.git
   ```

2. Open the solution file in Visual Studio.

3. Configure your Oracle database connection.

4. Make sure the required database tables are created.

5. Restore the NuGet packages.

6. Build and run the project.

> **Note:** Database credentials and connection settings should be configured locally. Do not commit private credentials to GitHub.

## 🗺️ Development Roadmap

alSati Store is actively being developed. Planned improvements include:

- [x] User registration and login.
- [x] Password hashing and verification.
- [x] Remember Me functionality.
- [x] Game management.
- [x] Game image management.
- [ ] Game browsing and discovery.
- [ ] Shopping cart and checkout.
- [ ] Purchase management.
- [ ] User game library.
- [ ] Favorites and reviews.
- [ ] Further UI and UX improvements.

## 🎯 Project Goals

The main goals of this project are to:

- Build a functional digital game store.
- Practice C# desktop application development.
- Apply relational database design principles.
- Integrate Oracle Database with Windows Forms.
- Improve software architecture and code organization.
- Create a polished and user-friendly experience.

## 👨‍💻 Contributors

Developed collaboratively as a software development project.

## 📜 License

This project is currently developed for educational purposes.

---

<p align="center">
  <strong>alSati Store — Discover. Play. Enjoy.</strong>
  <br>
  <sub>Built with passion for gaming and software development. 💜</sub>
</p>
