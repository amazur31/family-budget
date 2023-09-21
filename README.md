# family-budget

Application for Tivix recruitment process

---
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

5. Run `update-database`
---
# Libraries, tools and approaches used:

**Is it REST or not?**

According to Microsoft api design:
https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design
Which quotes Leonard Richardson that proposed following maturity for WebApis:
`Level 0: Define one URI, and all operations are POST requests to this URI.
Level 1: Create separate URIs for individual resources.
Level 2: Use HTTP methods to define operations on resources.
Level 3: Use hypermedia (HATEOAS, described below).`
This API IMO qualifies as a Level 2 API.

**ApiEndpoints for Request-EndPoint-Response(REPR) pattern on FamilyBudget.Server.API**

https://github.com/ardalis/ApiEndpoints
Why? Controllers generally are a bit too big. Also, looks like it works great with mediatr, when you write it with 1 endpoint = 1 mediatr handler approach.
Haven't used it previously, so I wanted to test it out.

**MediatR for Mediator pattern on FamilyBudget.Server.Core**

https://github.com/jbogard/MediatR
Why? Simplicity, Single Responsibility.

**Tests are only for FamilyBudget.Server.Core.**

Why? It's the only part of the application that does data manipulation.

**No repositories wrapping EF**

Why? Time savings, as well as the fact that EF implements UoW and repository patterns out-of-the box, as well as providing
useful test helpers like InMemoryDb.

**FluentValidation for Validation on FamilyBudget.Server.Core**

https://docs.fluentvalidation.net/en/latest/
Why? Industry standard, ease of use.

**xUnit for Tests**

https://xunit.net/
Why? Personal preference.
