# Known Issues - Contract Tracker

> **File Purpose:** Current blockers, workarounds, and technical debt items  
> **Last Updated:** January 2025 (Session 6)  
> **Update Trigger:** When issues are discovered, resolved, or workarounds implemented

## 🔴 Critical Issues (Blocking Progress)

### CI-001: Dashboard Data Validation Needed
**Discovered:** Session 6  
**Impact:** Dashboard may show incorrect or missing data  
**Severity:** High - affects primary user workflow  

**Description:** Financial dashboard implementation complete but data flow needs verification. Calculations may be incorrect due to:
- Resource.BurdenedCost potentially returning 0
- LCAT rate assignments not loading properly  
- Date range calculations in projections
- Empty data scenarios not handled gracefully

**Symptoms:**
- Dashboard cards showing $0 or empty values
- Charts not rendering or showing no data
- Console errors in browser developer tools
- API returning successful responses but empty data sets

**Immediate Actions Required:**
1. Test with known good data (create LCATs → Resources → Contracts → Assignments)
2. Add console logging to trace data through calculation pipeline
3. Verify DashboardAnalyticsService calculations manually
4. Add error boundaries and loading states to React components

**Workaround:** Use API debug panel to verify data existence before dashboard testing

### CI-002: Missing Test Data for Dashboard
**Discovered:** Session 6  
**Impact:** Cannot properly demonstrate dashboard functionality  
**Severity:** Medium - affects stakeholder demos  

**Description:** System has entities and relationships but insufficient test data to showcase dashboard analytics meaningfully.

**Required Test Data:**
- 3-5 LCATs with realistic rates ($50-150/hour range)
- 8-12 Resources assigned to various LCATs
- 4-6 Contracts with different funding levels and types
- Resource assignments across contracts with realistic allocation percentages

**Actions Required:**
1. Create data generation script or manual data entry process
2. Verify all relationships are properly established
3. Test edge cases (over-allocated resources, underwater margins, critical funding)

## 🟡 Important Issues (Should Address Soon)

### II-001: Performance Monitoring Missing
**Discovered:** Session 5  
**Impact:** Cannot identify performance bottlenecks  
**Severity:** Medium - affects user experience optimization  

**Description:** No systematic performance monitoring in place. Dashboard load times unknown, database query performance unmeasured.

**Missing Capabilities:**
- API response time measurement
- Database query execution time logging
- Frontend rendering performance tracking
- Memory usage monitoring

**Planned Resolution:** Add performance monitoring in next development phase

**Workaround:** Manual timing of operations using browser developer tools

### II-002: Error Handling Inconsistent
**Discovered:** Session 5  
**Impact:** Poor user experience when errors occur  
**Severity:** Medium - affects user experience and debugging  

**Description:** Error handling varies across components. Some areas have comprehensive error boundaries, others fail silently or show technical error messages.

**Specific Issues:**
- API errors not always translated to user-friendly messages
- Network errors can cause UI to freeze or show blank screens
- Database constraint violations shown as technical error messages
- No centralized error logging or user notification system

**Planned Resolution:** Implement comprehensive error handling strategy

**Workaround:** Check browser console for technical error details when UI behavior unexpected

### II-003: TypeScript 'any' Type Usage
**Discovered:** Session 5  
**Impact:** Reduced type safety and development experience  
**Severity:** Low - affects code quality and maintainability  

**Description:** Some components use 'any' type instead of proper TypeScript definitions, reducing type safety benefits.

**Locations:**
- Dashboard component state management
- API response handling in some services
- Event handlers in form components

**Planned Resolution:** Replace 'any' types with proper interfaces during code quality improvement phase

**Workaround:** Extra runtime validation in components using 'any' types

## 🟢 Minor Issues (Enhancement Opportunities)

### MI-001: Mobile Responsiveness Limited
**Discovered:** Session 5  
**Impact:** Dashboard not optimized for mobile/tablet viewing  
**Severity:** Low - affects accessibility but not core functionality  

