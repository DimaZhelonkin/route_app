# ASTRUM - Installation Guide

1) Run Docker containers

   ```bash
   # production mode
   docker-compose up -d 
   ```
   ```bash
   # development mode
   docker-compose -f .\docker-compose.dev.yml up -d 
   ```

2) You need to add migrations for relevant DbContexts if migrations haven't installed yet or DbContexts don't match to
   current models state.
   > **Note**: You don't need to run **database update**, because it will happen automatically <br>

3) if you need to auto migrate database and seed data, you need to add args to command `dotnet run`  
   For example:
   ```bash
   dotnet run --project .\src\Presentation\Astrum.Api\ /init
   ```   
   ```bash
   dotnet run --project .\src\Presentation\Astrum.Api\ /seed
   ```   
   ```bash
   dotnet run --project .\src\Presentation\Astrum.Api\ /migrate
   ```   

4) _**dev:**_ For work you should start 2 microservices:
    * Identity Server
    * Api

5) DbContexts bash commands:
   #### PersistedGrantDbContext
   ```bash
   # Add Migration
   -c PersistedGrantDbContext 
   -o Migrations/IdentityServer/PersistedGrantDb `
   -s ./src/Ark.Identity.App/ `
   -p ./src/Modules/Identity/Persistence/ `
   dotnet ef migrations add Initial `
   ```
   ```bash
   # Update Database
   -c PersistedGrantDbContext 
   -s ./src/Ark.Identity.App/ `
   -p ./src/Ark.Identity.Persistence/ `
   dotnet ef database update `
   ```
   #### ConfigurationDbContext
   ```bash
   # Add Migration
   -c ConfigurationDbContext 
   -o Migrations/IdentityServer/ConfigurationDb `
   -s ./src/Ark.Identity.App/ `
   -p ./src/Modules/Identity/Persistence/ `
   dotnet ef migrations add Initial `
   ```
   ```bash
   # Update Database
   -c ConfigurationDbContext 
   -s ./src/Ark.Identity.App/ `
   -p ./src/Ark.Identity.Persistence/ `
   dotnet ef database update `
   ```
   #### IdentityDbContext
   ```bash
   # Add Migration
   -c IdentityDbContext 
   -o Migrations/IdentityDb ` 
   -s ./src/Ark.Identity.App/ `
   -p ./src/Ark.Identity.Persistence/ `
   dotnet ef migrations add InitialIdentityDbMigration `
   ```
   ```bash
   # Update Database
   -c IdentityDbContext 
   -s ./src/Ark.Identity.App/ `
   -p ./src/Ark.Identity.Persistence/ `
   dotnet ef database update `
     ```
   #### ApplicationDbContext
   ```bash
   # Add Migration
   -c ApplicationDbContext 
   -o Migrations/Application ` 
   -s ./src/Api/ `
   -p ./src/Infrastructure/Astrum.Infrastructure.Persistence `
   dotnet ef migrations add InitialApplicationDbMigration `
   ```   
   ```bash
   # Update Database
   -c ApplicationDbContext 
   -s ./src/Api/ `
   -p ./src/Infrastructure/Astrum.Infrastructure.Persistence `
   dotnet ef database update `
   ```
   #### EventStoreDbContext
   ```bash
   # Add Migration
   -c EventStoreDbContext 
   -o Migrations/EventStore ` 
   -s ./src/Api/ `
   -p ./src/Infrastructure/Astrum.Infrastructure.Persistence `
   dotnet ef migrations add InitialEventStoreDbMigration `
   ```
   ```bash
   # Update Database
   -c EventStoreDbContext 
   -s ./src/Api/ `
   -p ./src/Infrastructure/Astrum.Infrastructure.Persistence `
   dotnet ef database update `
   ```
   #### AccountDbContext
   ```bash
   # Add Migration
   -c AccountDbContext 
   -o Migrations/Account ` 
   -s ./src/Api/ `
   -p ./src/Ark.Account.Persistence `
   dotnet ef migrations add InitialAccountDbMigration `
   ```
   ```bash
   # Update Database
   -c AccountDbContext 
   -s ./src/Api/ `
   -p ./src/Ark.Account.Persistence `
   dotnet ef database update `
   ```
   #### OrderingDbContext
   ```bash
   # Add Migration
   -c OrderingDbContext 
   -o Migrations/Ordering ` 
   -s ./src/Api/ `
   -p ./src/Ark.Ordering.Persistence `
   dotnet ef migrations add InitialOrderingDbMigration `
   ```
   ```bash
   # Update Database
   -c OrderingDbContext 
   -s ./src/Api/ `
   -p ./src/Ark.Ordering.Persistence `
   dotnet ef database update `
   ```
   #### NewsDbContext
   ```bash
   # Add Migration
   -c NewsDbContext 
   -o Migrations/News ` 
   -s ./src/Api/ `
   -p ./src/Ark.Ordering.Persistence `
   dotnet ef migrations add InitialNewsDbMigration `
   ```
   ```bash
   # Update Database
   -c NewsDbContext 
   -s ./src/Api/ `
   -p ./src/Ark.News.Persistence `
   dotnet ef database update `
   ```
