# Environment Configuration - Contract Tracker

> **File Purpose:** Development setup and configuration details  
> **Last Updated:** January 2025 (Session 6)  
> **Critical For:** Session startup, troubleshooting, onboarding

## Quick Start Checklist

### Pre-Session Verification
```bash
# 1. Check Docker (PostgreSQL)
docker ps                        # Should show postgres container running
docker-compose up -d            # Start if not running

# 2. Backend API
cd contract-tracker-backend
dotnet build                    # Should compile without errors
dotnet run --project ContractTracker.Api

# 3. Frontend React App
cd contract-tracker-frontend
npm start                       # Should start on http://localhost:3000

# 4. Verify Connectivity
# Browser: http://localhost:3000 should load app
# Test API: http://localhost:5154/swagger should show API docs
```

## Development Environment Setup

### Required Software Versions
```
Node.js:        18.19.0+ (LTS)
npm:            9.2.0+
.NET SDK:       8.0.101+
Docker:         24.0+
Git:            2.40+
PostgreSQL:     15+ (via Docker)
```

### Project Structure
```
C:\Projects\ContractTracker\              # Root project directory
├── contract-tracker-backend/             # .NET 8 Clean Architecture
│   ├── ContractTracker.Api/              # Web API project
│   ├── ContractTracker.Application/      # Business logic
│   ├── ContractTracker.Domain/           # Domain entities
│   ├── ContractTracker.Infrastructure/   # Data access
│   ├── docs/                             # Project documentation
│   └── docker-compose.yml               # PostgreSQL container
└── contract-tracker-frontend/            # React 18 + TypeScript
    ├── src/                              # Source code
    ├── public/                           # Static assets
    ├── package.json                      # Dependencies
    └── .env                              # Environment variables
```

## Database Configuration

### PostgreSQL Docker Setup
**Container Name:** `contracttracker-postgres`  
**Image:** `postgres:15-alpine`  
**Port:** `5432` (host) → `5432` (container)  
**Volume:** `postgres_data` (persistent storage)

```yaml
# docker-compose.yml
version: '3.8'
services:
  postgres:
    image: postgres:15-alpine
    container_name: contracttracker-postgres
    environment:
      POSTGRES_DB: ContractTracker
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: ContractTracker2025!
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data
    restart: unless-stopped

volumes:
  postgres_data:
```

### Database Connection
**Connection String:** `Host=localhost;Database=ContractTracker;Username=postgres;Password=ContractTracker2025!`  
**Entity Framework:** Code-first migrations  
**Current Schema:** Contracts, Resources, LCATs, ContractResources tables  

### Common Database Commands
```bash
# Start database
docker-compose up -d

# Check status
docker ps | grep postgres

# Connect to database
docker exec -it contracttracker-postgres psql -U postgres -d ContractTracker

# View logs
docker logs contracttracker-postgres

# Reset database (DANGER: loses all data)
docker-compose down -v
docker-compose up -d
```

## Backend API Configuration

### Development Server
**URL:** `http://localhost:5154` (HTTP) / `https://localhost:5155` (HTTPS)  
**Swagger UI:** `http://localhost:5154/swagger`  
**Health Check:** `http://localhost:5154/health` (planned)

### API Configuration
```csharp
// Program.cs key settings
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()));
```

### Environment Variables (Backend)
```
# appsettings.Development.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ContractTracker;Username=postgres;Password=ContractTracker2025!"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore": "Information"
    }
  }
}
```

### Common Backend Commands
```bash
# Build and run
cd contract-tracker-backend
dotnet build
dotnet run --project ContractTracker.Api

# Entity Framework migrations
dotnet ef migrations add "MigrationName" --project ContractTracker.Infrastructure --startup-project ContractTracker.Api
dotnet ef database update --project ContractTracker.Infrastructure --startup-project ContractTracker.Api

# Check project references
dotnet list reference
```

## Frontend Configuration

### Development Server
**URL:** `http://localhost:3000`  
**Build Output:** `build/` directory  
**Hot Reload:** Enabled (automatically reloads on file changes)

### Environment Variables (Frontend)
```bash
# .env file in contract-tracker-frontend/
REACT_APP_API_URL=http://localhost:5154
REACT_APP_ENV=development
REACT_APP_VERSION=1.0.0
```

### Package.json Scripts
```json
{
  "scripts": {
    "start": "react-scripts start",
    "build": "react-scripts build",
    "test": "react-scripts test",
    "eject": "react-scripts eject",
    "lint": "eslint src --ext .ts,.tsx",
    "lint:fix": "eslint src --ext .ts,.tsx --fix"
  }
}
```

### Common Frontend Commands
```bash
# Install dependencies
cd contract-tracker-frontend
npm install

# Start development server
npm start

# Build for production
npm run build

# Run tests
npm test

# Fix linting issues
npm run lint:fix
```

## IDE Configuration

### Visual Studio Code Settings
```json
// .vscode/settings.json (recommended)
{
  "typescript.preferences.importModuleSpecifier": "relative",
  "editor.formatOnSave": true,
  "editor.codeActionsOnSave": {
    "source.organizeImports": true,
    "source.fixAll.eslint": true
  },
  "files.associations": {
    "*.json": "jsonc"
  }
}
```

### Recommended Extensions
- **C# Dev Kit** (Microsoft) - Backend development
- **ES7+ React/Redux/React-Native snippets** - Frontend development
- **TypeScript Importer** - Auto-import management
- **Prettier - Code formatter** - Code formatting
- **ESLint** - Code quality and linting
- **GitLens** - Git integration and history

## Troubleshooting Guide

### API Won't Start
```bash
# Check port availability
netstat -ano | findstr :5154
# Kill process if needed
taskkill /PID <PID> /F

# Check .NET version
dotnet --version

# Rebuild solution
dotnet clean
dotnet build
```

### Database Connection Issues
```bash
# Verify container is running
docker ps | grep postgres

# Check container logs
docker logs contracttracker-postgres

# Test connection
docker exec -it contracttracker-postgres pg_isready -U postgres

# Restart container
docker restart contracttracker-postgres
```

### Frontend Build Issues
```bash
# Clear npm cache
npm cache clean --force

# Delete node_modules and reinstall
rm -rf node_modules package-lock.json
npm install

# Check TypeScript errors
npx tsc --noEmit

# Clear React cache
rm -rf build
npm start
```

### CORS Errors
**Symptom:** Network errors in browser console  
**Solution:** Verify CORS configuration in Program.cs  
**Check:** Ensure frontend .env has correct REACT_APP_API_URL  

### Database Migration Errors
**Symptom:** Entity Framework errors on startup  
**Solution:** Run migrations manually  
```bash
dotnet ef database update --project ContractTracker.Infrastructure --startup-project ContractTracker.Api
```

## Performance Monitoring

### Development Metrics
- **API Response Time:** <500ms for dashboard loads
- **Frontend Bundle Size:** <2MB total
- **Database Query Time:** <100ms for typical operations
- **Memory Usage:** <500MB for API, <200MB for React dev server

### Monitoring Tools
- **Browser DevTools:** Network tab for API calls, Performance tab for React rendering
- **Entity Framework Logging:** Query execution times and patterns
- **Docker Stats:** `docker stats` for container resource usage

---

*This configuration supports rapid development while maintaining production-readiness. Update as environment evolves.*