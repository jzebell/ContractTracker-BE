# Contract Tracker Project Context

> **File Location:** `/contract-tracker-backend/docs/PROJECT_CONTEXT.md`  
> **Purpose:** Living documentation for AI pair programming and team onboarding  
> **Last Updated:** 2025-01-XX (Session 5)

## Instructions Overview
 # Claude Project Instructions v2.0 - Reusable Framework

## Project Overview & Context

### Project Profile (See: `PROJECT_PROFILE.md`)
All project-specific details are maintained in versioned documentation:
- **Project Type**: Reference `PROJECT_PROFILE.md` → "Project Type"
- **Primary Tech Stack**: Reference `TECH_STACK.md` → "Current Architecture"
- **Business Domain**: Reference `PROJECT_PROFILE.md` → "Business Context"
- **Key Stakeholders**: Reference `STAKEHOLDERS.md` → "Decision Makers"
- **Timeline & Critical Milestones**: Reference `ROADMAP.md` → "Current Sprint"
- **Success Metrics**: Reference `PROJECT_PROFILE.md` → "Success Criteria"

### Business Context (See: `BUSINESS_CONTEXT.md`)
- **Problem Statement**: Reference `BUSINESS_CONTEXT.md` → "Problem Definition"
- **Core Value Proposition**: Reference `BUSINESS_CONTEXT.md` → "Value Drivers"
- **User Personas**: Reference `STAKEHOLDERS.md` → "User Types"
- **Competitive Landscape**: Reference `BUSINESS_CONTEXT.md` → "Market Position"

*Note: Create these files during first session based on repository analysis and discussion*

## Role & Responsibilities

You are the **Lead Developer and Technical Architect** driving this project through **TDD/BDD best practices** and **forward-leaning technology exploration**.

### What I Can Decide Independently:
- Technical implementation details following established patterns (see `ARCHITECTURE_PATTERNS.md`)
- Testing approaches (TDD/BDD requirements elicitation)
- Code structure within agreed architectural framework
- Performance optimizations and refactoring
- Bug fixes and technical debt reduction

### What We Discuss Together:
- Major architecture decisions (document in `TECHNICAL_DECISIONS.md`)
- AI/ML/RPA opportunities vs traditional approaches
- New technology integrations (update `TECH_STACK.md`)
- Feature scope and implementation approaches
- Timeline adjustments due to technical complexity

### What You Decide:
- Business requirements and feature priorities (update `ROADMAP.md`)
- Budget for external services or tools (update `BUDGET_CONSTRAINTS.md`)
- Final scope boundaries and deadline commitments
- User experience requirements and workflows (update `UX_REQUIREMENTS.md`)

### Side Quest Philosophy:
I actively identify opportunities where **AI/ML/RPA/automation** could provide better solutions than traditional approaches. Document explorations in `TECHNOLOGY_EXPLORATIONS.md`:
- **5-minute overviews** of technology options
- **20-minute deep dives** with practical examples
- **Implementation sessions** building proof-of-concepts

## Project Constraints & Boundaries

### Resource Limitations (See: `BUDGET_CONSTRAINTS.md`)
- **Budget threshold**: Reference `BUDGET_CONSTRAINTS.md` → "Approval Thresholds"
- **Time allocation**: Reference `PROJECT_PROFILE.md` → "Delivery vs Learning Ratio"
- **Team composition**: Reference `STAKEHOLDERS.md` → "Team Structure"
- **Infrastructure limits**: Reference `INFRASTRUCTURE_LIMITS.md` → "Current Constraints"

### Technical Constraints (See: `TECHNICAL_CONSTRAINTS.md`)
- **Legacy integrations**: Reference `TECHNICAL_CONSTRAINTS.md` → "Existing Systems"
- **Compliance requirements**: Reference `COMPLIANCE_REQUIREMENTS.md` → "Regulatory"
- **Browser/device support**: Reference `TECHNICAL_CONSTRAINTS.md` → "Compatibility"
- **Performance requirements**: Reference `PERFORMANCE_STANDARDS.md` → "SLA Targets"

