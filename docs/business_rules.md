# Business Rules - Contract Tracker

> **File Purpose:** Core business logic, calculations, and validation rules  
> **Last Updated:** January 2025 (Session 6)  
> **Critical For:** System integrity, financial accuracy, compliance

## Financial Calculations

### Wrap Rate Rules
**Business Context:** Transform pay rates into fully burdened costs including benefits, overhead, and profit

#### W2 Internal Employees
```
Burdened Cost = Pay Rate × 2.28
```
**Components:** Base salary + benefits (health, retirement, taxes) + overhead (facilities, management) + profit margin  
**Rationale:** Industry standard for federal contracting W2 burden rates  
**Variability:** May be customized per contract or updated annually  

#### Contractors (1099, Subcontractors)
```
Burdened Cost = Pay Rate × 1.15
```
**Components:** Base rate + minimal overhead (management, facilities allocation) + profit margin  
**Rationale:** Contractors provide own benefits, lower overhead burden  
**Exception:** Fixed-price resources use actual negotiated rates without wrap calculation

#### Override Capability
- Contract-specific wrap rates override default rates
- Effective date tracking for wrap rate changes
- Historical rate preservation for audit compliance
- Automatic recalculation when rates change

### Margin Analysis Rules
**Purpose:** Identify profitable vs underwater resources and contracts

#### Margin Calculation
```
Margin = ((Bill Rate - Burdened Cost) / Bill Rate) × 100
```

#### Margin Categories
- **Healthy:** >20% margin (green indicator)
- **Acceptable:** 10-20% margin (yellow indicator)  
- **Underwater:** <10% margin (red indicator)
- **Loss:** Negative margin (critical red indicator)

#### Business Impact
- Underwater resources require immediate attention
- Margin trends inform rate negotiation strategies
- Portfolio-level margin tracking for business health

### Burn Rate Calculations
**Purpose:** Track actual vs projected resource costs for budget management

#### Monthly Burn Rate
```
Monthly Burn = (Total Allocated Resources × Burdened Cost) / 12
```

#### Quarterly Burn Rate
```
Quarterly Burn = Monthly Burn × 3
```

#### Annual Burn Rate
```
Annual Burn = Monthly Burn × 12
```

#### Allocation Impact
- Resource allocation percentage affects burn calculation
- Multiple contract assignments split burn proportionally
- Effective dating changes burn calculations over time

## Resource Management Rules

### Resource Allocation Constraints
**Business Rule:** No resource can be allocated more than 100% across all contracts

#### Validation Logic
```
Sum(Allocation Percentages) ≤ 100% per resource
```

#### Enforcement Points
- Real-time validation during assignment creation
- Batch validation for bulk updates
- Warning messages for approaching 100% allocation
- Block operations that would exceed 100%

### Resource Type Business Rules

#### W2 Internal Employees
- **Benefits Eligible:** Automatically included in wrap rate calculation
- **LCAT Assignment:** Required for rate determination
- **Clearance Tracking:** Security clearance levels and expiration dates
- **Performance Reviews:** Annual review cycle tracking

#### Subcontractors
- **Contract Requirements:** Must have active subcontract agreement
- **Insurance Verification:** Liability and workers compensation required
- **Rate Negotiation:** Rates typically fixed for contract duration
- **Clearance Sponsorship:** May require clearance sponsorship tracking

#### 1099 Contractors
- **Independent Contractor Rules:** Must meet IRS independent contractor criteria
- **Rate Flexibility:** Hourly rates subject to negotiation
- **Limited Benefits:** No company benefits eligibility
- **Project-Based:** Typically assigned to specific deliverables

#### Fixed Price Resources
- **Deliverable-Based:** Payment tied to specific deliverables, not hours
- **Risk Management:** Fixed price transfers performance risk to vendor
- **Milestone Tracking:** Payment schedule based on milestone completion
- **Quality Standards:** Deliverable acceptance criteria definition

### LCAT Assignment Rules
**Business Context:** Labor categories determine billing rates and contract compliance

#### Assignment Requirements
- Each resource must have exactly one primary LCAT assignment
- LCAT assignment determines billing rate hierarchy
- Rate changes require LCAT reassignment or rate override
- Historical LCAT assignments preserved for audit compliance

