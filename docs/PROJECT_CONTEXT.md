# Contract Tracker Project Context

> **File Location:** `/contract-tracker-backend/docs/PROJECT_CONTEXT.md`  
> **Purpose:** Living documentation for AI pair programming and team onboarding  
> **Last Updated:** 2025-01-10 (Session 2)

## Project Overview
**Name:** Contract Tracker  
**Purpose:** Federal contract resource management system for tracking personnel, LCATs, rates, and profitability  
**Stage:** MVP Development - Core Features Working  
**Started:** August 2025  

## Business Context
### Problem Statement
As a Program Manager on federal government contracts, managing personnel and financials across multiple contracts requires tracking numerous moving parts: labor categories (LCATs), billing rates, resource assignments, and wrap rates. Currently, this information is scattered across spreadsheets, emails, and various systems, making it difficult to:
- Quickly assess team composition and costs
- Make informed staffing decisions
- Track rate changes over time
- Calculate actual vs. projected costs with wrap rates
- Ensure compliance with contract requirements

### Key Stakeholders
- **Primary Users:** Program Managers responsible for federal contract staffing and financials
- **Secondary Users:** 
  - Finance teams needing cost/revenue reports
  - HR teams coordinating hiring/onboarding
  - Proposal teams needing historical rate data
- **Admin Users:** System administrators managing access and data integrity

### Core Business Value
1. **Centralized Information Hub:** Single source of truth for all contract personnel and rates
2. **Financial Visibility:** Real-time understanding of team costs including wrap rates
3. **Improved Decision Making:** Quick access to data for staffing and negotiation decisions
4. **Compliance Tracking:** Ensure resources meet contract requirements (clearances, certs, etc.)
5. **Historical Analysis:** Track rate changes and resource movements over time

## Technical Architecture

### Stack Overview
- **Backend:** .NET 8, C#, ASP.NET Core Web API
- **Frontend:** React 19, TypeScript, TailwindCSS (separate repository)
- **Database:** PostgreSQL (containerized in Docker)
- **Architecture Pattern:** Clean Architecture (Domain-Driven Design)
- **API Style:** RESTful with OpenAPI/Swagger documentation
- **Testing:** xUnit, SpecFlow (BDD), FluentAssertions [TO BE IMPLEMENTED]
- **Containerization:** Docker, future cloud deployment planned

### Project Structure
```
contract-tracker/
├── contract-tracker-backend/
│   ├── ContractTracker.Domain/       # Domain entities, value objects
│   ├── ContractTracker.Infrastructure/ # Data access, external services
│   ├── ContractTracker.Api/          # Web API, controllers
│   └── docs/                         # Project documentation
│       ├── PROJECT_CONTEXT.md       # This file
│       └── SESSION_CONTINUITY_GUIDE.md
└── contract-tracker-frontend/
    ├── src/
    │   ├── components/               # React components
    │   │   ├── LCAT/                # LCAT management UI ✅
    │   │   ├── Resource/            # Resource management UI ✅
    │   │   └── Debug/               # API debug panel ✅
    │   ├── services/                # API integration ✅
    │   ├── hooks/                   # Custom React hooks
    │   └── types/                   # TypeScript definitions ✅
    └── .env                         # Environment configuration ✅
```

## Development Progress Log

### Session 1 (2025-08-07) - Initial Setup
**Completed:**
- ✅ Created Clean Architecture structure
- ✅ Set up PostgreSQL in Docker
- ✅ Created all domain entities with proper encapsulation
- ✅ Implemented Entity Framework configurations
- ✅ Created LCAT controller with CRUD operations
- ✅ Created Resource controller with basic operations
- ✅ Built LCAT Management UI with inline editing

**Technical Decisions Made:**
- Chose PostgreSQL for JSON support and scalability
- Separate repositories for frontend and backend
- No permissions initially (all users are super users)
- Rate versioning from day one for historical accuracy
- Default wrap rate: 2.28x for W2 employees

### Session 2 (2025-01-10) - Frontend Implementation & API Integration
**Completed:**
- ✅ Fixed CORS configuration in backend
- ✅ Created Resource Management frontend component
- ✅ Implemented inline editing with batch save for Resources
- ✅ Added visual feedback (yellow highlighting) for unsaved changes
- ✅ Created API debug panel for troubleshooting
- ✅ Fixed API route mapping (/api/LCAT and /api/Resource)
- ✅ Configured environment variables correctly
- ✅ Added proper TypeScript types for all entities
- ✅ Implemented service layer for API calls
- ✅ Added filtering and search capabilities
- ✅ Implemented underwater resource detection (negative margin alerts)
- ✅ Added comprehensive error handling and validation

