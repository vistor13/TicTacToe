# Contributing to TicTacToe

I am excited to have you contribute to TICTACTOE! This document provides clear guidelines to help you set up your environment, follow project standards, and submit your contributions effectively.

---

## Table of Contents
1. [How to Get Started](#how-to-get-started)
    - [Prerequisites](#prerequisites)
    - [Setting Up the Development Environment](#setting-up-the-development-environment)
2. [Testing Guidelines](#testing-guidelines)
3. [Submitting Your Work](#submitting-your-work)

---
## How to Get Started

### Prerequisites

Ensure the following tools are installed on your machine:

- **Visual Studio** (Community, Professional, or Enterprise) with the `.NET and Web Development` workload.
- **.NET SDK**: Verify the required version in the `global.json` file.
- **Git**: For version control.

### Setting Up the Development Environment

1. **Fork and clone the repository**:
   ```bash
   git clone https://github.com/vistor13/TicTacToe.git
2. **Open the project in Visual Studio**:
   - Launch Visual Studio.
   - Open the solution file (`.sln`) located in the root directory.

3. **Restore dependencies**:
   - Restore required NuGet packages:
     ```bash
     dotnet restore
     ```

4. **Run the application**:
   - Use the **Start Debugging** option in Visual Studio to launch the API.

---

## Testing Guidelines

We use **xUnit** for unit testing. Please follow these best practices:

1. **Place tests in the appropriate folder**:
   - Test files should be located in the `Tests` project, e.g., `ProjectName.Tests`.

2. **Use descriptive test names**:
   - Write names that clearly explain the test purpose, e.g., `Should_ReturnExpectedResponse_When_ValidInputIsProvided`.

3. **Mock dependencies**:
   - Use mocking libraries for external dependencies.

4. **Run all tests locally**:
   ```bash
   dotnet test
5. **Ensure test coverage**:
   - Aim for at least 80% coverage for new code and features.

## Submitting Your Work

Follow these steps to contribute:

1. **Create a new branch**:
   - Use a descriptive branch name `bugfix/fix-some-bug`.
     ```bash
     git checkout -b branch-name
     ```

2. **Make your changes**:
   - Ensure your code follows the project's coding standards and is well-documented.

3. **Commit your changes**:  
   - Write clear and meaningful commit messages following the **Semantic Commit Messages** convention:  
     - **feat**: A new feature  
     - **fix**: A bug fix  
     - **docs**: Documentation updates  
     - **style**: Code style changes (formatting, no code logic changes)  
     - **refactor**: Code refactoring (neither fixes a bug nor adds a feature)  
     - **test**: Adding or updating tests  
     - **chore**: Changes to the build process or auxiliary tools  

   Example:  
   ```bash
   git commit -m "feat: add authentication middleware for API"
   git commit -m "fix: resolve header validation issue"
   git commit -m "docs: update README"

4. **Push your branch**:
   ```bash
   git push origin branch-name
   
5. **Open a pull request (PR)**:  
   - Go to the repository on GitHub.  
   - Click **New Pull Request** and include:  
     - A summary of the changes.  
     - Any issue numbers your PR addresses (e.g., `Fixes #42`).  
     - Relevant testing details.  