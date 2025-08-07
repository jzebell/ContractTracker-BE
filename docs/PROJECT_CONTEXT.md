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
[TO BE DEFINED - What problem are we solving?]

### Key Stakeholders
- **Primary Users:** [TO BE DEFINED]
- **Secondary Users:** [TO BE DEFINED]
- **Admin Users:** System administrators

### Core Business Value
[TO BE DEFINED - How does this create value?]

## Technical Architecture

### Stack Overview
- **Backend:** .NET 8, C# 
- **Frontend:** React 19, TypeScript, TailwindCSS
- **Database:** PostgreSQL
- **Architecture Pattern:** Clean Architecture (Domain-Driven Design)
- **API Style:** RESTful with OpenAPI/Swagger documentation

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
   - Properties: Id, Name, Number, StartDate, EndDate, Value, Status
   - Relationships: Has many LCATs, Has many Resources

2. **LCAT (Labor Category)**
   - Represents billable labor categories
   - Properties: Id, Name, Description, Code
   - Relationships: Has many Rates, Has many Position Titles

3. **Resource**
   - Represents people assigned to contracts
   - Properties: Id, Name, Email, LCATId, ContractId
   - Relationships: Belongs to Contract, Has one LCAT

4. **LCATRate**
   - Represents billing rates over time
   - Properties: Id, LCATId, Rate, EffectiveDate, EndDate

5. **ContractLCATRate**
   - Contract-specific LCAT rates
   - Properties: ContractId, LCATId, Rate, Year

### Business Rules
[TO BE DEFINED based on requirements]
- Contract date validations
- Rate change restrictions
- Resource assignment rules

## Features Roadmap

### Phase 1: MVP [CURRENT]
- [ ] Basic CRUD for Contracts
- [ ] Basic CRUD for LCATs
- [ ] Basic CRUD for Resources
- [ ] Simple reporting dashboard

### Phase 2: Enhanced Features
- [ ] Rate history tracking
- [ ] Resource allocation visualization
- [ ] Contract document attachments
- [ ] Audit logging

### Phase 3: Advanced Features
- [ ] AI-powered contract analysis
- [ ] Predictive analytics for renewals
- [ ] Automated compliance checking
- [ ] RPA integration for approvals

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
[TO BE DEFINED - Key scenarios to implement]

## Development Workflow

### Git Strategy
- **Main Branch:** `main` (production-ready)
- **Development Branch:** `develop`
- **Feature Branches:** `feature/[ticket-number]-description`
- **Commit Convention:** Conventional Commits (feat:, fix:, docs:, etc.)

### Definition of Done
- [ ] Code written and working locally
- [ ] Unit tests written and passing
- [ ] Integration tests updated if needed
- [ ] Code reviewed by peer
- [ ] Documentation updated
- [ ] Deployed to staging environment

## AI/ML Opportunities

### Near-term
1. **Contract Term Extraction** - NLP to identify key dates, parties, obligations
2. **Similar Contract Search** - Find similar past contracts for reference
3. **Rate Anomaly Detection** - Flag unusual billing rates

### Long-term
1. **Predictive Renewal Analysis** - Forecast likelihood of contract renewal
2. **Budget Impact Prediction** - Predict future costs based on trends
3. **Automated Contract Generation** - Generate standard contracts from templates
4. **Compliance Risk Scoring** - AI-driven risk assessment

## Technical Debt & Improvements
- [ ] Add comprehensive test coverage
- [ ] Implement CI/CD pipeline
- [ ] Add Docker containerization
- [ ] Set up monitoring and logging
- [ ] Implement caching strategy
- [ ] Add API rate limiting
- [ ] Implement background job processing

## Questions to Resolve
1. What are the specific user personas and their needs?
2. What compliance/regulatory requirements exist?
3. What are the performance requirements (users, data volume)?
4. What third-party integrations are needed?
5. What are the security/authentication requirements?

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

## Notes & Decisions Log
- **2025-08-07:** Initial project structure created with Clean Architecture
- **2025-08-07:** Chose PostgreSQL for better JSON support and scalability
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