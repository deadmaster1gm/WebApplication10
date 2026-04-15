FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["WebApplication10.csproj", "./"]
RUN dotnet restore "WebApplication10.csproj"

COPY . .
RUN dotnet publish "WebApplication10.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "WebApplication10.dll"]
