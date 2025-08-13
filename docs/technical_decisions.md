# Technical Decisions - Contract Tracker

> **File Purpose:** Architecture choices, technology selections, and rationale  
> **Last Updated:** January 2025 (Session 6)  
> **Update Trigger:** Any significant technical or architectural decision

## Architecture Decisions

### ADR-001: Clean Architecture Pattern
**Decision Date:** January 2025 (Session 1)  
**Status:** ✅ Implemented  
**Decision:** Adopt Clean Architecture with Domain-Driven Design

**Context:** Need for maintainable, testable codebase that can evolve with business requirements

**Rationale:**
- **Separation of Concerns:** Business logic isolated from infrastructure dependencies
- **Testability:** Domain layer can be tested without database or web dependencies  
- **Flexibility:** Can change UI, database, or external services without affecting business logic
- **Team Understanding:** Well-documented pattern with clear layer responsibilities

**Implementation:**
```
Domain Layer:        Entities, business rules, no dependencies
Application Layer:   Use cases, business orchestration, depends only on Domain
Infrastructure:      Data access, external services, implements Application interfaces  
API Layer:          Controllers, DTOs, depends on Application
```

**Consequences:**
- ✅ High maintainability and testability
- ✅ Clear separation of business vs technical concerns
- ⚠️ Initial complexity higher than simple layered architecture
- ⚠️ More files and projects to manage

### ADR-002: Entity Framework Core with PostgreSQL
**Decision Date:** January 2025 (Session 1)  
**Status:** ✅ Implemented  
**Decision:** Use EF Core 8 with PostgreSQL 15 for data persistence

**Context:** Need for robust, scalable database solution with strong .NET integration

**Rationale:**
- **Performance:** PostgreSQL superior performance for complex queries and analytics
- **Cost:** Open source with no licensing costs vs SQL Server
- **JSON Support:** Native JSON capabilities for flexible data structures
- **Ecosystem:** Excellent tooling and .NET integration through Npgsql
- **Scalability:** Proven enterprise scalability and reliability

**Implementation:**
- Code-first migrations for schema management
- Repository pattern for data access abstraction
- Connection pooling and optimized queries
- Docker containerization for development consistency

**Consequences:**
- ✅ Excellent performance and scalability potential
- ✅ No database licensing costs
- ✅ Strong JSON and analytical capabilities
- ⚠️ Team PostgreSQL learning curve vs SQL Server familiarity

### ADR-003: React with TypeScript (No UI Framework)
**Decision Date:** January 2025 (Session 2)  
**Status:** ✅ Implemented  
**Decision:** Build custom UI components rather than adopt UI framework

**Context:** Need for responsive, maintainable frontend that matches specific business workflows

**Rationale:**
- **Control:** Complete control over styling and behavior
- **Performance:** No framework bloat, only necessary code shipped
- **Customization:** Excel-like editing patterns not available in standard UI libraries
- **Learning:** TypeScript provides type safety without framework complexity

**Implementation:**
- Custom CSS with Flexbox and Grid for layouts
- TypeScript for type safety and development experience
- Recharts for data visualization (focused library choice)
- Component-based architecture with clear separation of concerns

**Consequences:**
- ✅ Complete UI control and customization capability
- ✅ Lightweight bundle size and fast loading
- ✅ No framework upgrade dependencies or breaking changes
- ⚠️ More development time for custom components
- ⚠️ Accessibility features require manual implementation

### ADR-004: RESTful API Design
**Decision Date:** January 2025 (Session 2)  
**Status:** ✅ Implemented  
**Decision:** Use REST over GraphQL for API design

**Context:** Need for simple, maintainable API that supports current use cases

**Rationale:**
- **Simplicity:** REST patterns well understood by team and consumers
- **Tooling:** Excellent tooling support with Swagger/OpenAPI
- **Caching:** HTTP caching strategies mature and effective
- **Debugging:** Simple to debug and monitor with standard HTTP tools

**Implementation:**
- Standard HTTP verbs (GET, POST, PUT, DELETE)
- Resource-based URLs (/api/Contract, /api/Resource)
- Consistent error handling and status codes
- OpenAPI documentation generation

**Future Consideration:** GraphQL evaluation if client data fetching becomes complex

**Consequences:**
- ✅ Simple to implement and maintain
- ✅ Excellent tooling and debugging support
- ✅ Wide ecosystem compatibility
- ⚠️ Potential over-fetching compared to GraphQL
- ⚠️ Multiple requests needed for complex data relationships

