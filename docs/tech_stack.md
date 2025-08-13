# Technology Stack - Contract Tracker

> **File Purpose:** Current architecture, versions, and technology decisions  
> **Last Updated:** January 2025 (Session 6)  
> **Critical For:** Environment setup, dependency management, deployment

## Current Architecture

### Backend Stack
**Framework:** .NET 8 (LTS)  
**Architecture Pattern:** Clean Architecture with Domain-Driven Design  
**API Style:** RESTful with OpenAPI/Swagger documentation  
**Authentication:** Placeholder (planned: Azure AD / OAuth 2.0)  

```
ContractTracker.Domain/          # Entities, value objects, business rules
├── Entities/                   # Contract, Resource, LCAT, ContractResource
├── Enums/                      # ResourceType, ContractType, ContractStatus
└── ValueObjects/               # BurnRateAnalysis, financial calculations

ContractTracker.Application/     # Business logic and services
├── Services/                   # DashboardAnalyticsService, business operations
├── Interfaces/                 # Repository and service contracts
└── DTOs/                       # Data transfer objects

ContractTracker.Infrastructure/  # Data access and external services
├── Data/                       # Entity Framework Core, DbContext
├── Configurations/             # EF Core entity configurations
└── Repositories/               # Data access implementation

ContractTracker.Api/            # Web API controllers and configuration
├── Controllers/                # REST endpoints
├── DTOs/                       # API-specific data transfer objects
└── Program.cs                  # Application startup and configuration
```

### Database
**Primary Database:** PostgreSQL 15+ in Docker  
**ORM:** Entity Framework Core 8.0  
**Migration Strategy:** Code-first with automated migrations  
**Connection Management:** Connection pooling, 30-second timeout  

### Frontend Stack
**Framework:** React 18 with TypeScript 5.0+  
**Build Tool:** Create React App (CRA) with TypeScript template  
**State Management:** React Hooks (useState, useEffect)  
**Routing:** React Router v6  
**UI Framework:** Custom CSS with Flexbox/Grid (no external UI library)  

```
src/
├── components/                 # React components organized by feature
│   ├── Contract/              # Contract management UI
│   ├── Dashboard/             # Financial dashboard and analytics
│   ├── LCAT/                  # LCAT management with inline editing
│   ├── Resource/              # Resource management and allocation
│   └── Debug/                 # Development and troubleshooting tools
├── services/                  # API integration layer
│   ├── dashboardService.ts    # Dashboard analytics API calls
│   ├── contractService.ts     # Contract CRUD operations
│   ├── resourceService.ts     # Resource and LCAT operations
│   └── apiConfig.ts           # Base API configuration
├── types/                     # TypeScript type definitions
│   ├── dashboard.ts           # Dashboard and analytics types
│   ├── contract.ts            # Contract and business types
│   └── common.ts              # Shared types and interfaces
└── utils/                     # Helper functions and constants
    ├── calculations.ts        # Financial calculation utilities
    └── formatting.ts          # Data display formatting
```

### Data Visualization
**Primary Charts:** Recharts 2.8+ (React-native chart library)  
**Chart Types:** Line charts, bar charts, pie charts, area charts  
**Styling:** Custom CSS with responsive design principles  
**Interactivity:** Tooltips, legends, click handlers  

## Development Environment

### Required Software
- **Node.js:** 18+ (LTS recommended)
- **npm:** 9+ (package management)
- **.NET SDK:** 8.0+ (backend development)
- **Docker:** Latest (PostgreSQL container)
- **Git:** Latest (version control)

### IDE Configuration
**Recommended:** Visual Studio Code with extensions:
- C# Dev Kit (backend development)
- ES7+ React/Redux/React-Native snippets
- TypeScript Importer
- Prettier (code formatting)
- ESLint (code quality)

### Local Development Ports
```
PostgreSQL Database:     5432 (Docker container)
Backend API:             5154 (https://localhost:5154)
Frontend Development:    3000 (http://localhost:3000)
Swagger UI:             5154/swagger (API documentation)
```

