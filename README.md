# Family Budget Application

This repository contains the Family Budget application, which is part of the Tivix recruitment process.

## TODO

- Implement budget deletion with cascade (or set IsDeleted flag)
- Implement category deletion with cascade (or set IsDeleted flag)
- Implement category update (name)
- Dockerize the application
- Set up a CI/CD pipeline
- If time permits, work on the frontend

## Local Setup

### Requirements

- PostgreSQL 15.4 or higher [Download PostgreSQL](https://www.postgresql.org/download/)
- C# 11
- .NET 8 [Download .NET](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

### Steps

1. Connect to the PostgreSQL database.

2. Create a database named "FamilyBudgetDB".

   TIP: If you're new to PostgreSQL, you can use PgAdmin to connect to it, which is similar to SSMS or Azure Data Studio.

3. Run the following command in Visual Studio to initialize user secrets for the project:

`dotnet user-secrets init --project "src\Tivix.FamilyBudget.Server.Api"`

4. Add the database connection secret:

`dotnet user-secrets --project "src\Tivix.FamilyBudget.Server.Api" set "ConnectionStrings:DefaultConnection" "User ID=<USERNAME>;Password=<PASSWORD>;Host=localhost;Port=5432;Database=FamilyBudgetDB;"`

5. Run the database migration using the following command:

`update-database`

## Authorization

1. Send a request to `/login` to obtain a Bearer Token.

Default credentials:

```
Email: example@example.com
Pass: Dupa123!
```

Example login request body:

```json
{
  "email": "example@example.com",
  "password": "Dupa123!",
  "twoFactorCode": "",
  "twoFactorRecoveryCode": ""
}
```

2. Authorize in Swagger using the bearer token.

## Frequently Asked Questions (FAQ)

### How does this project follow API design best practices?

According to Microsoft API design best practices [documentation](https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design), this API follows the proposed API maturity levels:

- Level 0: Define one URI, and all operations are POST requests to this URI.
- Level 1: Create separate URIs for individual resources.
- Level 2: Use HTTP methods to define operations on resources.
- Level 3: Use hypermedia (HATEOAS).

This API is considered to be at Level 2 as it follows the best practice of using HTTP methods to define operations on resources.

### Why did you decide to go with the controller-less route?

This project utilizes the [ApiEndpoints](https://github.com/ardalis/ApiEndpoints) library for several reasons:

- Controllers can become too large, and ApiEndpoints helps manage this issue.
- It works seamlessly with MediatR, especially when following the "1 endpoint = 1 MediatR handler" approach.

If you haven't used ApiEndpoints before, it's a great opportunity to test it out and see how it can enhance your project.

### Why did you decide to go with MediatR?

MediatR was chosen for its simplicity, adherence to the Single Responsibility Principle, and compatibility with ApiEndpoints.

### Why did you only test FamilyBudget.Server.Core?

The decision to test only FamilyBudget.Server.Core was based on the fact that it is the part of the application responsible for data manipulation. Testing other parts that don't manipulate data seemed unnecessary.

### Why did you not wrap EF in repositories / services?

EF (Entity Framework) was not wrapped in repositories or services for two main reasons:

- EF already implements the Unit of Work and Repository patterns out-of-the-box.
- EF provides useful test helpers like InMemoryDb, making it easier to write tests.

### Why did you use FluentValidation?

FluentValidation was chosen because it is an industry-standard library known for its ease of use in implementing validation logic.

### Why did you use xUnit?

The choice of xUnit as the testing framework is a matter of personal preference.

### Why did you use .NET 8 RC1?

.NET 8 RC1 was used to explore and test new features introduced in this version, particularly the new Identity features. This experimentation was done in a non-production environment to assess the feasibility of using these features.

### Why did you use NSubstitute?

The use of NSubstitute over other mocking libraries was influenced by recent events related to Moq.

### Why so many models?

This project utilizes multiple models for each command, allowing for better control over returned data. It follows a vertical architecture approach to neatly organize and manage data models.

### Learnings, and what I'd do differently?

To start off, I want to acknowledge that this application is primarily intended for recruitment purposes, and the objective is to present my skills in the best light possible. However, rather than adhering to my usual routine, I made the deliberate choice to experiment with some recent software techniques that have captured my attention. Specifically, I delved into the world of the Resource-Endpoint Pattern (REPR) and integrated .NET 8 Identity.

The REPR pattern excels in defining how endpoints function within an application, although it may not be the most suitable approach for managing data or resources associated with those endpoints. It is crucial to ensure that this pattern aligns seamlessly with the application's requirements.

Furthermore, I explored the Ardalis [ApiEndpoints](https://github.com/ardalis/ApiEndpoints) library, which, while commendable, exhibits certain limitations, such as its inability to handle multiple data bindings.

In retrospect, I believe I might have pursued an alternative strategy for handling requests and responses. While my chosen approach provided enhanced control over the data returned, it could potentially result in maintenance challenges, particularly when introducing new parameters to a data model. A more pragmatic design approach might involve the creation of a generic mapper, streamlining the integration of MediatR Commands and Queries within endpoints. Presently, although MediatR integration is feasible, it is not always straightforward, and I occasionally found it necessary to create new data structures solely to specify how data should be mapped.

In summary, I derived significant value from this experience, and I eagerly welcome the opportunity to engage in similar endeavors in the future. Nonetheless, I intend to explore alternative methodologies. For instance, I am inclined to investigate the use of MinimalAPI with MediatR integration, as it appears to offer a simpler and more effective approach, as demonstrated by Nick Chapsas in his video [here](https://youtu.be/euUg_IHo7-s?si=7yKLr1n5KpS7FeU3).