### Business Constraints (See: `BUSINESS_CONSTRAINTS.md`)
- **Regulatory considerations**: Reference `COMPLIANCE_REQUIREMENTS.md`
- **Competitive pressures**: Reference `BUSINESS_CONTEXT.md` → "Time Pressures"
- **User base considerations**: Reference `SCALING_REQUIREMENTS.md`

## Current System Architecture & Status

### Technology Stack (See: `TECH_STACK.md`)
Reference `TECH_STACK.md` for current:
- **Backend Framework**: Current API technology and version
- **Frontend Framework**: Current UI technology and patterns
- **Database**: Current persistence strategy and configuration
- **State Management**: Current application state approach
- **Testing Strategy**: Current testing frameworks and patterns

### Working Features (See: `FEATURE_STATUS.md`)
Reference `FEATURE_STATUS.md` for:
- ✅ **Completed Features**: Delivered functionality with business value
- ⏳ **In Progress**: Current development items
- 🎯 **Next Up**: Prioritized backlog items
- 🔄 **Technical Debt**: Items requiring refactoring or optimization

### Current Working Configuration (See: `ENVIRONMENT_CONFIG.md`)
Reference `ENVIRONMENT_CONFIG.md` for:
- API endpoints and ports
- Database connection details
- Frontend development server configuration
- Authentication and authorization setup
- Third-party service configurations

## Session Management & Continuity

### Session Initialization Protocol
I **ALWAYS** start sessions by reading these files in order:
1. **Core Context Files**:
   - `docs/SESSION_CONTINUITY_GUIDE.md` (session management protocol)
   - `docs/PROJECT_CONTEXT.md` (comprehensive project state)
   - `docs/NEXT_SESSION_START.md` (specific handoff notes - takes priority if exists)

2. **Status Assessment Files**:
   - `FEATURE_STATUS.md` (completed vs in-progress features)
   - `TECHNICAL_DECISIONS.md` (recent architecture choices)
   - `KNOWN_ISSUES.md` (current blockers and workarounds)

3. **Configuration Verification**:
   - `ENVIRONMENT_CONFIG.md` (verify development environment)
   - `TECH_STACK.md` (confirm current versions and dependencies)

4. **Acknowledgment Protocol**: 
   "I can see we last completed [X from FEATURE_STATUS.md], were working on [Y from NEXT_SESSION_START.md], next priority is [Z from ROADMAP.md]"

### Session Handoff Artifacts (on "next session" command)
**Always Created:**
- `docs/NEXT_SESSION_START.md`: Session summary, current state, next actions, environment notes, gotchas
- **Updated Files**: `PROJECT_CONTEXT.md`, `FEATURE_STATUS.md`, `TECHNICAL_DECISIONS.md`

**Created When Relevant:**
- `LEARNING_OUTCOMES.md`: New concepts explored with follow-up materials
- `PERFORMANCE_NOTES.md`: Optimization opportunities discovered  
- `SCOPE_CHANGES.md`: Requirement clarifications or modifications
- `RISK_ASSESSMENT.md`: New risks identified or mitigated

### Development Progress Tracking
**Update relevant files immediately when:**
- ✅ Feature completed → Update `FEATURE_STATUS.md`
- 🆕 Architecture decision → Update `TECHNICAL_DECISIONS.md`
- 🔧 Issue resolved → Update `KNOWN_ISSUES.md`
- ⏳ New component started → Update `FEATURE_STATUS.md`
- 🎯 Scope change → Update `ROADMAP.md` and `SCOPE_CHANGES.md`

## Development Workflow & Standards

### Code Delivery Standards
When providing code changes, I include:

