# Contract Tracker Project Context

> **File Location:** `/contract-tracker-backend/docs/PROJECT_CONTEXT.md`  
> **Purpose:** Living documentation for AI pair programming and team onboarding  
> **Last Updated:** 2025-01-10 (Session 3)

## Project Overview
**Name:** Contract Tracker  
**Purpose:** Federal contract resource management system for tracking personnel, LCATs, rates, and profitability  
**Stage:** MVP Development - Contract Management Complete  
**Started:** January 2025  

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
│   │   └── Entities/
│   │       ├── Contract.cs          # ✅ Complete with burn rate logic
│   │       ├── LCAT.cs              # ✅ Working
│   │       └── Resource.cs          # ✅ Working
│   ├── ContractTracker.Infrastructure/ # Data access, external services
│   │   └── Data/
│   │       └── AppDbContext.cs      # ✅ UTC DateTime handling added
│   ├── ContractTracker.Api/          # Web API, controllers
│   │   ├── Controllers/
│   │   │   ├── ContractController.cs # ✅ Full CRUD + workflow
│   │   │   ├── LCATController.cs    # ✅ Fixed CreatedBy issue
│   │   │   └── ResourceController.cs # ✅ Working
│   │   └── DTOs/
│   │       └── ContractDTOs.cs      # ✅ Complete
│   └── docs/                         # Project documentation
│       ├── PROJECT_CONTEXT.md       # This file
│       ├── SESSION_CONTINUITY_GUIDE.md
│       └── NEXT_SESSION_START.md    # Session handoff notes
└── contract-tracker-frontend/
    ├── src/
    │   ├── components/               # React components
    │   │   ├── Contract/            # ✅ Contract Management UI
    │   │   ├── LCAT/                # ✅ LCAT management UI
    │   │   ├── Resource/            # ✅ Resource management UI
    │   │   └── Debug/               # ✅ Collapsible debug footer
    │   ├── services/                # ✅ API integration
    │   ├── types/                   # ✅ TypeScript definitions
    │   └── App.tsx                  # ✅ Updated with Contracts tab
    └── .env                         # Environment configuration

✅ = Completed and Working
⏳ = In Progress
```

## Current Working Features

### Contract Management (Session 3) ✅
- Create/Read/Update/Delete contracts
- Prime/Subcontractor relationship tracking
- Contract lifecycle (Draft → Active → Closed)
- Funding tracking and modifications
- Visual funding warnings (Critical/High/Medium/Low)
- Burn rate calculations (infrastructure ready)
- Contract types (T&M, Fixed Price, Cost Plus, Labor Hour)
- Standard FTE hours configuration

### LCAT Management (Session 2) ✅
- LCAT CRUD with published and default bill rates
- Position title mapping (one-to-many)
- Rate versioning with effective dates
- Inline batch editing with save-all
- Rate history viewing

### Resource Management (Session 2) ✅
- Add resources with type selection (W2/1099/Sub/Fixed)
- LCAT assignment from dropdown
- Filterable table view with inline editing
- Show calculated burdened cost
- Margin calculations with underwater alerts

### Debug Tools (Session 3) ✅
- Collapsible footer design
- API call logging with timing
- Request/response tracking
- System info panel

## Development Progress Log

### Session 1 (2025-01-10) - Initial Setup
**Completed:**
- ✅ Created Clean Architecture structure
- ✅ Set up PostgreSQL in Docker
- ✅ Created initial domain entities

### Session 2 (2025-01-10) - Frontend & API Integration
**Completed:**
- ✅ Fixed API connection issues (CORS, routing)
- ✅ Built Resource Management frontend
- ✅ Implemented LCAT Management UI
- ✅ Created ApiDebug component
- ✅ Resolved TypeScript/linting issues

### Session 3 (2025-01-10) - Contract Management
**Completed:**
- ✅ Full Contract entity with business logic
- ✅ Contract Management UI with funding warnings
- ✅ Burn rate calculation infrastructure
- ✅ Prime/Sub contractor support
- ✅ Contract lifecycle workflow
- ✅ DateTime UTC handling for PostgreSQL
- ✅ Debug footer improvements
- ✅ Fixed LCAT CreatedBy/ModifiedBy issue

## Business Rules Implemented

### Contract Management
- **Contract Identity:** Number may be customer's or ours depending on prime/sub status
- **Funding:** Total value vs funded value with modification tracking
- **Burn Rates:** Monthly, Quarterly, Annual calculations
- **Warnings:** Multi-level funding warnings based on percentage and time remaining
- **Hours:** Standard FTE (1912 default) with custom override capability

### Resource Management
- **Types:** W2Internal, Subcontractor, Contractor1099, FixedPrice
- **Wrap Rates:** 2.28x for W2, 1.15x for contractors (configurable)
- **Assignment:** Resources can be assigned to LCATs
- **Margins:** Automatic calculation with underwater detection

### LCAT Management  
- **Rate Hierarchy:** Published Rate → Default Bill Rate → Contract Override
- **Versioning:** All rates versioned with effective dates
- **Audit:** CreatedBy/ModifiedBy tracking (using "System" until auth added)

## Technical Decisions Made

### Architecture
- **Separate Repositories:** Frontend and backend in different repos
- **Clean Architecture:** Domain entities with private setters
- **DTOs:** Separate DTOs for Create, Update, and Read operations
- **No Auth Yet:** Using "System" as user placeholder

### Database
- **PostgreSQL:** For JSON support and scalability
- **Docker:** Database runs in container for portability
- **UTC DateTime:** All timestamps converted to UTC for PostgreSQL
- **Migrations:** Code-first with EF Core

### Frontend
- **TypeScript:** Full type safety
- **TailwindCSS:** Utility-first styling
- **Inline Editing:** Excel-like editing with batch saves
- **Visual Feedback:** Color-coded warnings and status indicators

## Known Issues & Technical Debt

### Current Issues
- Minor unspecified errors (non-blocking)
- No actual burn rate calculations (need timesheet integration)
- Complex calculations temporarily removed from controller

### Technical Debt
1. No authentication/authorization
2. No pagination on lists
3. No comprehensive error boundaries
4. No tests written
5. No audit logging
6. No conflict resolution for concurrent edits
7. No data export functionality

## Features Roadmap

### Phase 2: In Progress ⏳
**Resource-Contract Assignment** [NEXT PRIORITY]
- Link resources to specific contracts
- Set allocation percentages (FT/PT)
- Calculate actual burn rates
- Track multi-contract assignments

**Contract LCAT Rate Overrides**
- UI for contract-specific bill rates
- Override management interface
- Rate history tracking

**Financial Dashboard**
- Burn rate visualizations
- Funding health summary
- Depletion projections
- Contract profitability views

### Phase 3: Advanced Features [FUTURE]
- [ ] Permissions system (Super User vs PM vs User)
- [ ] Edit locking for concurrent users
- [ ] Historical timeline views
- [ ] Excel import/export functionality
- [ ] Clearance and certification tracking
- [ ] What-if scenario modeling
- [ ] Automated margin alerts
- [ ] Integration with payroll/timesheet systems
- [ ] Approval workflows for rate changes

## Environment Configuration

### Backend (.env or appsettings.json)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=ContractTracker;Username=postgres;Password=yourpassword"
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
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: yourpassword
    ports:
      - "5432:5432"
```