### Environment Variables
**Backend (.NET):**
```
ConnectionStrings__DefaultConnection=Host=localhost;Database=ContractTracker;Username=postgres;Password=yourpassword
```

**Frontend (React):**
```
REACT_APP_API_URL=http://localhost:5154
REACT_APP_ENV=development
```

## Dependency Management

### Backend NuGet Packages
```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="8.0.1" />
<PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="8.0.0" />
<PackageReference Include="Microsoft.AspNetCore.OpenApi" Version="8.0.1" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
```

### Frontend npm Dependencies
```json
{
  "dependencies": {
    "react": "^18.2.0",
    "react-dom": "^18.2.0",
    "react-router-dom": "^6.8.0",
    "react-scripts": "5.0.1",
    "recharts": "^2.8.0",
    "typescript": "^5.0.0"
  },
  "devDependencies": {
    "@types/react": "^18.2.0",
    "@types/react-dom": "^18.2.0",
    "eslint": "^8.0.0",
    "prettier": "^3.0.0"
  }
}
```

## Database Schema

### Current Tables
```sql
-- Core business entities
Contracts              # Contract master data and funding
Resources              # Personnel and their attributes
LCATs                  # Labor categories and rate structures
ContractResources      # Junction table for resource assignments

-- Metadata tables
__EFMigrationsHistory  # Entity Framework migration tracking
```

### Key Relationships
- **Resources → LCATs:** Many-to-one (resource has one LCAT)
- **Contracts ↔ Resources:** Many-to-many through ContractResources
- **ContractResources:** Tracks allocation percentage and effective dates

## API Design Patterns

### RESTful Conventions
```
GET    /api/Contract              # List all contracts
GET    /api/Contract/{id}         # Get specific contract
POST   /api/Contract              # Create new contract
PUT    /api/Contract/{id}         # Update existing contract
DELETE /api/Contract/{id}         # Delete contract

GET    /api/Dashboard/complete    # Comprehensive dashboard data
GET    /api/Dashboard/overview    # Portfolio overview metrics
```

### DTO Pattern
- **Separate DTOs for input and output:** ContractCreateDTO, ContractUpdateDTO, ContractDTO
- **Nested DTOs for complex operations:** CompleteDashboardDTO with embedded metrics
- **Validation attributes:** Required fields, range validations, business rules

### Error Handling
- **HTTP Status Codes:** 200 (success), 201 (created), 400 (bad request), 404 (not found), 500 (server error)
- **Consistent Error Format:** { "error": "message", "details": [...] }
- **Validation Errors:** Field-specific error messages for client-side display

## Deployment Architecture

### Current State (Development)
- **Backend:** Local development server (dotnet run)
- **Frontend:** Create React App development server (npm start)
- **Database:** Docker container with persistent volume
- **File Storage:** Local file system (planning cloud storage)

### Planned Production Architecture
- **Backend:** Azure App Service or AWS Elastic Beanstalk
- **Frontend:** Azure Static Web Apps or AWS S3 + CloudFront
- **Database:** Azure Database for PostgreSQL or AWS RDS
- **File Storage:** Azure Blob Storage or AWS S3
- **Monitoring:** Application Insights or CloudWatch

## Security Considerations

### Current Security (Development)
- **API CORS:** Configured for localhost development
- **Authentication:** Placeholder "System" user (not production-ready)
- **Data Validation:** Basic input validation and business rule enforcement
- **HTTPS:** Development certificates (not production-ready)

### Planned Security (Production)
- **Authentication:** Azure AD integration with role-based access
- **Authorization:** Role-based permissions (Admin, Manager, ReadOnly)
- **Data Protection:** Encryption at rest and in transit
- **Audit Logging:** Complete change tracking and access logging

---

*This technology stack balances modern capabilities with proven stability. Regular updates ensure security and performance optimization.*