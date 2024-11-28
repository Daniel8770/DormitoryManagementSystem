# DormitoryManagementSystem

This is the source code of my bachelor project in Computer Science on Univeristy of Copenhagen, Department of Computer Science.

The project is about Domain-Driven Design (DDD) and how it can be applied on a real world domain. This code is an example application backend for which to experiment with DDD principles in the bachelor project. It consists of a web API developed in ASP.NET in C#, a domain model designed using tactical DDD principles and an infrastructure layer which can persist domain objects. The code also has an SQL script that defines some of the relational database tables, representing the domain objects.

Besides developing an application backend, this project has primarly focused on experimenting with making a lightweight framework for implementing DDD principles. You find this in `DormitoryManagementSystem.Domain.Common` where base classes for entities, value objects, aggregate roots and domain events are defined. In `DormitoryManagementSystem.Infrastructure/Common` you will find classes that implement persistence of domain objects and domain events atomically.

What has been focused on the most in this code is the `BookableResourceAggregate` within the club context. You can find this aggregate in `DormitoryManagementSystem.Domain.ClubsContext/BookableResourceAggregate`. In the infrastructure layer you will find a repository implemented using [Dapper](https://www.learndapper.com/) that persists this object, and in the `SQL` folder you will find the table definitions for this aggregate. In `DormitoryManagementSystem.Application/ClubsContext` you will find the application code, which is the layer between the API and the domain layer, for this aggregate.


## How to run the application

This guide assumes that you start this project without any of the needed dependencies, like for example the Visual Studio IDE.

To run the application follow these steps:

1. Clone the repository from the `master` branch
2. Download [Microsoft SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
3. Using SQL Server Management Studio, open a local server and create a  database called `DormitoryManagementSystemCloud01`
4. Open the SQL script called `create_clubs_schema.sql` from the `SQL` folder in SQL Server Management Studio and run it on this database
5. Download the [Visual Studio IDE](https://visualstudio.microsoft.com/)
6. Make sure you have installed the [.NET 8.0 SDK](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks)
7. Open the solution file `DormitoryManagementSystem.sln` of the C# solution in Visual Studio
8. Make sure the connection strings in `DormitoryManagementSystem.API/appsettings.Development.json` has the right server name and database name, corresponding to the database you just made
9. Run the application in `Debug` mode
10. This will open a browser with a basic UI, provided by [Swagger](https://swagger.io/), that allows you to try the different endpoints of the API. The ones that are fully implemented with persistence is the `Clubs` endpoints.


## How to run the tests of the application

1. In Visual Studio press `View` in the top bar and then choose `Test Explorer`.
2. This opens a test explorer in which you can see all the tests and run them.
