FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY EduardTeodor.DbMigrator/EduardTeodor.DbMigrator.csproj EduardTeodor.DbMigrator/
RUN dotnet restore EduardTeodor.DbMigrator/EduardTeodor.DbMigrator.csproj

COPY EduardTeodor.DbMigrator/Program.cs EduardTeodor.DbMigrator/
COPY EduardTeodor.DbMigrator/MigrationRunner.cs EduardTeodor.DbMigrator/
COPY EduardTeodor.DbMigrator/Migrations/ EduardTeodor.DbMigrator/Migrations/
RUN dotnet publish EduardTeodor.DbMigrator/EduardTeodor.DbMigrator.csproj \
    --configuration Release --no-restore --output /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:11.0 AS final
WORKDIR /app
COPY --from=build /app/publish/ ./
USER $APP_UID

ENTRYPOINT ["dotnet", "EduardTeodor.DbMigrator.dll"]
