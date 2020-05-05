#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.


FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env

WORKDIR /app
COPY ./Furs2Feathers.sln ./
COPY ["./Furs2FeathersAPI/*.csproj", "Furs2FeathersAPI/"]
COPY ["./Furs2Feathers.DataAccess/*.csproj", "Furs2Feathers.DataAccess/"]
COPY ["./Furs2Feathers.Domain/Furs2Feathers.Domain.csproj", "Furs2Feathers.Domain/"]
RUN dotnet restore 

COPY . ./
RUN dotnet publish Furs2FeathersAPI -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "Furs2FeathersAPI.dll"]