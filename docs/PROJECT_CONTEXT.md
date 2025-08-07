# Contract Tracker Project Context

> **File Location:** `/contract-tracker-backend/docs/PROJECT_CONTEXT.md`  
> **Purpose:** Living documentation for AI pair programming and team onboarding  
> **Last Updated:** 2025-08-07  

## Project Overview
**Name:** Contract Tracker  
**Purpose:** [TO BE DEFINED - What is the primary business goal?]  
**Stage:** Initial Development  
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
- **Database:** PostgreSQL (containerized)
- **Architecture Pattern:** Clean Architecture (Domain-Driven Design)
- **API Style:** RESTful with OpenAPI/Swagger documentation
- **Testing:** xUnit, SpecFlow (BDD), FluentAssertions
- **Containerization:** Docker, future cloud deployment planned

### Project Structure
```
contract-tracker/
├── contract-tracker-backend/
│   ├── ContractTracker.Domain/       # Domain entities, value objects
│   ├── ContractTracker.Infrastructure/ # Data access, external services
│   ├── ContractTracker.Api/          # Web API, controllers
│   └── ContractTracker.Tests/        # [TO BE CREATED]
└── contract-tracker-frontend/
    ├── src/
    │   ├── components/               # React components
    │   ├── services/                # API integration
    │   ├── hooks/                   # Custom React hooks
    │   └── types/                   # TypeScript definitions
    └── tests/                       # [TO BE CREATED]
```

### Key Technologies
- **ORM:** Entity Framework Core 9
- **Testing:** xUnit (backend), Jest (frontend) [TO BE IMPLEMENTED]
- **Form Handling:** React Hook Form
- **Tables:** TanStack React Table
- **HTTP Client:** Axios
- **Date Handling:** date-fns

## Domain Model

### Core Entities
1. **Contract**
   - Represents a government/commercial contract
   - Properties: Id, Name, Number, StartDate, EndDate, TotalValue, Type (Federal/State/Commercial), ContractingOfficer, Status
   - Relationships: Has many LCATs with specific bill rates (overrides), Has many Resources

2. **LCAT (Labor Category)**
   - Fixed company-controlled categories for billing
   - Properties: Id, Name, PublishedRate (GSA ceiling), DefaultBillRate
   - All rates are versioned with effective dates
   - Relationships: Has many Position Titles (one-to-many), Has many Rate Versions

3. **LCATRate**
   - Versioned rate history for LCATs
   - Properties: Id, LCATId, RateType (Published/Default), Rate, EffectiveDate, EndDate
   - Tracks both published and default bill rate changes

4. **ContractLCATRate**
   - Contract-specific LCAT rate overrides
   - Properties: ContractId, LCATId, BillRate, EffectiveDate, EndDate
   - Allows PMs to override default rates per contract

5. **PositionTitle**
   - Maps various job titles to LCATs
   - Properties: Id, Title, LCATId
   - Example: "Sr. Developer", "Senior Dev", "Lead Developer" → all map to "Senior Developer" LCAT

6. **Resource**
   - Represents people assigned to contracts
   - Properties: Id, FirstName, LastName, Email, ResourceType, HourlyRate, StartDate
   - ResourceType: W2/1099/Subcontractor/FixedPrice (expandable)
   - Relationships: Assigned to Contract(s), Has LCAT assignment

7. **ResourceTypeDefinition**
   - Defines resource types and their wrap rates
   - Properties: Id, Name, DefaultWrapRate, RequiresFixedPrice
   - Expandable list managed through UI

### Business Rules
1. **Rate Management Hierarchy**
   - Published Rate: GSA ceiling rate (set by super user)
   - Default Bill Rate: Company standard fallback (set by super user)
   - Contract Override Rate: PM can override for specific contracts
   - All rates are versioned with effective dates for historical accuracy

2. **Resource Type Calculations**
   - W2 Internal: Full wrap rate (2.28x default) with benefits
   - Subcontractor: Different wrap rate (negotiated)
   - 1099 Contractor: Similar to sub, no benefits, profit margin only
   - Fixed Price: Total amount divided by period (capped hours e.g., 2080/year)

3. **LCAT Management**
   - LCATs are fixed (company-controlled, hard to change)
   - Multiple position titles map to one LCAT
   - Rate changes create new versions, don't overwrite
   - Batch editing supported with save-all functionality

4. **Financial Tracking**
   - Monthly billing split into 24 payments across the year
   - Work hours: 1912 annually (52 weeks × 36.77 hours)
   - Profitability = Bill Rate - (Pay Rate × Wrap Rate)
   - Margin tracking at resource and contract level

