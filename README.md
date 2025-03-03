# Demo Projects

Simple learning projects to explore various technologies.

## Technology List
- **C#**: Entity Framework, ASP.NET Core, SQLite
- **JavaScript**: Node.js, SQLite, Vue.js
- **HTML**
- **CSS**
  
## How to Start

### Prerequisites
Before running the projects, ensure you have the following installed:
- [Node.js](https://nodejs.org/) (for JavaScript/Vue.js projects)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (for C# projects)
- [SQLite](https://www.sqlite.org/download.html) (for database support)

### C# Projects
This repository includes two C# demo projects: `CSLocalDB` and `CSServerDB`.

#### CSLocalDB
A C# project using a local SQLite database.

Option 1: From Visual Studio
Open the solution file (CSLocalDB.sln) in Visual Studio 2022 (or later).
Set CSLocalDB as the startup project (right-click the project in Solution Explorer > "Set as Startup Project").
Press F5 or click "Start" to run the project.


Option 2: Using .NET CLI
Navigate to the project directory:

cd CSLocalDB
Restore dependencies:
bash

dotnet restore

Run the project:

dotnet run

CSServerDB
A C# project with a server-side database demo (assumed to be ASP.NET Core with SQLite).

From Visual Studio
Open the solution file (CSServerDB.sln) in Visual Studio 2022 (or later).
Ensure the database is set up:
Run the DemoDB project (if it’s a separate project to initialize the database).
Alternatively, apply migrations if Entity Framework is used:
bash

Collapse

Wrap

Copy
dotnet ef database update --project DemoDB
Set Demo as the startup project.
Press F5 or click "Start" to launch the web server.
Open a browser and navigate to http://localhost:3000 (check launchSettings.json for the exact port).
Using .NET CLI
Navigate to the project directory:
bash

Collapse

Wrap

Copy
cd CSServerDB/Demo
Restore dependencies:
bash

Collapse

Wrap

Copy
dotnet restore
Initialize the database (if applicable):
bash

Collapse

Wrap

Copy
dotnet ef database update --project ../DemoDB
Run the project:
bash

Collapse

Wrap

Copy
dotnet run


JavaScript Projects
(Assumed based on your tech list; update this section with specifics if needed.)

A Vue.js frontend with a Node.js backend using SQLite.

Setup
Navigate to the JavaScript project directory:
bash

Collapse

Wrap

Copy
cd JSProject
Install Node.js dependencies:
bash

Collapse

Wrap

Copy
npm install
Launch
Start the Node.js backend:
bash

Collapse

Wrap

Copy
node server.js
In a separate terminal, start the Vue.js frontend:
bash

Collapse

Wrap

Copy
npm run serve
Open a browser and go to http://localhost:8080 (default Vue.js dev server port).
