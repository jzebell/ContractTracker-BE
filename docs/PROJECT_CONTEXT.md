# Contract Tracker Project Context

> **File Location:** `/contract-tracker-backend/docs/PROJECT_CONTEXT.md`  
> **Purpose:** Living documentation for AI pair programming and team onboarding  
> **Last Updated:** 2025-01-XX (Session 5)

## Project Overview
**Name:** Contract Tracker  
**Purpose:** Federal contract resource management system for tracking personnel, LCATs, rates, and profitability  
**Stage:** MVP Development - Financial Dashboard Complete  
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
5. **Historical Analysis:** Track rate changes and resource allocation over time

## System Architecture

### Technology Stack
- **Backend:** .NET 8 with Clean Architecture
- **Frontend:** React 18 with TypeScript
- **Database:** PostgreSQL (Docker)
- **Charts:** Recharts for data visualization
- **State Management:** React hooks
- **API:** RESTful with potential for GraphQL later
- **Authentication:** Planned (currently using "System" placeholder)

### Project Structure
```
contract-tracker-backend/
├── ContractTracker.Api/              # Web API layer
├── ContractTracker.Application/      # Business logic and services
├── ContractTracker.Domain/           # Domain entities and business rules
├── ContractTracker.Infrastructure/   # Data access and external services
└── docs/                             # Documentation
    ├── PROJECT_CONTEXT.md
    ├── SESSION_CONTINUITY_GUIDE.md
    └── NEXT_SESSION_START.md

contract-tracker-frontend/
├── src/
│   ├── components/                   # React components
│   │   ├── Contract/                # Contract management UI
│   │   ├── Dashboard/               # Financial dashboard
│   │   ├── LCAT/                   # LCAT management UI
│   │   └── Resource/                # Resource management UI
│   ├── services/                    # API integration
│   ├── types/                       # TypeScript definitions
│   └── utils/                       # Helper functions
└── .env                             # Environment configuration
```

## Current Working Features

### Financial Dashboard (Session 5) ✅
- Portfolio overview with key metrics cards
- Contract health matrix with visual warnings
- Resource utilization analysis
- 12-month financial projections with charts
- Critical alerts system
- Interactive charts using Recharts
- Tab-based navigation (Overview, Contracts, Resources, Projections)
- Auto-refresh capability

### Contract Management (Session 3-4) ✅
- Create/Read/Update/Delete contracts
- Prime/Subcontractor relationship tracking
- Contract lifecycle (Draft → Active → Closed)
- Funding tracking and modifications
- Visual funding warnings (Critical/High/Medium/Low)
- Resource-to-contract assignments
- Burn rate calculations with actual resources
- Contract types (T&M, Fixed Price, Cost Plus, Labor Hour)

### Resource Management (Session 2, Enhanced Session 4) ✅
- Add resources with type selection (W2/1099/Sub/Fixed)
- LCAT assignment from dropdown
- Automatic wrap rate calculation (2.28x for W2, 1.15x for contractors)
- Pay rate and burdened cost tracking
- Margin calculations with underwater detection
- Resource allocation across contracts
- Termination and reactivation

### LCAT Management (Session 2) ✅
- LCAT CRUD with published and default bill rates
- Position title mapping (one-to-many)
- Rate versioning with effective dates
- Inline batch editing with save-all
- Rate history tracking

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

### Session 4 (2025-01-XX) - Resource Assignment & Burn Rates
**Completed:**
- ✅ Resource-to-Contract assignments with allocation validation
- ✅ Real burn rate calculations from actual resource data
- ✅ Visual dashboard with funding analytics
- ✅ Allocation sliders and resource management UI
- ✅ ContractResource junction entity
- ✅ Prevented over-allocation (>100%)

### Session 5 (2025-01-XX) - Financial Dashboard & Major Refactoring
**Completed:**
- ✅ Built complete Financial Dashboard with 4 views
- ✅ Created DashboardAnalyticsService with comprehensive metrics
- ✅ Implemented portfolio overview, contract health, resource utilization
- ✅ Added 12-month financial projections with charts
- ✅ Integrated Recharts for data visualizations
- ✅ Major entity refactoring (fixed all property mismatches)
- ✅ Rewrote all controllers to match entity structure
- ✅ Fixed 89+ compilation errors
- ✅ Created proper DTO structure
- ✅ Updated all EF Core configurations

## Business Rules Implemented

### Contract Management
- **Contract Identity:** Number may be customer's or ours depending on prime/sub status
- **Funding:** Total value vs funded value with modification tracking
- **Burn Rates:** Monthly, Quarterly, Annual calculations based on assigned resources
- **Warnings:** Multi-level funding warnings based on percentage and time remaining
- **Hours:** Standard FTE (1912 default) with custom override capability
- **Resource Assignment:** Resources can be allocated across multiple contracts

### Resource Management
- **Types:** W2Internal, Subcontractor, Contractor1099, FixedPrice
- **Wrap Rates:** 2.28x for W2, 1.15x for contractors (configurable)
- **Assignment:** Resources can be assigned to LCATs for rate determination
- **Margins:** Automatic calculation with underwater detection
- **Allocation:** Total allocation across contracts cannot exceed 100%

### LCAT Management  
- **Rate Hierarchy:** Published Rate → Default Bill Rate → Contract Override
- **Versioning:** All rates versioned with effective dates
- **Audit:** CreatedBy/ModifiedBy tracking (using "System" until auth added)

