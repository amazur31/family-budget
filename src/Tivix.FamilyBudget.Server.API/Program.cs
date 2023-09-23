using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Tivix.FamilyBudget.Server.API.Middlewares;
using Tivix.FamilyBudget.Server.Core;
using Tivix.FamilyBudget.Server.Infrastructure;
using Tivix.FamilyBudget.Server.Infrastructure.DAL;
using Tivix.FamilyBudget.Server.Infrastructure.DAL.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Family Budget - Tivix App", Version = "v1" });
    c.EnableAnnotations();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "Bearer",
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddControllers();

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<UserProviderMiddleware>();

builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddHealthChecks();
builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
builder.Services.AddAuthorizationBuilder();
builder.Services.AddIdentityCore<UserEntity>()
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddApiEndpoints();

var app = builder.Build();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.InjectStylesheet("/swagger-ui.css");
    });
}
app.MapHealthChecks("/healthz");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<UserProviderMiddleware>();

app.MapIdentityApi<UserEntity>();

app.MapControllers();

app.Run();
