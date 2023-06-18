FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mafia.Api/Mafia.Api.csproj", "Mafia.Api/"]
RUN dotnet restore "Mafia.Api/Mafia.Api.csproj"
COPY . .
WORKDIR "/src/Mafia.Api"
RUN dotnet build "Mafia.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mafia.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mafia.Api.dll"]