**Description:** Dashboard designed for desktop viewing. Charts and tables may not display optimally on smaller screens.

**Specific Issues:**
- Chart legends may overlap on narrow screens
- Table horizontal scrolling needed for many columns
- Touch interactions not optimized for charts
- Font sizes may be too small on mobile devices

**Planned Resolution:** Responsive design improvements in UI enhancement phase

**Workaround:** Use desktop or tablet in landscape mode for optimal experience

### MI-002: No Keyboard Navigation
**Discovered:** Session 2  
**Impact:** Accessibility concern for keyboard-only users  
**Severity:** Low - affects accessibility compliance  

**Description:** Custom form components lack comprehensive keyboard navigation support.

**Missing Features:**
- Tab navigation through inline editing fields
- Keyboard shortcuts for common actions (Save All, Cancel)
- Enter key handling in form fields
- Escape key to cancel editing mode

**Planned Resolution:** Accessibility improvements in UI enhancement phase

**Workaround:** Use mouse/touch for navigation and form interaction

## 🔧 Resolved Issues

### RI-001: Entity Property Mismatches (Session 5)
**Discovered:** Session 5  
**Resolved:** Session 5  
**Issue:** Controllers referenced non-existent entity properties causing 89+ compilation errors

**Resolution:** Complete entity and controller refactoring:
- Fixed all property name mismatches (CreatedAt vs CreatedDate, etc.)
- Added missing properties (Resource.BurdenedCost)
- Created comprehensive DTO structure
- Updated all Entity Framework configurations

**Outcome:** Clean compilation, proper entity structure, maintainable codebase

### RI-002: CORS Configuration Issues (Session 2)  
**Discovered:** Session 2  
**Resolved:** Session 2  
**Issue:** Frontend unable to connect to backend API due to CORS policy

**Resolution:** 
- Added CORS configuration in Program.cs
- Ensured CORS middleware ordered before authorization
- Configured to allow all origins for development

**Outcome:** Successful API connectivity, frontend-backend integration working

### RI-003: Database Migration Conflicts (Session 1)
**Discovered:** Session 1  
**Resolved:** Session 1  
**Issue:** Entity Framework migrations failing due to schema conflicts

**Resolution:**
- Reset migration history
- Recreated initial migration with correct schema
- Established proper migration workflow

**Outcome:** Stable database schema, reliable migration process

## 🚨 Technical Debt

### TD-001: No Automated Testing
**Category:** Quality Assurance  
**Priority:** High (for production)  
**Estimated Effort:** 2-3 weeks

**Description:** No unit tests, integration tests, or end-to-end tests implemented. All testing currently manual.

**Risks:**
- Regression bugs during refactoring
- Difficult to validate business logic changes
- Slower development velocity for complex changes
- Reduced confidence in releases

**Resolution Plan:**
- Unit tests for domain entities and business logic
- Integration tests for API endpoints
- End-to-end tests for critical user workflows
- Test data factories for consistent test scenarios

### TD-002: Hardcoded Configuration Values
**Category:** Configuration Management  
**Priority:** Medium  
**Estimated Effort:** 1 week

**Description:** Some configuration values embedded in code rather than externalized.

**Examples:**
- Wrap rate multipliers (2.28, 1.15) in business logic
- API timeouts and connection strings in some components
- Chart colors and styling in component code

**Resolution Plan:**
- Configuration service for business rules
- Environment-specific configuration files
- Admin interface for configurable business rules

### TD-003: Limited Logging and Monitoring
**Category:** Observability  
**Priority:** High (for production)  
**Estimated Effort:** 1-2 weeks

**Description:** Minimal logging and no structured monitoring in place.

**Missing Capabilities:**
- Structured logging with correlation IDs
- Performance metrics collection
- Error tracking and alerting
- User activity monitoring

**Resolution Plan:**
- Implement structured logging (Serilog)
- Add application performance monitoring
- Create operational dashboards
- Establish alerting for critical errors

---

*Known issues guide debugging efforts and technical debt prioritization. Update immediately when issues are discovered or resolved.*