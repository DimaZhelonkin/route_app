FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
RUN ls -v
COPY . .
RUN ls -v
WORKDIR /src/Modules/IdentityServer/App/
RUN ls -v
RUN dotnet restore *.csproj
RUN dotnet build *.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Arl.IdentityServer.App.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Arl.IdentityServer.App.dll"]