FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/Presentation/Presentation.csproj", "src/Presentation/"]
COPY ["src/Infrastructure/Infrastructure.csproj", "src/Infrastructure/"]
COPY ["src/Core/Domain/Domain.csproj", "src/Core/Domain/"]
COPY ["src/Core/Application/Application.csproj", "src/Core/Application/"]
RUN dotnet restore "src/Presentation/Presentation.csproj"
COPY . .
RUN dotnet build "src/Presentation/Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Presentation/Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]