## Data Architecture Decisions

### DTD-001: Separate DTOs for Input/Output
**Decision Date:** January 2025 (Session 5)  
**Status:** ✅ Implemented  
**Decision:** Create separate DTO classes for Create, Update, and Read operations

**Context:** Need for clear API contracts and validation while protecting internal domain model

**Rationale:**
- **API Clarity:** Clear contracts for what data clients can send vs receive
- **Validation:** Different validation rules for create vs update operations
- **Security:** Prevents over-posting and exposes only necessary data
- **Evolution:** Can evolve API contracts without breaking domain model

**Implementation:**
```csharp
ContractCreateDTO    // Client input for new contracts
ContractUpdateDTO    # Client input for modifications  
ContractDTO          # Server output to clients
CompleteDashboardDTO # Complex aggregated data for dashboard
```

**Consequences:**
- ✅ Clear API boundaries and contracts
- ✅ Strong validation and security
- ⚠️ Additional mapping code between DTOs and entities
- ⚠️ More classes to maintain

### DTD-002: Entity-Based Business Logic
**Decision Date:** January 2025 (Session 1)  
**Status:** ✅ Implemented  
**Decision:** Place business logic in domain entities rather than services

**Context:** Need for rich domain model that encapsulates business rules

**Rationale:**
- **Encapsulation:** Business rules stay close to the data they operate on
- **Consistency:** Impossible to modify entity without business rule validation
- **Discoverability:** Business logic easily found in entity classes
- **Domain-Driven Design:** Aligns with DDD principles and rich domain models

**Implementation:**
```csharp
public class Contract
{
    private Contract() { } // Prevent invalid creation
    
    public static Contract Create(string number, string title, ContractType type)
    {
        // Business validation here
        return new Contract { ... };
    }
    
    public void UpdateFunding(decimal fundedAmount, string justification)
    {
        // Business logic for funding updates
    }
}
```

**Consequences:**
- ✅ Business rules enforced at entity level
- ✅ Clear location for business logic
- ✅ Impossible to create invalid entities
- ⚠️ Entities more complex than simple data containers

### DTD-003: Calculated Properties in Domain
**Decision Date:** January 2025 (Session 4)  
**Status:** ✅ Implemented  
**Decision:** Calculate financial metrics in domain layer rather than database

**Context:** Complex business calculations for margins, burn rates, and allocations

**Rationale:**
- **Business Logic Centralization:** All calculations in domain layer
- **Testability:** Business calculations can be unit tested
- **Flexibility:** Easy to modify calculation logic without database changes
- **Performance:** Calculations can be cached and optimized

**Implementation:**
- BurnRateAnalysis value object for complex calculations
- Cached properties with invalidation on data changes
- Business logic methods for margin and allocation calculations

**Consequences:**
- ✅ Centralized, testable business logic
- ✅ Flexible calculation modification
- ⚠️ Potential performance impact for large datasets
- ⚠️ More complex domain model

## Technology Selection Decisions

### TSD-001: Recharts for Data Visualization
**Decision Date:** January 2025 (Session 5)  
**Status:** ✅ Implemented  
**Decision:** Use Recharts library for dashboard charts and graphs

**Context:** Need for interactive, responsive charts in financial dashboard

**Rationale:**
- **React Integration:** Built specifically for React applications
- **TypeScript Support:** Strong TypeScript definitions and type safety
- **Performance:** Good performance with reasonable dataset sizes
- **Customization:** Flexible styling and interaction capabilities
- **Size:** Reasonable bundle impact compared to alternatives

**Implementation:**
- Line charts for burn rate trends
- Bar charts for contract comparisons
- Pie charts for resource allocation
- Area charts for financial projections

**Alternatives Considered:**
- Chart.js: More features but React integration complexity
- D3.js: Maximum flexibility but steep learning curve
- Victory: Similar to Recharts but larger bundle size

**Consequences:**
- ✅ Excellent React integration and TypeScript support
- ✅ Good performance for current data volumes
- ⚠️ Limited advanced visualization options
- ⚠️ Bundle size increase (acceptable trade-off)

### TSD-002: Docker for Development Database
**Decision Date:** January 2025 (Session 1)  
**Status:** ✅ Implemented  
**Decision:** Use Docker container for PostgreSQL in development

**Context:** Need for consistent development environment across team members

