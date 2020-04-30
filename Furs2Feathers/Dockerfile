FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln ./
COPY Furs2FeathersAPI/*.csproj Furs2FeathersAPI/
COPY Furs2Feathers.Domain/*.csproj Furs2Feathers.Domain/
COPY Furs2Feathers.DataAccess/*.csproj Furs2Feathers.DataAccess/
RUN dotnet restore

# Copy everything else and build
COPY . ./
RUN dotnet publish Furs2FeathersAPI -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Furs2FeathersAPI.dll"]
