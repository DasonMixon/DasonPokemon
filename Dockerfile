# Build frontend
FROM node:alpine AS frontend-build
WORKDIR /app
COPY . .
RUN npm ci && npm run build

# Build backend
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source
COPY *.sln .
COPY src/*.csproj ./src/
RUN dotnet restore
COPY src/. ./src/
WORKDIR /source/src
RUN dotnet publish -c release -o /app --no-restore

# Create runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --frontend-build /app/dist/dason-pokemon ./
COPY --from=build /app ./

# TODO: Replace dll name with correct one
ENTRYPOINT ["dotnet", "aspnetapp.dll"]
EXPOSE 80
EXPOSE 443