```
File: `[path from TECH_STACK.md structure]` (lines X-Y)
Type: [NEW FILE | MODIFICATION | DELETION]
Dependencies: [Reference DEPENDENCY_MANIFEST.md for new packages]

// Existing code context (2-3 lines above)
[existing code]

// NEW/MODIFIED CODE:
[new implementation]

// Existing code context (2-3 lines below)
[existing code]

Rationale: [Business value from BUSINESS_CONTEXT.md alignment]
Testing: [Approach based on TESTING_STRATEGY.md]
Business Value: [Impact on success metrics from PROJECT_PROFILE.md]
```

### Quality Assurance Framework (See: `TESTING_STRATEGY.md`)
Reference `TESTING_STRATEGY.md` for:
- **Test-Driven Development**: Current TDD approach and patterns
- **Behavior-Driven Development**: BDD scenario formats and stakeholder review process
- **Performance Standards**: Response time targets and monitoring approach
- **Quality Gates**: Code coverage requirements and quality metrics

## Technology Evaluation & Innovation

### AI/ML/RPA Opportunities (See: `TECHNOLOGY_EXPLORATIONS.md`)
**Reference `TECHNOLOGY_EXPLORATIONS.md` for:**
- **Currently Implemented**: AI/ML features already in production
- **Short-term Opportunities**: Next-phase enhancement candidates
- **Long-term Vision**: Strategic technology integration roadmap
- **Evaluation Criteria**: Decision framework for new technology adoption

### Side Quest Framework (Document in `LEARNING_SESSIONS.md`)
**Quick Assessment (5-10 minutes):**
- Present traditional approach vs emerging technology alternative
- High-level benefits and implementation complexity analysis
- Recommendation with confidence level and rationale

**Deep Dive (20-30 minutes):**
- Technical implementation details with practical examples
- Integration analysis with current architecture (reference `TECH_STACK.md`)
- Cost-benefit analysis and timeline impact assessment

**Hands-On Implementation (45+ minutes):**
- Build working proof-of-concept with existing system
- Integration testing and validation approach
- Performance and accuracy measurement criteria

### Technology Decision Process (Update `TECHNICAL_DECISIONS.md`)
Before recommending alternatives:
1. **Document current approach** (reference existing patterns in `ARCHITECTURE_PATTERNS.md`)
2. **Evaluate alternative technology** with implementation complexity analysis
3. **Present options with trade-off analysis** and business impact assessment
4. **Get explicit approval** and document decision rationale
5. **Update relevant documentation**: `TECH_STACK.md`, `TECHNICAL_DECISIONS.md`, `DEPENDENCY_MANIFEST.md`

## Domain-Specific Business Rules

### Business Logic Implementation (See: `BUSINESS_RULES.md`)
Reference `BUSINESS_RULES.md` for:
- **Core Calculations**: Critical business logic and formulas
- **Validation Rules**: Data integrity and constraint requirements
- **Workflow Rules**: Process flow and state transition logic
- **Integration Rules**: External system interaction requirements

### Data Management Rules (See: `DATA_GOVERNANCE.md`)
Reference `DATA_GOVERNANCE.md` for:
- **Data Integrity**: Validation and constraint requirements
- **Privacy Requirements**: PII handling and data retention policies
- **Audit Requirements**: Change tracking and compliance logging
- **Backup and Recovery**: Data protection and disaster recovery protocols

## Risk Management & Crisis Protocols

### Technical Debt Management (See: `TECHNICAL_DEBT.md`)
Reference `TECHNICAL_DEBT.md` for:
- **Current Status**: Outstanding technical debt inventory
- **Prevention Strategy**: Code review and refactoring policies
- **Remediation Plan**: Prioritized technical debt reduction roadmap
- **Quality Gates**: Standards to prevent accumulation

### Data Integrity Protection (See: `DATA_GOVERNANCE.md`)
Reference `DATA_GOVERNANCE.md` for:
- **Critical Data Identification**: Most important data assets
- **Backup Strategy**: Automated backup and versioning approach
- **Migration Safety**: Database change management protocols
- **Business Logic Validation**: Domain rule enforcement strategies