## Technical Decisions Made

### Architecture
- **Separate Repositories:** Frontend and backend in different repos
- **Clean Architecture:** Domain entities with private setters
- **DTOs:** Separate DTOs for Create, Update, and Read operations
- **Service Layer:** Complex calculations in Application services
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
- **Recharts:** For data visualizations

### Dashboard Architecture (Session 5)
- **Service Layer:** DashboardAnalyticsService for complex calculations
- **Aggregation:** Single /complete endpoint for dashboard data
- **Projections:** 12-month rolling forecasts
- **Charts:** Recharts library for visualizations
- **Fallback Values:** Default rates when data unavailable

## Known Issues & Technical Debt

### Current Issues (After Session 5)
- Dashboard may not display without seed data
- Resource revenue using hardcoded fallback rate (150)
- Some TypeScript 'any' types in chart components
- Burn rate calculations need actual timesheet data
- Complex calculations temporarily simplified

### Technical Debt
1. No authentication/authorization
2. No pagination on lists
3. No comprehensive error boundaries
4. No tests written
5. No audit logging
6. No conflict resolution for concurrent edits
7. No data export functionality (planned for Session 6)

## API Endpoints Available

### Dashboard Endpoints (Session 5)
- `GET /api/Dashboard/metrics` - Overall portfolio metrics
- `GET /api/Dashboard/contracts/health` - Contract health cards
- `GET /api/Dashboard/resources/utilization` - Resource utilization
- `GET /api/Dashboard/projections?months=12` - Financial projections
- `GET /api/Dashboard/alerts` - Critical alerts
- `GET /api/Dashboard/complete` - All dashboard data in one call

### Contract Endpoints
- `GET /api/Contract` - List all contracts
- `GET /api/Contract/{id}` - Get specific contract
- `POST /api/Contract` - Create contract
- `POST /api/Contract/{id}/activate` - Activate draft contract
- `POST /api/Contract/{id}/close` - Close active contract
- `POST /api/Contract/{id}/update-funding` - Update contract funding
- `POST /api/Contract/assign-resource` - Assign resource to contract
- `GET /api/Contract/{id}/resources` - Get contract resources
- `DELETE /api/Contract/{contractId}/resources/{resourceId}` - Remove resource
- `DELETE /api/Contract/{id}` - Delete contract

### LCAT Endpoints
- `GET /api/LCAT` - List all LCATs
- `GET /api/LCAT/{id}` - Get specific LCAT
- `POST /api/LCAT` - Create LCAT
- `PUT /api/LCAT/{id}` - Update LCAT
- `POST /api/LCAT/{id}/rates` - Add rate to LCAT
- `POST /api/LCAT/{id}/position-titles` - Add position title
- `POST /api/LCAT/{id}/deactivate` - Deactivate LCAT
- `POST /api/LCAT/{id}/reactivate` - Reactivate LCAT
- `DELETE /api/LCAT/{id}` - Delete LCAT

### Resource Endpoints
- `GET /api/Resource` - List all resources
- `GET /api/Resource/{id}` - Get specific resource
- `POST /api/Resource` - Create resource
- `PUT /api/Resource/{id}` - Update resource
- `PUT /api/Resource/batch` - Batch update resources
- `POST /api/Resource/{id}/terminate` - Terminate resource
- `POST /api/Resource/{id}/reactivate` - Reactivate resource
- `DELETE /api/Resource/{id}` - Delete resource

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

## Session 6 Priorities

### Option 1: Debug and Complete Dashboard
1. Ensure data flows properly
2. Fix calculation issues
3. Add error boundaries
4. Seed test data

### Option 2: Export/Reporting Features
1. Excel export functionality
2. PDF report generation
3. CSV data downloads
4. Report templates

### Option 3: Advanced Analytics
1. Trending analysis
2. Predictive forecasting
3. Anomaly detection
4. Natural language insights

### Option 4: Timesheet Integration
1. Actual hours tracking
2. Real burn rate calculations
3. Variance analysis
4. Time entry UI

## AI/ML/RPA Opportunities Identified

### Implemented/Started
1. **Dashboard Analytics:** Complex calculations and projections (Session 5)
2. **Burn Rate Predictions:** Foundation laid with projection system

### Short-term Opportunities
1. **Smart LCAT Matching:** ML to suggest best LCAT based on position title
2. **Rate Prediction:** Predict optimal pay rates based on historical data
3. **Anomaly Detection:** Flag unusual burn rates or margins
4. **Natural Language Queries:** "Show me all underwater resources"

### Long-term Opportunities
1. **Resource Demand Forecasting:** Predict future staffing needs
2. **Automated Compliance Checking:** RPA for clearance verification
3. **Intelligent Resource Allocation:** AI-optimized staff assignments
4. **Predictive Risk Scoring:** Identify at-risk contracts early

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
- **2025-01-XX Session 4:** Resource assignments and real burn calculations
- **2025-01-XX Session 5:** Financial Dashboard and major refactoring
- **Decision:** Defer auth to later phase, use "System" as placeholder user
- **Decision:** Implement burn rate calculations with actual resource data
- **Decision:** Use visual indicators (emojis/colors) for funding warnings
- **Decision:** Use Recharts for dashboard visualizations
- **Decision:** Service layer for complex analytics calculations

---
*This is a living document. Last updated after Session 5 completion.*