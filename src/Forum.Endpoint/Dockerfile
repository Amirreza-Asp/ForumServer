#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Forum.Endpoint/Forum.Endpoint.csproj", "src/Forum.Endpoint/"]
COPY ["src/Forum.Infrastructure/Forum.Infrastructure.csproj", "src/Forum.Infrastructure/"]
COPY ["src/Forum.Application/Forum.Application.csproj", "src/Forum.Application/"]
COPY ["src/Forum.Domain/Forum.Domain.csproj", "src/Forum.Domain/"]
COPY ["src/Forum.Persistence/Forum.Persistence.csproj", "src/Forum.Persistence/"]
RUN dotnet restore "src/Forum.Endpoint/Forum.Endpoint.csproj"
COPY . .
WORKDIR "/src/src/Forum.Endpoint"
RUN dotnet build "Forum.Endpoint.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Forum.Endpoint.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Forum.Endpoint.dll"]