**Issues Resolved:**
- **CORS blocking:** Fixed by properly configuring CORS middleware in Program.cs
- **404 on API calls:** Corrected routes to use /api/LCAT instead of /lcat
- **Port mismatch:** Updated .env to use correct port 5154
- **LCAT selection:** Fixed dropdown to properly pass GUID values
- **TypeScript errors:** Added proper typing for all state and props

**Known Issues & Fixes Applied:**
- **Issue:** CORS policy blocking frontend-backend communication
  - **Fix:** Added proper CORS configuration with AllowAnyOrigin for development
- **Issue:** API routes returning 404
  - **Fix:** Updated service files to use correct routes (/api/LCAT, /api/Resource)
- **Issue:** Resource creation failing with "LCAT not found"
  - **Fix:** Added validation and proper LCAT selection in dropdown

## Current Working Features

### ✅ LCAT Management
- Create new LCATs with published and default bill rates
- Position title mapping
- Inline editing of rates
- Batch save functionality
- Rate versioning with effective dates
- Visual feedback for unsaved changes

### ✅ Resource Management  
- Create resources with type selection (W2/1099/Sub/Fixed)
- LCAT assignment from dropdown
- Inline editing of resource properties
- Batch save for multiple edits
- Filtering by type, LCAT, active status
- Search by name or email
- Automatic burden cost calculation based on resource type
- Margin calculation and underwater resource detection
- Termination capability with end date

### ✅ API Integration
- Full CRUD operations for LCATs
- Full CRUD operations for Resources
- Batch update endpoints
- Proper error handling and validation
- CORS properly configured
- Swagger documentation available

## API Endpoints Available

### LCAT Endpoints
- `GET /api/LCAT` - Get all LCATs
- `GET /api/LCAT/{id}` - Get single LCAT
- `POST /api/LCAT` - Create new LCAT
- `POST /api/LCAT/batch-update-rates` - Batch update rates

### Resource Endpoints
- `GET /api/Resource` - Get all resources (with filtering)
- `GET /api/Resource/{id}` - Get single resource
- `POST /api/Resource` - Create new resource
- `PUT /api/Resource/{id}` - Update resource
- `PUT /api/Resource/batch` - Batch update resources
- `POST /api/Resource/{id}/terminate` - Terminate resource

## Business Rules Implemented

1. **Rate Management Hierarchy**
   - Published Rate: GSA ceiling rate
   - Default Bill Rate: Company standard fallback
   - All rates are versioned with effective dates

2. **Resource Type Calculations**
   - W2 Internal: 2.28x wrap rate
   - Subcontractor: 1.15x wrap rate
   - 1099 Contractor: 1.15x wrap rate
   - Fixed Price: 1.0x wrap rate

3. **Validation Rules**
   - Email must be unique per resource
   - LCAT names must be unique
   - Resources must be assigned to existing LCATs
   - Hourly rates must be positive
   - End dates cannot be before start dates

## Technical Decisions Made

### Backend Architecture
- **Controllers:** Separate controllers per entity (LCATController, ResourceController)
- **Route Pattern:** `/api/[controller]` for all endpoints
- **DTOs:** Separate Create, Update, and Read DTOs for each entity
- **Validation:** Controller-level validation before database operations
- **Transactions:** Used for batch operations

### Frontend Architecture
- **Component Structure:** Feature-based organization (LCAT/, Resource/)
- **State Management:** React hooks (useState, useEffect)
- **API Layer:** Centralized axios instance with interceptors
- **Type Safety:** Full TypeScript coverage with interfaces
- **UI Patterns:** 
  - Inline editing with batch saves
  - Visual feedback with color coding
  - Confirmation before destructive actions

### Development Patterns
- **Error Handling:** Try-catch with detailed console logging
- **Loading States:** Consistent loading indicators
- **User Feedback:** Success alerts and error messages
- **Code Style:** Functional components with hooks
- **API Calls:** Async/await pattern with proper error handling

## Environment Configuration

### Backend (.env or appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ContractTracker;Username=;Password="
  }
}
```

### Frontend (.env)
```
REACT_APP_API_URL=http://localhost:5154
```

### Docker Compose (for PostgreSQL)
```yaml
version: '3.8'
services:
  postgres:
    image: postgres:15
    environment:
      POSTGRES_DB: ContractTracker
      POSTGRES_USER: [username]
      POSTGRES_PASSWORD: [password]
    ports:
      - "5432:5432"
