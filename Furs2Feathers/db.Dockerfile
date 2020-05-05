FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./Furs2Feathers.sln ./
COPY ["./Furs2FeathersAPI/*.csproj", "Furs2FeathersAPI/"]
COPY ["./Furs2Feathers.DataAccess/*.csproj", "Furs2Feathers.DataAccess/"]
COPY ["./Furs2Feathers.Domain/Furs2Feathers.Domain.csproj", "Furs2Feathers.Domain/"]
RUN dotnet restore

COPY .config ./
RUN dotnet tool restore

# Copy everything else and generate SQL script from migrations
COPY . ./
RUN dotnet ef migrations script -p Furs2Feathers.DataAccess -s Furs2FeathersAPI -o init-db.sql

# Build runtime image
FROM postgres:12.0
WORKDIR /docker-entrypoint-initdb.d
ENV POSTGRES_PASSWORD 35122jhb
COPY --from=build-env /app/init-db.sql .