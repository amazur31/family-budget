    
# family-budget

Application for Tivix recruitment process

---
<h1># Local Setup </h1>

Required:
Postgre 15.4 or higher `https://www.postgresql.org/download/`
C# 11
.NET 8 https://dotnet.microsoft.com/en-us/download/dotnet/8.0

Step by step:
1. Connect to Database
2. Run `Create database FamilyBudgetDB`
TIP: If you're new - you can use PgAdmin to connect to Postgres. It's simillar to SSMS or Azure Data in that regard.

3. Run in Visual Studio: `dotnet user-secrets init --project "src\Tivix.FamilyBudget.Server.Api"`
4. Add secret:
`dotnet user-secrets --project "src\Tivix.FamilyBudget.Server.Api" set "ConnectionStrings:DefaultConnection" "User ID=<USERNAME>;Password=<PASSWORD>;Host=localhost;Port=5432;Database=FamilyBudgetDB;"`

5. Run `update-database`


<h1># Authorization </h1>

1. Shoot off a request at /login for Bearer Token</br>
Default credentials:


`email: example@example.com
pass: Dupa123!`
<br>
Login request:
</br>
`
{
  "email": "example@example.com",
  "password": "Dupa123!",
  "twoFactorCode": "",
  "twoFactorRecoveryCode": ""
}
`
</br>

2. Authorize in swagger with the bearer token

<h1># Frequently Asked Questions (FAQ)</h1>

<h2>How does this project follow API design best practices?</h2>
<p>According to Microsoft API design best practices as outlined in their documentation
    <a href="https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design" target="_blank">here</a>, the proposed API maturity levels are:</p>
<blockquote>
    <p>Level 0: Define one URI, and all operations are POST requests to this URI.</p>
    <p>Level 1: Create separate URIs for individual resources.</p>
    <p>Level 2: Use HTTP methods to define operations on resources.</p>
    <p>Level 3: Use hypermedia (HATEOAS, described below).</p>
</blockquote>
<p>This API is considered to be at Level 2, as it follows the best practice of using HTTP methods to define operations on resources.</p>

<h2>Why did you decide to go with the controller-less route?</h2>
<p>This project utilizes the <a href="https://github.com/ardalis/ApiEndpoints" target="_blank">ApiEndpoints</a> library.</p>
<p>Why? The use of ApiEndpoints is beneficial for several reasons:</p>
<ol>
    <li>Controllers generally tend to become too large, and ApiEndpoints helps manage this issue.</li>
    <li>It works seamlessly with MediatR, especially when following the "1 endpoint = 1 MediatR handler" approach.</li>
</ol>
<p>If you haven't used ApiEndpoints before, it's a great opportunity to test it out and see how it can enhance your project.</p>

<h2>Why did you decide to go with MediatR</h2>
<p>Simplicity, Single Responsibility, as well as goes well with ApiEndpoints.</p>

<h2>Why did you only test FamilyBudget.Server.Core?</h2>
<p>Why? It's the only part of the application that does data manipulation. If it ain't fiddlin' with data, then why test the fiddlin' of the data?</p>

<h2>Why did you not wrap EF in repositories / services?</h2>
<p>Why? Time savings, as well as the fact that EF implements UoW and repository patterns out-of-the box, as well as providing
useful test helpers like InMemoryDb.</p>

<h2>Why did you use FluentValidation?</h2>
<p>Why? Industry standard, ease of use.</p>

<h2>Why did you use xUnit?</h2>
<p>Why? Personal preference.</p>

<h2>Why did you use .NET 8 RC1?</h2>
<p>Why? I wanted to test out a bunch of new features rolled out in .NET 8 RC1 - namely the new Identity
I wouldn't do it in Production environment - but since it's an app where I can try out fancy things
then so be it.</p>

<footer>
    <p>&copy; 2023 aezakmi software</p>
</footer>
