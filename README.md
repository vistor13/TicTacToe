# Tic-Tac-Toe Project (Mentorship Program)

## About the Project
This is a personal project developed as part of a mentorship program. The goal is to build a simple yet functional **Tic-Tac-Toe** game while learning the fundamentals of software development.

---

## Features
- Playable 2-player Tic-Tac-Toe game.
- Beginner-friendly for .NET learners.
- Clean and modular code for easy maintenance.

---

## Setup Instructions

### Prerequisites
- Install **Visual Studio** (Community Edition or higher) with the .NET development workload.  
- Ensure **Git** is installed for cloning the repository.
- Install **Docker** to run the application in a containerized environment.

### Local Development Setup
- Clone the repository to your local machine:  
   ```bash
   git clone <repository_url>
- Open the solution file (`.sln`) in **Visual Studio**.  
- Build the solution by pressing `Ctrl+Shift+B`.  
- Run the project by pressing `F5`.  
### Docker Setup
- Ensure Docker is installed and running on your machine.
- Build the Docker image:  
   ```bash
   docker build -t tic-tac-toe:latest
- Run the application in a Docker container: 
   ```bash
   docker run -p 8080:80 tic-tac-toe:latest
- Open a browser and navigate to http://localhost:8080 to access the application.
## Contribution Guidelines
### Getting Started
- Fork the repository and create a new branch for your changes.  
- Create a new branch for your changes: 
   ```bash
   git checkout -b feature/your-feature-name
### Submitting Changes
- Commit your changes with clear and concise messages: 
   ```bash
   git commit -m "Add: Description of your change"
- Push your branch to your forked repository: 
   ```bash
   git push origin feature/your-feature-name
- Submit a pull request with a detailed explanation of your contributions.
### Additional Notes
- Make sure your changes do not break existing functionality.
- Include tests for any new features added.
---

## License
This project is licensed under the MIT License. For more details, please refer to the `LICENSE.md` file.
