using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Tivix.FamilyBudget.Server.API.Middlewares;
using Tivix.FamilyBudget.Server.Core;
using Tivix.FamilyBudget.Server.Infrastructure;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Family Budget - Tivix App", Version = "v1" });
    c.EnableAnnotations();
});

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddIdentityCore<UserEntity>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddApiEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapHealthChecks("/healthz");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapIdentityApi<UserEntity>();

app.MapControllers();

app.Run();
