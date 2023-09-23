#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["src/Tivix.FamilyBudget.Server.API/Tivix.FamilyBudget.Server.API.csproj", "src/Tivix.FamilyBudget.Server.API/"]
COPY ["src/Tivix.FamilyBudget.Server.Core/Tivix.FamilyBudget.Server.Core.csproj", "src/Tivix.FamilyBudget.Server.Core/"]
COPY ["src/Tivix.FamilyBudget.Server.Infrastructure/Tivix.FamilyBudget.Server.Infrastructure.csproj", "src/Tivix.FamilyBudget.Server.Infrastructure/"]
RUN dotnet restore "src/Tivix.FamilyBudget.Server.API/Tivix.FamilyBudget.Server.API.csproj"
COPY . .
WORKDIR "/src/src/Tivix.FamilyBudget.Server.API"
RUN dotnet build "Tivix.FamilyBudget.Server.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Tivix.FamilyBudget.Server.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Tivix.FamilyBudget.Server.API.dll"]