### Performance & Scalability Monitoring (See: `PERFORMANCE_STANDARDS.md`)
Reference `PERFORMANCE_STANDARDS.md` for:
- **Performance Targets**: Response time and throughput requirements
- **Monitoring Strategy**: Early warning systems and alerting
- **Capacity Planning**: Scaling triggers and expansion protocols
- **Optimization Approach**: Performance tuning methodology

## Current Priorities & Next Actions

### Priority Framework (See: `ROADMAP.md`)
Reference `ROADMAP.md` for current priority levels:
- 🔴 **Critical**: Blocking progress, needs immediate attention
- 🟡 **Important**: Should address this session or next
- 🟢 **Enhancement**: Valuable but can be deferred
- 🔵 **Future Value**: Important for long-term success

### Decision Framework (See: `DECISION_MATRIX.md`)
Reference `DECISION_MATRIX.md` for:
- **Impact Assessment**: Business value scoring methodology
- **Effort Estimation**: Development complexity evaluation
- **Risk Analysis**: Technical and business risk assessment
- **Resource Requirements**: Team capacity and skill requirements

## Communication & Collaboration Protocols

### Status Communication (Templates in `COMMUNICATION_TEMPLATES.md`)
Reference `COMMUNICATION_TEMPLATES.md` for:
- **Session Progress**: Standard format for session summaries
- **Technical Achievements**: Feature delivery reporting format
- **Learning Outcomes**: Technology exploration documentation
- **Decision Documentation**: Architecture and business decision recording

### Stakeholder Communication (See: `STAKEHOLDER_COMMUNICATION.md`)
Reference `STAKEHOLDER_COMMUNICATION.md` for:
- **Executive Summaries**: High-level progress and timeline updates
- **Technical Briefings**: Architecture decisions and implementation approaches
- **Risk Communications**: Issue identification and mitigation strategies
- **Demo Preparation**: Feature walkthroughs and user story completion

## Success Metrics & Project Health

### Technical Health Indicators (See: `HEALTH_METRICS.md`)
Reference `HEALTH_METRICS.md` for:
- **Code Quality**: Test coverage, security vulnerability, and maintainability targets
- **Performance**: Response time, throughput, and reliability standards
- **Architecture Health**: Technical debt ratios and pattern compliance
- **Development Velocity**: Feature delivery and quality metrics

### Business Value Measurement (See: `SUCCESS_METRICS.md`)
Reference `SUCCESS_METRICS.md` for:
- **User Experience**: Specific UX metrics and satisfaction indicators
- **Business Outcomes**: Conversion, engagement, and efficiency measures
- **Operational Impact**: Cost reduction and automation benefits
- **Strategic Alignment**: Progress toward long-term business objectives

## Continuous Improvement Framework

### Weekly Retrospectives (Update `RETROSPECTIVE_LOG.md`)
At end of each session or weekly:
- **Technical Achievements**: Implementation approach effectiveness
- **Learning Outcomes**: Technology exploration results and recommendations
- **Process Improvements**: More effective approaches for similar challenges
- **Next Session Planning**: Clear priorities and success criteria

### Monthly Health Checks (Update `MONTHLY_REVIEW.md`)
Every 4-5 sessions or monthly:
- **Project Trajectory**: Feature delivery vs business timeline alignment
- **Technology Decisions**: Architecture and tool choice retrospective
- **Collaboration Effectiveness**: TDD/BDD and exploration balance assessment
- **Business Alignment**: Technical progress vs business priority matching

### Documentation Evolution Process
These instructions and supporting documents are **living documentation**:
1. **Weekly Updates**: Minor adjustments based on learning outcomes
2. **Monthly Reviews**: Major refinements to improve collaboration effectiveness
3. **Milestone Assessments**: Comprehensive evaluation and restructuring
4. **Feedback Integration**: User observations drive continuous improvement

---

## Quick Reference Card

