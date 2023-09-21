# family-budget
Application for Tivix recruitment process

# Local setup

Required:
Postgre 15.4 or higher `https://www.postgresql.org/download/`
C# 11
.NET 6

Step by step:
1. Connect to Database
2. Run `Create database FamilyBudgetDB`
TIP: If you're new - you can use PgAdmin to connect to Postgres. It's simillar to SSMS or Azure Data in that regard.

3. Run in Visual Studio: `dotnet user-secrets init --project "src\Tivix.FamilyBudget.Server.Api"`
4. Add secret:
`dotnet user-secrets --project "src\Tivix.FamilyBudget.Server.Api" set "ConnectionStrings:DefaultConnection" "User ID=<USERNAME>;Password=<PASSWORD>;Host=localhost;Port=5432;Database=FamilyBudgetDB;"`


`dotnet user-secrets set "ConnectionStrings:DefaultConnection" "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=FamilyBudgetDB;"`

5. Run `update-database`

# Libraries, tools and approaches used:
ApiEndpoints for Request-EndPoint-Response(REPR) pattern on FamilyBudget.Server.API
https://github.com/ardalis/ApiEndpoints
Why? Controllers generally are a bit too big. Also, looks like it works great with mediatr, when you write it with 1 endpoint = 1 mediatr handler approach.
Haven't used it previously, so I wanted to test it out.

MediatR for Mediator pattern on FamilyBudget.Server.Core
https://github.com/jbogard/MediatR
Why? Simplicity, Single Responsibility.

Tests are only for FamilyBudget.Server.Core.
Why? It's the only part of the application that does data manipulation.

FluentValidation for Validation on FamilyBudget.Server.Core
https://docs.fluentvalidation.net/en/latest/
Why? Industry standard, ease of use.

xUnit for Tests
https://xunit.net/
Why? Personal preference.