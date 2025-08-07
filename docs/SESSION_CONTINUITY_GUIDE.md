# Session Continuity Guide for AI Assistant

## 🎯 Purpose
This document ensures seamless continuation across sessions when working on the Contract Tracker project. Follow these steps at the start of each session to quickly regain context and continue productive development.

## 📚 Session Start Protocol

### Step 1: Read Core Context Documents
**Always start by searching for and reading these documents in order:**

1. **PROJECT_CONTEXT.md** (in `/contract-tracker-backend/docs/`)
   - Contains business requirements, technical decisions, and development progress
   - Pay special attention to the "Development Progress Log" section
   - Note any "In Progress" items - these are likely where we left off

2. **NEXT_SESSION_START.md** (if it exists in `/contract-tracker-backend/docs/`)
   - Contains specific handoff notes from the previous session
   - Includes current state, what's working, and what to build next
   - Has environment details and gotchas to remember
   - **If this file exists, it takes priority for understanding the immediate state**

3. **SESSION_CONTINUITY_GUIDE.md** (this document)
   - Ensures you're following the correct protocol
   - Contains the current state checklist

4. **Financials Workbook.xlsx** (if discussing financial calculations)
   - Contains the original Excel formulas and business logic
   - Reference for wrap rate calculations and financial requirements

### Step 2: Assess Current State
After reading the context documents, verify the current state by checking:

```
✓ What was the last completed feature?
✓ What was in progress when the session ended?
✓ Are there any known issues that need addressing?
✓ What's the next item in the roadmap?
✓ Are there specific handoff notes to follow?
```

### Step 3: Acknowledge Context to User
Start the session by confirming what you understand:

```
"I've reviewed the project context and handoff notes. I can see that we:
- Last completed: [specific feature from NEXT_SESSION_START.md or PROJECT_CONTEXT.md]
- Were working on: [in-progress item]
- Next priority is: [next roadmap item]
- [Any specific notes from the handoff]

Shall we continue with [specific task] or would you like to focus on something else?"
```

## 🔍 Key Information Locations

### Backend Structure
```
contract-tracker-backend/
├── docs/
│   ├── PROJECT_CONTEXT.md      # Main project documentation
│   ├── SESSION_CONTINUITY_GUIDE.md  # This file
│   ├── NEXT_SESSION_START.md   # Session handoff notes (if exists)
│   └── Financials Workbook.xlsx # Business logic reference
├── ContractTracker.Domain/      # Entity definitions
│   └── Entities/               # Check here for domain model
├── ContractTracker.Infrastructure/
│   └── Data/
│       ├── AppDbContext.cs    # Database context
│       └── Configurations/    # EF configurations
└── ContractTracker.Api/
    ├── Controllers/            # API endpoints
    └── DTOs/                  # Data transfer objects
```

### Frontend Structure
```
contract-tracker-frontend/
├── .env                        # Environment variables (API URL)
├── src/
│   ├── components/            # React components
│   │   ├── LCAT/             # LCAT management UI
│   │   ├── Resource/         # Resource management UI
│   │   └── Debug/            # API debug panel
│   ├── services/             # API integration
│   ├── types/                # TypeScript definitions
│   └── App.tsx               # Main app component
```

## 🚦 Current System Status

### ✅ Completed Components (Session 2 - Jan 10, 2025)
- **Domain Layer**: All entities created with proper encapsulation
- **Database**: PostgreSQL running in Docker, all migrations applied
- **LCAT Management**: Full CRUD with inline editing and batch saves
- **Resource Management**: Full CRUD with filtering, search, and margin calculations
- **API Integration**: All endpoints working with proper CORS configuration
- **Debug Tools**: ApiDebug component for troubleshooting

### 🔄 In Progress
- **Contract Management**: Not started
- **Financial Dashboard**: Not started
- **Resource-Contract Assignment**: Not started

### 📋 Known Configuration
- **API URL**: http://localhost:5154
- **Frontend URL**: http://localhost:3000
- **Database**: PostgreSQL in Docker (port 5432)
- **Default Wrap Rate**: 2.28x for W2, 1.15x for contractors
- **API Routes**: `/api/LCAT`, `/api/Resource` (note the /api prefix and capitalization)

## 💡 Development Patterns to Follow

### Backend Patterns
1. **Entity Creation**: Domain entities with private setters and business logic methods
2. **DTOs**: Separate DTOs for Create, Update, and Read operations
3. **Validation**: Check uniqueness, existence, and business rules in controllers
4. **Transactions**: Use transactions for batch operations
5. **Routes**: Use `/api/[controller]` pattern with proper capitalization