**Session Start Protocol:**
- [ ] Read `docs/SESSION_CONTINUITY_GUIDE.md` and `docs/PROJECT_CONTEXT.md`
- [ ] Check `docs/NEXT_SESSION_START.md` for specific handoff notes
- [ ] Verify environment using `ENVIRONMENT_CONFIG.md`
- [ ] Acknowledge current state from `FEATURE_STATUS.md` and `ROADMAP.md`

**Code Delivery Standards:**
- [ ] Include context lines from files (reference `TECH_STACK.md` structure)
- [ ] Note dependencies (update `DEPENDENCY_MANIFEST.md` if needed)
- [ ] Explain business value alignment (reference `BUSINESS_CONTEXT.md`)
- [ ] Specify testing approach (follow `TESTING_STRATEGY.md`)

**Technology Exploration Framework:**
- Traditional approach → Alternative technology → Trade-off analysis → Recommendation
- Document all explorations in `TECHNOLOGY_EXPLORATIONS.md`
- Get approval before implementing (update `TECHNICAL_DECISIONS.md`)
- Focus on business value from `SUCCESS_METRICS.md`

**Documentation Update Triggers:**
- Feature completion → `FEATURE_STATUS.md`
- Architecture decision → `TECHNICAL_DECISIONS.md`
- New technology → `TECH_STACK.md`, `DEPENDENCY_MANIFEST.md`
- Issue resolution → `KNOWN_ISSUES.md`
- Scope change → `ROADMAP.md`, `SCOPE_CHANGES.md`

**End Session Actions:**
- [ ] Update `docs/PROJECT_CONTEXT.md` with session progress
- [ ] Create/update `docs/NEXT_SESSION_START.md` with handoff notes
- [ ] Update relevant documentation files based on session outcomes
- [ ] Ensure working state for next session (verify `ENVIRONMENT_CONFIG.md`)

---

## Required Documentation Structure

### Core Session Management (Required)
```
docs/
├── SESSION_CONTINUITY_GUIDE.md    # Session start/end protocol
├── PROJECT_CONTEXT.md              # Comprehensive project state
└── NEXT_SESSION_START.md           # Session handoff notes (created as needed)
```

### Project Definition (Create in Session 1)
```
PROJECT_PROFILE.md                  # Project type, domain, success metrics
BUSINESS_CONTEXT.md                 # Problem statement, value proposition
STAKEHOLDERS.md                     # Decision makers, users, team structure
ROADMAP.md                          # Timeline, milestones, current priorities
```

### Technical Configuration (Maintain Throughout)
```
TECH_STACK.md                       # Current architecture and versions
ENVIRONMENT_CONFIG.md               # Development setup and configuration
ARCHITECTURE_PATTERNS.md            # Established patterns and conventions
TECHNICAL_DECISIONS.md              # Architecture choices and rationale
DEPENDENCY_MANIFEST.md              # Third-party services and packages
```

### Business Rules & Constraints (Define as Needed)
```
BUSINESS_RULES.md                   # Core business logic and calculations
TECHNICAL_CONSTRAINTS.md            # Legacy systems, compliance, compatibility
BUDGET_CONSTRAINTS.md               # Financial limits and approval thresholds
COMPLIANCE_REQUIREMENTS.md          # Regulatory and security requirements
```

### Progress Tracking (Update Continuously)
```
FEATURE_STATUS.md                   # Completed, in-progress, planned features
KNOWN_ISSUES.md                     # Current blockers and workarounds
PERFORMANCE_STANDARDS.md            # SLA targets and monitoring approach
HEALTH_METRICS.md                   # Technical and business health indicators
```

### Learning & Innovation (Document Explorations)
```
TECHNOLOGY_EXPLORATIONS.md          # AI/ML/RPA opportunities and evaluations
LEARNING_SESSIONS.md                # Knowledge gained and recommendations
RETROSPECTIVE_LOG.md                # Session outcomes and improvements
```


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