**Rationale:**
- **Consistency:** Identical database environment for all developers
- **Isolation:** Database changes don't affect host system
- **Version Control:** docker-compose.yml provides infrastructure as code
- **Speed:** Faster setup than local PostgreSQL installation

**Implementation:**
- PostgreSQL 15 Alpine image for smaller size
- Persistent volume for data retention
- Port mapping for local development access
- Environment variables for configuration

**Consequences:**
- ✅ Consistent development environment
- ✅ Easy setup and teardown
- ✅ No local PostgreSQL installation required
- ⚠️ Docker dependency for development
- ⚠️ Additional complexity for developers unfamiliar with Docker

## Development Workflow Decisions

### DWD-001: Session-Based Development with AI
**Decision Date:** January 2025 (Session 1)  
**Status:** ✅ Implemented  
**Decision:** Structure development in focused sessions with comprehensive documentation

**Context:** AI pair programming requires different workflow than traditional team development

**Rationale:**
- **Context Preservation:** Detailed documentation maintains context between sessions
- **Knowledge Transfer:** Clear handoff notes enable effective session transitions
- **Progress Tracking:** Session-based progress measurement and planning
- **Quality Assurance:** Regular review and reflection on technical decisions

**Implementation:**
- Comprehensive project documentation framework
- Session handoff notes (NEXT_SESSION_START.md)
- Real-time documentation updates during development
- Regular retrospectives and decision documentation

**Consequences:**
- ✅ Excellent context preservation and knowledge transfer
- ✅ Clear progress tracking and accountability
- ✅ High-quality documentation maintained throughout project
- ⚠️ Documentation overhead during development
- ⚠️ Requires discipline to maintain documentation quality

### DWD-002: Test-Last Development (MVP Phase)
**Decision Date:** January 2025 (Session 1)  
**Status:** ⚠️ Temporary  
**Decision:** Focus on feature delivery over test coverage during MVP phase

**Context:** MVP timeline requires rapid feature delivery to validate business value

**Rationale:**
- **Speed:** Faster initial feature delivery to validate concepts
- **Learning:** Business requirements likely to change during MVP phase
- **Focus:** Limited development time better spent on core features
- **Future Investment:** Comprehensive testing planned for production phase

**Implementation:**
- Manual testing and validation during development
- Business logic concentrated in domain entities for easier testing later
- Clean architecture enables testability when tests are added
- Test infrastructure planning for post-MVP implementation

**Future Plan:** Implement comprehensive testing strategy before production deployment

**Consequences:**
- ✅ Faster MVP feature delivery
- ✅ More time for business value validation
- ⚠️ Higher risk of bugs in production
- ⚠️ Refactoring risk when adding tests later

## Performance Decisions

### PD-001: Client-Side Financial Calculations
**Decision Date:** January 2025 (Session 5)  
**Status:** ✅ Implemented  
**Decision:** Perform dashboard calculations in C# service layer, not database

**Context:** Complex financial calculations for dashboard analytics

**Rationale:**
- **Business Logic Location:** Calculations belong in domain/application layer
- **Testability:** C# calculations easier to unit test than SQL
- **Flexibility:** Easy to modify calculation logic without database changes
- **Performance:** Can implement caching strategies more effectively

**Implementation:**
- DashboardAnalyticsService handles all aggregations
- Entity Framework loads data, C# performs calculations
- Calculated properties cached where appropriate
- Database optimized for data retrieval, not computation

**Trade-offs:**
- ✅ Clear separation of concerns
- ✅ Highly testable business logic
- ⚠️ Potential performance impact with large datasets
- ⚠️ More memory usage for data loading

### PD-002: Eager Loading for Dashboard Data
**Decision Date:** January 2025 (Session 5)  
**Status:** ✅ Implemented  
**Decision:** Use eager loading for dashboard data to minimize database round trips

**Context:** Dashboard requires data from multiple related entities

**Rationale:**
- **Performance:** Single query better than N+1 query problems
- **Predictability:** Known data requirements allow optimization
- **Simplicity:** Easier to reason about than complex lazy loading scenarios

**Implementation:**
```csharp
var contracts = await _context.Contracts
    .Include(c => c.ContractResources)
        .ThenInclude(cr => cr.Resource)
            .ThenInclude(r => r.LCAT)
    .ToListAsync();
```

**Consequences:**
- ✅ Predictable performance characteristics
- ✅ No N+1 query problems
- ⚠️ Larger memory usage for complex object graphs
- ⚠️ All data loaded even if not all needed