### Frontend Patterns
1. **Inline Editing**: Excel-like editing with batch save
2. **Visual Feedback**: Yellow highlighting for unsaved changes
3. **Service Layer**: Centralized API calls through service files
4. **Type Safety**: Full TypeScript types for all data structures
5. **Error Handling**: Comprehensive try-catch with user feedback

## 🎯 Quick Decision Reference

### Business Rules
- **Rate Hierarchy**: Published Rate → Default Bill Rate → Contract Override
- **Resource Types**: W2Internal, Subcontractor, Contractor1099, FixedPrice
- **Billing**: Monthly split into 24 payments/year, 1912 work hours annually
- **Versioning**: All rates are versioned with effective dates

### Technical Choices
- **No permissions** initially (all users are super users)
- **No conflict handling** yet (will add locking later)
- **Separate repositories** for frontend and backend
- **Rate versioning** from day one for historical accuracy

## 🔄 Session Continuity Checklist

Before starting work:
- [ ] Read PROJECT_CONTEXT.md
- [ ] Read NEXT_SESSION_START.md (if exists)
- [ ] Check "Development Progress Log" for last session's work
- [ ] Verify API is running (`dotnet run` in Api folder)
- [ ] Verify database is running (`docker ps`)
- [ ] Verify frontend is running (`npm start` in frontend folder)
- [ ] Test that Resources and LCATs tabs load data
- [ ] Ask user: "Should we continue with [last in-progress item]?"

## 🚨 Common Issues & Quick Fixes

### API Won't Start
```bash
# Check if port is in use
netstat -ano | findstr :5154
# Kill the process or use different port
```

### Database Connection Failed
```bash
# Start Docker container
cd C:\Projects\ContractTracker
docker-compose up -d
```

### Build Errors
```bash
# Ensure all project references are added
dotnet add ContractTracker.Api/ContractTracker.Api.csproj reference ContractTracker.Infrastructure/ContractTracker.Infrastructure.csproj
dotnet add ContractTracker.Infrastructure/ContractTracker.Infrastructure.csproj reference ContractTracker.Domain/ContractTracker.Domain.csproj
```

### Frontend Won't Connect to Backend
```bash
# Check .env file exists with:
REACT_APP_API_URL=http://localhost:5154

# Restart React app after creating/modifying .env
npm start
```

### CORS Errors
Ensure Program.cs has CORS configured BEFORE UseAuthorization:
```csharp
app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
```

## 📝 Documentation Maintenance Protocol

### Creating Handoff Notes for Next Session
At the end of each session, create or update `NEXT_SESSION_START.md` with:
1. **Session summary** - What was accomplished
2. **Current state** - What's working
3. **Next steps** - Clear priorities for next session
4. **Environment details** - Ports, URLs, configurations
5. **Gotchas** - Any tricky issues to remember

### Continuous Updates During Session
**IMPORTANT: Update PROJECT_CONTEXT.md immediately when:**

1. **Feature Completed** 
   - Move item from "In Progress" to "Completed" with ✅
   - Add any new API endpoints created
   - Document any patterns established

2. **New Decision Made**
   - Add to "Technical Decisions Made" section
   - Update "Business Rules" if affected
   - Note in "Development Progress Log" with date

3. **Issue Encountered & Resolved**
   - Add to "Known Issues & Fixes Applied"
   - Include the solution for future reference

4. **Starting New Component**
   - Add to "In Progress" with ⏳
   - Update roadmap if priorities change

### Update Triggers - Do This After:
- ✅ Creating new entity or domain model → Update domain model section
- ✅ Adding new API endpoint → Update "API Endpoints Available"
- ✅ Implementing new feature → Update "Current Working Features"
- ✅ Making architectural decision → Update "Technical Decisions Made"
- ✅ Solving a problem → Add to "Known Issues & Fixes Applied"
- ✅ Completing a TODO → Move from roadmap to completed

### How to Update
```markdown
Example update after creating a new feature:
1. In PROJECT_CONTEXT.md, find "Development Progress Log"
2. Update the current session section
3. Change: "⏳ Need to build React frontend for Resources"
4. To: "✅ Built React frontend for Resources with filtering and sorting"
5. Add any new decisions or patterns discovered
```

### Session End Protocol
Before ending a session:
1. Update PROJECT_CONTEXT.md with all progress
2. Create/Update NEXT_SESSION_START.md with handoff notes
3. Commit all documentation changes to git
4. Ensure the project is in a working state

### Sample Update Command
When making updates, use clear commit-style messages:
```
"Updated PROJECT_CONTEXT.md: 
- Completed Resource frontend with filtering
- Added batch update pattern to Technical Decisions
- Next: Contract management UI"
```

---
*Remember: Always start by reading PROJECT_CONTEXT.md and NEXT_SESSION_START.md (if it exists) to understand where we are in the development journey.*