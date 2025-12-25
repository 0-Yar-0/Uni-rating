# Build frontend
FROM node:18-alpine AS node-build
WORKDIR /app/frontend
COPY frontend/package*.json ./
RUN npm ci --silent
COPY frontend/ .
# Force Vite to emit build into ./dist so we can reliably copy it later
RUN npm run build -- --outDir dist

# Build .NET backend and include frontend build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
# Copy frontend build into backend/wwwroot
RUN mkdir -p backend/wwwroot
COPY --from=node-build /app/frontend/dist backend/wwwroot/

# Skip frontend MSBuild step (built earlier in node-build stage) to avoid requiring npm in the SDK image
RUN dotnet publish backend/backend.csproj -c Release -o /app/publish -p:SkipFrontendBuild=true

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:80
EXPOSE 80
ENTRYPOINT ["dotnet", "backend.dll"]
