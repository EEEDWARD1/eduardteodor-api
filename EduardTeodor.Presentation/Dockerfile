FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY EduardTeodor.Domain/EduardTeodor.Domain.csproj EduardTeodor.Domain/
COPY EduardTeodor.Application/EduardTeodor.Application.csproj EduardTeodor.Application/
COPY EduardTeodor.Infrastructure/EduardTeodor.Infrastructure.csproj EduardTeodor.Infrastructure/
COPY EduardTeodor.Presentation/EduardTeodor.Presentation.csproj EduardTeodor.Presentation/
RUN dotnet restore EduardTeodor.Presentation/EduardTeodor.Presentation.csproj

COPY EduardTeodor.Domain/ EduardTeodor.Domain/
COPY EduardTeodor.Application/ EduardTeodor.Application/
COPY EduardTeodor.Infrastructure/ EduardTeodor.Infrastructure/
COPY EduardTeodor.Presentation/ EduardTeodor.Presentation/
RUN dotnet publish EduardTeodor.Presentation/EduardTeodor.Presentation.csproj \
    --configuration Release --no-restore --output /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:11.0 AS final
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
COPY --from=build /app/publish/ ./
USER $APP_UID

ENTRYPOINT ["dotnet", "EduardTeodor.Presentation.dll"]