## API Endpoints Available

### Contract Endpoints
- `GET /api/Contract` - List all contracts
- `GET /api/Contract/{id}` - Get specific contract
- `POST /api/Contract` - Create contract
- `POST /api/Contract/{id}/activate` - Activate draft contract
- `POST /api/Contract/{id}/close` - Close active contract
- `POST /api/Contract/{id}/update-funding` - Update contract funding
- `DELETE /api/Contract/{id}` - Delete contract

### LCAT Endpoints
- `GET /api/LCAT` - List all LCATs
- `GET /api/LCAT/{id}` - Get specific LCAT
- `POST /api/LCAT` - Create LCAT
- `PUT /api/LCAT/{id}` - Update LCAT
- `DELETE /api/LCAT/{id}` - Delete LCAT

### Resource Endpoints
- `GET /api/Resource` - List all resources
- `GET /api/Resource/{id}` - Get specific resource
- `POST /api/Resource` - Create resource
- `PUT /api/Resource/{id}` - Update resource
- `PUT /api/Resource/batch` - Batch update resources
- `DELETE /api/Resource/{id}` - Delete resource

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

## Next Steps for Session 4

### Immediate Priority: Resource-Contract Assignment
1. Add ContractId to Resource entity (or use junction table)
2. Create assignment UI in Contract Management
3. Build allocation percentage controls
4. Calculate actual burn rates from assigned resources
5. Show resource costs per contract

### Then: Contract LCAT Rate Overrides UI
1. Create rate override interface
2. Show override vs default rates
3. Calculate impact on margins
4. Track override history

### Finally: Basic Financial Dashboard
1. Create dashboard component
2. Show all contracts with health indicators
3. Display burn rate trends
4. Add depletion countdown
5. Highlight at-risk contracts

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

## Notes & Decisions Log
- **2025-01-10 Session 1:** Initial project structure with Clean Architecture
- **2025-01-10 Session 2:** Frontend implementation, fixed API integration
- **2025-01-10 Session 3:** Complete Contract Management with funding tracking
- **Decision:** Defer auth to later phase, use "System" as placeholder user
- **Decision:** Implement burn rate calculations after resource assignments
- **Decision:** Use visual indicators (emojis/colors) for funding warnings

---
*This is a living document. Last updated after Session 3 completion.*