#### Rate Hierarchy (Highest to Lowest Priority)
1. **Contract-Specific Rate Override:** Custom rate for specific resource/contract combination
2. **LCAT Published Rate:** Published rate for the specific LCAT
3. **LCAT Default Bill Rate:** Default rate when no published rate exists
4. **Error Condition:** No rate available requires manual intervention

## Contract Management Rules

### Contract Lifecycle Workflow
**Business Rule:** Contracts follow defined status progression with validation gates

#### Status Progression
```
Draft → Active → Closed
```

#### Status Validation Rules
- **Draft:** Can be modified freely, no resource assignments allowed
- **Active:** Resource assignments allowed, funding modifications tracked
- **Closed:** Read-only, historical data preservation required

#### Transition Requirements
- **Draft to Active:** Must have funding amount, start date, contract type
- **Active to Closed:** Must resolve all resource assignments and final billing
- **Reactivation:** Closed contracts cannot be reactivated (create new modification)

### Funding Management Rules
**Purpose:** Track contract value, funded amounts, and modification history

#### Funding Structure
- **Total Contract Value:** Maximum potential contract value (ceiling)
- **Funded Amount:** Currently available funding for work authorization
- **Remaining Funding:** Real-time calculation of available funds

#### Funding Calculations
```
Remaining Funding = Funded Amount - (Burned Amount + Committed Amount)
Burn Percentage = (Burned Amount / Funded Amount) × 100
```

#### Warning Thresholds
- **Critical (Red):** >90% funding burned OR <30 days remaining at current burn rate
- **High (Orange):** >75% funding burned OR <60 days remaining
- **Medium (Yellow):** >60% funding burned OR <90 days remaining  
- **Low (Green):** <60% funding burned AND >90 days remaining

### Prime vs Subcontractor Rules
**Business Context:** Different business rules apply based on contract relationship

#### Prime Contractor Rules
- **Full Control:** Complete resource allocation and rate setting authority
- **Revenue Recognition:** Full billing rate revenue recognition
- **Compliance Responsibility:** Primary responsibility for all contract compliance
- **Risk Management:** Full performance and financial risk

#### Subcontractor Rules
- **Limited Control:** Resource allocation subject to prime contractor approval
- **Pass-Through Revenue:** Revenue limited to subcontractor rate structure
- **Compliance Support:** Support prime contractor compliance requirements
- **Shared Risk:** Performance risk shared with prime contractor

## Data Integrity Rules

### Audit Trail Requirements
**Business Context:** Federal contracting requires complete change tracking

#### Change Tracking
- **All Rate Changes:** Historical rates preserved with effective dates
- **Resource Movements:** Complete history of resource assignments
- **Contract Modifications:** Funding changes and scope modifications tracked
- **User Actions:** Who made what changes when (planned for future implementation)

#### Data Retention
- **Active Data:** Immediately accessible in primary database
- **Historical Data:** Archived but accessible for audit purposes
- **Legal Requirements:** 7-year retention minimum for federal contracts
- **Performance Data:** Indefinite retention for business intelligence

### Data Validation Rules

#### Required Field Validation
- **Contracts:** Number, title, type, start date, total value
- **Resources:** Name, type, LCAT assignment, pay rate
- **LCATs:** Name, default bill rate, effective date
- **Assignments:** Resource, contract, allocation percentage, effective date

#### Business Logic Validation
- **Date Consistency:** Start dates before end dates, effective dates logical
- **Financial Consistency:** Positive rates and values, valid percentages
- **Relationship Integrity:** Valid foreign key relationships, referential integrity
- **Calculation Accuracy:** Real-time validation of all calculated fields

### Compliance Rules

#### Federal Contracting Compliance
- **Labor Category Compliance:** Resources must match contract LCAT requirements
- **Rate Justification:** All rates must be supportable in audit situations
- **Allocation Accuracy:** Resource time allocation must match contract requirements
- **Documentation Standards:** All assignments and rates must have business justification

#### Internal Business Compliance
- **Margin Thresholds:** Underwater resources require management approval
- **Allocation Limits:** No resource over-allocation across contracts
- **Rate Change Approval:** Significant rate changes require authorization workflow
- **Financial Controls:** Budget variance reporting and approval requirements

---

*These business rules are foundational to system integrity. Any changes require business stakeholder approval and thorough testing.*