## Security Decisions

### SD-001: Deferred Authentication (MVP Phase)
**Decision Date:** January 2025 (Session 1)  
**Status:** ⚠️ Temporary  
**Decision:** Use placeholder authentication during MVP development

**Context:** MVP focus on business functionality over security infrastructure

**Rationale:**
- **Speed:** Avoid authentication complexity during rapid prototyping
- **Focus:** Business logic validation more important for MVP
- **Planning:** Security architecture designed for post-MVP implementation
- **Risk Acceptance:** Internal development environment acceptable for placeholder security

**Current Implementation:**
- All operations attributed to "System" user
- No authorization checks in business logic
- CORS configured for development (overly permissive)

**Production Plan:**
- Azure AD integration for authentication
- Role-based authorization (Admin, Manager, ReadOnly)
- Audit logging for all operations
- HTTPS enforcement and security headers

**Consequences:**
- ✅ Faster MVP development
- ✅ No authentication complexity blocking business logic development
- 🚨 NOT PRODUCTION READY - security implementation required before deployment
- ⚠️ Potential security architecture refactoring required

### SD-002: Input Validation Strategy
**Decision Date:** January 2025 (Session 2)  
**Status:** ✅ Implemented  
**Decision:** Multi-layer validation with business rules in domain entities

**Context:** Need for data integrity and business rule enforcement

**Rationale:**
- **Defense in Depth:** Validation at multiple layers provides comprehensive protection
- **Business Rules:** Domain entities enforce business logic constraints
- **API Contracts:** DTO validation ensures clean API contracts
- **User Experience:** Client-side validation provides immediate feedback

**Implementation:**
- Client-side validation for immediate user feedback
- API DTO validation attributes for contract enforcement
- Domain entity business rule validation
- Database constraints as final safety net

**Consequences:**
- ✅ Comprehensive data integrity protection
- ✅ Good user experience with immediate feedback
- ✅ Business rules enforced consistently
- ⚠️ Validation logic distributed across multiple layers

## Future Technology Decisions

### FTD-001: AI/ML Integration Planning
**Decision Date:** January 2025 (Session 6)  
**Status:** 🔄 Under Evaluation  
**Decision:** Evaluate AI/ML opportunities for predictive analytics

**Context:** Opportunity to provide intelligent business insights beyond basic reporting

**Potential Applications:**
- **Burn Rate Forecasting:** ML models for more accurate project predictions
- **Resource Optimization:** AI-powered resource allocation recommendations
- **Risk Assessment:** Automated contract health scoring
- **Natural Language:** AI-generated executive summaries and insights

**Evaluation Criteria:**
- Business value vs implementation complexity
- Data quality and quantity requirements
- Model accuracy and reliability expectations
- Integration complexity with existing architecture

**Next Steps:**
- Proof of concept with simple forecasting model
- Evaluate Azure ML vs AWS SageMaker vs local models
- Define success metrics for AI features

### FTD-002: Microservices Evolution Path
**Decision Date:** January 2025 (Session 6)  
**Status:** 📋 Future Planning  
**Decision:** Plan for potential microservices architecture at scale

**Context:** Current monolithic API may need to evolve for enterprise scale

**Trigger Conditions:**
- Team size >10 developers
- User base >1000 active users
- Data volume >1M records per entity
- Need for independent deployment cycles

**Potential Service Boundaries:**
- Contract Management Service
- Resource Management Service  
- Financial Analytics Service
- Reporting and Export Service
- Authentication and Authorization Service

**Architecture Considerations:**
- Event-driven communication between services
- Shared database vs database per service
- API gateway for client communication
- Service discovery and load balancing

## Decision Review Schedule

### Quarterly Architecture Review
**Purpose:** Evaluate architectural decisions against current business needs
**Participants:** Technical lead, product owner, key stakeholders
**Scope:** Major architectural decisions and technology choices

### Annual Technology Assessment
**Purpose:** Review technology stack for obsolescence, security, and performance
**Participants:** Full development team and IT architecture board
**Scope:** All technology decisions and future roadmap alignment

### Ad-Hoc Decision Reviews
**Triggers:**
- Significant performance issues
- Security vulnerabilities discovered
- Major business requirement changes
- Technology deprecation announcements

---

*Technical decisions shape the foundation of the system. All decisions are documented with context, rationale, and consequences for future reference and evolution.*