```

## Features Roadmap

### Phase 1: MVP ✅ [COMPLETED]
**LCAT Management**
- ✅ LCAT CRUD with published and default bill rates
- ✅ Position title mapping (one-to-many)
- ✅ Rate versioning with effective dates
- ✅ Inline batch editing with save-all
- ✅ Rate history viewing

**Resource Management**
- ✅ Add resources with type selection (W2/1099/Sub/Fixed)
- ✅ LCAT assignment from dropdown
- ✅ Filterable table view with inline editing
- ✅ Show calculated burdened cost
- ✅ Margin calculations with underwater alerts

### Phase 2: Contract & Financial Features [NEXT]
- ⏳ Contract management with LCAT rate overrides
- ⏳ PM-level rate override capabilities
- ⏳ Profitability calculations (Bill Rate vs Burdened Cost)
- ⏳ Contract-level financial rollups
- ⏳ Resource assignment to contracts
- ⏳ Multi-contract resource allocation

### Phase 3: Advanced Features [FUTURE]
- [ ] Permissions system (Super User vs PM vs User)
- [ ] Edit locking for concurrent users
- [ ] Historical timeline views
- [ ] Excel import/export functionality
- [ ] Clearance and certification tracking
- [ ] What-if scenario modeling
- [ ] Automated margin alerts
- [ ] Integration with payroll/timesheet systems

## Testing Strategy [TO BE IMPLEMENTED]

### Unit Tests (70%)
- Domain logic tests
- Service layer tests
- Component tests

### Integration Tests (20%)
- API endpoint tests
- Database integration tests
- External service tests

### E2E Tests (10%)
- Critical user journeys
- Cross-browser testing

## Development Workflow

### Local Development Commands
```bash
# Backend
cd contract-tracker-backend
dotnet restore
dotnet ef database update --project ContractTracker.Infrastructure --startup-project ContractTracker.Api
dotnet run --project ContractTracker.Api

# Frontend
cd contract-tracker-frontend
npm install
npm start
```

### Git Strategy
- **Separate Repositories:**
  - `contract-tracker-backend` (.NET API)
  - `contract-tracker-frontend` (React/TypeScript)
- **Main Branch:** `main` (production-ready)
- **Development Branch:** `develop`
- **Feature Branches:** `feature/[ticket-number]-description`
- **Commit Convention:** Conventional Commits (feat:, fix:, docs:, etc.)

## Next Steps

### Immediate Priorities
1. **Contract Management Module**
   - Create Contract entity and DTOs
   - Build Contract CRUD operations
   - Create Contract UI component
   - Implement contract-LCAT rate overrides

2. **Resource-Contract Assignment**
   - Add ContractId to Resource
   - Build assignment UI
   - Calculate profitability per resource

3. **Financial Dashboard**
   - Aggregate burn rate calculations
   - Margin analysis views
   - Underwater resource alerts
   - Export capabilities

### Technical Debt to Address
1. Add comprehensive error boundaries in React
2. Implement proper logging (Serilog for backend)
3. Add unit tests for critical business logic
4. Implement optimistic UI updates
5. Add data pagination for large datasets

## AI/ML/RPA Opportunities Identified

### Short-term
1. **Smart LCAT Matching:** ML to suggest best LCAT based on position title
2. **Rate Prediction:** Predict optimal pay rates based on historical data
3. **Anomaly Detection:** Flag unusual rate changes or margins

### Long-term
1. **Resource Demand Forecasting:** Predict future staffing needs
2. **Automated Compliance Checking:** RPA for clearance verification
3. **Intelligent Resource Allocation:** AI-optimized staff assignments
4. **Natural Language Queries:** "Show me all underwater resources on Contract X"

## Notes & Decisions Log
- **2025-08-07:** Initial project structure created with Clean Architecture
- **2025-08-07:** Chose PostgreSQL for better JSON support and scalability
- **2025-08-07:** Defined core business context - federal contract program management focus
- **2025-08-07:** Established key entities including wrap rate tracking and resource types
- **2025-01-10:** Completed MVP frontend with full CRUD operations
- **2025-01-10:** Fixed all API integration issues and established working patterns
- **2025-01-10:** Decided to defer permissions system to Phase 3

---
*This is a living document. Update it as the project evolves.*