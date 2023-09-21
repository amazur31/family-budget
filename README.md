# family-budget
Application for Tivix recruitment process

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