5. **Data Entry & Validation**
   - Inline editing with Excel-like functionality
   - Validation on blur and save-all
   - Visual feedback for unsaved changes
   - Batch operations to avoid one-at-a-time updates

## Features Roadmap

### Phase 1: MVP [CURRENT]
**LCAT Management (Build First)**
- [ ] LCAT CRUD with published and default bill rates
- [ ] Position title mapping (one-to-many)
- [ ] Rate versioning with effective dates
- [ ] Inline batch editing with save-all
- [ ] Rate history viewing

**Resource Management (Build Second)**
- [ ] Add resources with type selection (W2/1099/Sub/Fixed)
- [ ] LCAT assignment from dropdown
- [ ] Filterable table view (read-only initially)
- [ ] Show calculated burdened cost
- [ ] Built for future in-place editing

**Deferred from MVP**
- Financial profitability analysis (needs contract rates first)
- Permissions system (all users are super users initially)
- Conflict handling for simultaneous edits
- Excel import functionality

### Phase 2: Contract & Financial Features
- [ ] Contract management with LCAT rate overrides
- [ ] PM-level rate override capabilities
- [ ] Profitability calculations (Bill Rate vs Burdened Cost)
- [ ] Underwater resource detection and alerts
- [ ] Contract-level financial rollups
- [ ] Permissions system (Super User vs PM vs User)
- [ ] Edit locking for concurrent users

### Phase 3: Advanced Features
- [ ] Resource allocation across multiple contracts
- [ ] Historical timeline views
- [ ] Excel import/export functionality
- [ ] Clearance and certification tracking
- [ ] What-if scenario modeling
- [ ] Automated margin alerts
- [ ] Integration with payroll/timesheet systems

## Testing Strategy

### Testing Pyramid
1. **Unit Tests** (70%)
   - Domain logic tests
   - Service layer tests
   - Component tests

2. **Integration Tests** (20%)
   - API endpoint tests
   - Database integration tests
   - External service tests

3. **E2E Tests** (10%)
   - Critical user journeys
   - Cross-browser testing

### BDD Scenarios
**Feature: Contract Resource Management**
```gherkin
Scenario: Program Manager assigns resource to contract LCAT
  Given I am a Program Manager
  And I have a contract "CMS Development" with LCAT "Senior Developer" at $150/hr
  When I assign "John Smith" as W2 employee to "Senior Developer" at $75/hr pay
  Then the system should calculate total cost as $172.50/hr (with 2.3x wrap)
  And the margin should show as -$22.50/hr (negative margin alert)

Scenario: Track resource movement between contracts
  Given "Jane Doe" is currently on "Contract A" as "Junior Developer"
  When I reassign "Jane Doe" to "Contract B" as "Senior Developer" effective next month
  Then the system should maintain history showing both assignments
  And financial projections should reflect the change starting next month
```

**Feature: Financial Tracking and Reporting**
```gherkin
Scenario: Calculate team burn rate with mixed resource types
  Given I have a contract with:
    | Resource    | Type | LCAT           | Pay Rate | Wrap |
    | John Smith  | W2   | Senior Dev     | $75/hr   | 2.3x |
    | Jane Doe    | 1099 | Senior Dev     | $85/hr   | 1.8x |
    | Bob Wilson  | Sub  | Junior Dev     | $95/hr   | 1.0x |
  When I view the team cost dashboard
  Then I should see total hourly burn rate of $423.50
  And monthly burn rate of $73,540 (assuming 174 hours)

Scenario: Alert on rate changes affecting margin
  Given LCAT "Senior Developer" bills at $150/hr
  And our resource costs $80/hr with 2.3x wrap ($184 total)
  When the contract modification reduces bill rate to $140/hr
  Then the system should alert negative margin of -$44/hr
  And suggest resources that would maintain positive margin
```

## Development Workflow

### Git Strategy
- **Separate Repositories:**
  - `contract-tracker-backend` (.NET API)
  - `contract-tracker-frontend` (React/TypeScript)
- **Main Branch:** `main` (production-ready)
- **Development Branch:** `develop`
- **Feature Branches:** `feature/[ticket-number]-description`
- **Commit Convention:** Conventional Commits (feat:, fix:, docs:, etc.)

### UI/UX Design Principles
- **Excel-like inline editing** for familiar user experience
- **Batch operations** to avoid repetitive single-item updates
- **Visual feedback** for unsaved changes (highlighted cells)
- **Filterable table headers** for easy data discovery
- **Single-page approach** with inline editing (no separate edit pages)
- **Performance:** Handle 250+ LCATs with pagination/virtual scrolling

