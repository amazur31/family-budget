# family-budget
Application for Tivix recruitment process

# Approaches used:
ApiEndpoints for Request-EndPoint-Response(REPR) pattern on FamilyBudget.Server.API
https://github.com/ardalis/ApiEndpoints
Why? Controllers are a bit too big. Also, works great with mediatr.

MediatR for Mediator pattern on FamilyBudget.Server.Core
https://github.com/jbogard/MediatR
Why? Simplicity, Single Responsibility.

Tests are only for FamilyBudget.Server.Core.
Why? It's the only part of the application that does data manipulation.