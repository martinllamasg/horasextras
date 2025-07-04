FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "HorasExtrasAppClean.csproj"
RUN dotnet build "HorasExtrasAppClean.csproj" -c Release -o /app/build
RUN dotnet publish "HorasExtrasAppClean.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HorasExtrasAppClean.dll"]