### MVP Technical Decisions
- **No permissions initially** (all users are super users)
- **No conflict handling** (add locking in Phase 2)
- **PostgreSQL in Docker** for easy local development
- **Separate repos** for clean separation of concerns
- **TypeScript** for type safety in financial calculations
- **Rate versioning** from day one for historical accuracy

## AI/ML Opportunities

### Near-term
1. **LCAT Matching** - NLP to parse resumes and match qualifications to LCAT requirements
2. **Rate Optimization** - ML model to suggest optimal pay rates based on market data and margins
3. **Resource Recommendation** - Suggest best-fit resources for open positions based on historical performance
4. **Anomaly Detection** - Flag unusual billing patterns or cost overruns

### Long-term
1. **Predictive Staffing** - Forecast staffing needs based on contract pipeline and historical patterns
2. **Contract Win Probability** - Analyze RFP requirements and predict win probability based on available resources
3. **Automated Compliance Monitoring** - AI to review contract docs and ensure resource compliance
4. **Smart Negotiations** - AI assistant for rate negotiations based on historical data
5. **Performance Prediction** - Predict resource performance based on similar past assignments
6. **Market Intelligence** - Scrape and analyze competitor rates from public contract awards

## Technical Debt & Improvements
- [ ] Add comprehensive test coverage
- [ ] Implement CI/CD pipeline
- [ ] Add Docker containerization
- [ ] Set up monitoring and logging
- [ ] Implement caching strategy
- [ ] Add API rate limiting
- [ ] Implement background job processing

## Questions to Resolve
1. ~~What are the specific user personas and their needs?~~ ✓ Program Managers managing federal contracts
2. What compliance/regulatory requirements exist? (FAR, DCAA, etc.)
3. What are the performance requirements? (Expected: ~50 users, ~1000 resources, ~20 contracts)
4. What third-party integrations are needed? (Deltek, QuickBooks, ADP, etc.)
5. What are the security/authentication requirements? (CAC cards, MFA, etc.)
6. How should we handle subcontractor resources and their different wrap rates?
7. Do we need to track ODCs (Other Direct Costs) in addition to labor?
8. Should the system support multiple contract vehicles (IDIQ, T&M, FFP)?
9. How detailed should clearance tracking be? (Investigation dates, adjudication, CE, etc.)
10. Integration with SF-328 reporting requirements?

## Development Environment Setup

### Prerequisites
- .NET 8 SDK
- Node.js 18+ and npm
- PostgreSQL 15+
- Visual Studio 2022 or VS Code
- Git

### Local Development Commands
```bash
# Backend
cd contract-tracker-backend
dotnet restore
dotnet ef database update
dotnet run --project ContractTracker.Api

# Frontend
cd contract-tracker-frontend
npm install
npm start
```

### Environment Variables
```env
# Backend (.env in Api project)
ConnectionStrings__DefaultConnection=Host=localhost;Database=ContractTracker;Username=;Password=
JWT_SECRET=[TO BE SET]
CORS_ORIGINS=http://localhost:3000

# Frontend (.env)
REACT_APP_API_URL=http://localhost:5000
```

## Key Terminology
- **LCAT (Labor Category):** Job classification defined in government contracts with specific education/experience requirements
- **Wrap Rate:** Multiplier applied to base salary to account for overhead, benefits, G&A, and profit
- **Bill Rate:** What the government pays per hour for an LCAT
- **Pay Rate:** What the company pays the employee (before wrap)
- **Burn Rate:** Total cost per time period for all resources
- **T&M:** Time & Materials contract type
- **FFP:** Firm Fixed Price contract type
- **IDIQ:** Indefinite Delivery/Indefinite Quantity contract vehicle
- **PoP:** Period of Performance
- **CLIN:** Contract Line Item Number
- **FAR:** Federal Acquisition Regulation
- **DCAA:** Defense Contract Audit Agency
- **ODC:** Other Direct Costs (travel, equipment, etc.)

## Notes & Decisions Log
- **2025-08-07:** Initial project structure created with Clean Architecture
- **2025-08-07:** Chose PostgreSQL for better JSON support and scalability
- **2025-08-07:** Defined core business context - federal contract program management focus
- **2025-08-07:** Established key entities including wrap rate tracking and resource types
- [Add new decisions here as we make them]

---
*This is a living document. Update it as the project evolves.*

## Setup Instructions

### Creating the docs folder:
```bash
cd contract-tracker-backend
mkdir docs
# Save this file as: docs/PROJECT_CONTEXT.md
git add docs/PROJECT_CONTEXT.md
git commit -m "docs: add project context for AI pair programming"
git push
```

### Referencing in new sessions:
Tell the AI: "Check PROJECT_CONTEXT.md in the backend docs folder for project context"