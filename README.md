# 🚀 User Management Console App

This project is a console application written in C# and .NET, built as a practical case study for applying SOLID principles and good software design patterns
 **The primary purpose of this project is to learn and demonstrate a CI workflow using GitHub Actions.

The application provides a simple command-line interface (CLI) to manage users, including adding, searching, and deleting them, with a strong focus on writing clean, testable, and maintainable code.

---

## ✨ Key Features & Concepts Applied

This project was designed to be a practical example of software development best practices:

* **SOLID Principles:** The five principles were strictly followed to separate responsibilities and create flexible, scalable code.
* **Command Pattern:** Used to process user input in a dynamic and extensible way (OCP).
* **Dependency Injection:** Uses `Microsoft.Extensions.DependencyInjection` to manage object lifecycles and decouple dependencies.
* **Comprehensive Unit Testing:** High test coverage for all application logic using **xUnit** and **Moq**.
* **Abstraction:** Leverages interfaces to decouple layers, enabling effective testing and modularity.
* **CI with GitHub Actions:** An automated workflow is set up to build the code and run all tests.

## ⚙️ Getting Started

Follow these steps to run the project on your local machine.

### Prerequisites

* [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 

### Installation & Running

1.  Clone the repository to your machine:

2.  Run the application:

##  CI Pipeline (GitHub Actions)

This project contains a Continuous Integration (CI) workflow , It is designed to ensure code stability.

1.  **Triggers:** The workflow runs automatically on every `push` or `pull_request` to the `main` branch.
2.  **Build & Test Job (`build-and-test`):**
    * Sets up the .NET environment.
    * Restores NuGet packages.
    * Builds the project in `Release` configuration.
    * Runs all unit tests defined in the test project.

---
