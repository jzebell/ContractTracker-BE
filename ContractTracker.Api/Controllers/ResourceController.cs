using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractTracker.Api.DTOs.Resource;
using ContractTracker.Domain.Entities;
using ContractTracker.Infrastructure.Data;

namespace ContractTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly AppDbContext _context;
        private const decimal DEFAULT_WRAP_RATE = 2.28m; // Default wrap rate

        public ResourceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResources()
        {
            var resources = await _context.Resources
                .Include(r => r.LCAT)
                    .ThenInclude(l => l.Rates)
                .Where(r => r.IsActive)
                .ToListAsync();

            var dtos = resources.Select(r => MapToDto(r));
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDto>> GetResource(Guid id)
        {
            var resource = await _context.Resources
                .Include(r => r.LCAT)
                    .ThenInclude(l => l.Rates)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resource == null)
                return NotFound();

            return Ok(MapToDto(resource));
        }

        [HttpPost]
        public async Task<ActionResult<ResourceDto>> CreateResource(CreateResourceDto dto)
        {
            // Validate email uniqueness
            if (await _context.Resources.AnyAsync(r => r.Email == dto.Email))
                return BadRequest($"Resource with email '{dto.Email}' already exists");

            // Validate LCAT exists
            var lcat = await _context.LCATs
                .Include(l => l.Rates)
                .FirstOrDefaultAsync(l => l.Id == dto.LCATId);
            
            if (lcat == null)
                return BadRequest($"LCAT with ID '{dto.LCATId}' not found");

            // Parse resource type enum
            if (!Enum.TryParse<ResourceType>(dto.ResourceType, out var resourceType))
                return BadRequest($"Invalid resource type: {dto.ResourceType}");

            var resource = new Resource(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                resourceType,
                dto.LCATId,
                dto.HourlyRate,
                dto.StartDate,
                "System"
            );

            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            // Reload with includes for DTO mapping
            resource = await _context.Resources
                .Include(r => r.LCAT)
                    .ThenInclude(l => l.Rates)
                .FirstAsync(r => r.Id == resource.Id);

            return CreatedAtAction(nameof(GetResource), new { id = resource.Id }, MapToDto(resource));
        }

        private ResourceDto MapToDto(Resource resource)
        {
            // Calculate burdened cost based on resource type
            var wrapRate = resource.ResourceType switch
            {
                ResourceType.Subcontractor => 1.15m,
                ResourceType.Contractor1099 => 1.15m,
                ResourceType.FixedPrice => 1.0m,
                _ => DEFAULT_WRAP_RATE // W2Internal
            };

            var burdenedCost = resource.HourlyRate * wrapRate;

            // Get bill rate from LCAT default rate
            decimal? billRate = null;
            if (resource.LCAT != null)
            {
                var defaultRate = resource.LCAT.GetCurrentDefaultRate();
                billRate = defaultRate?.Rate;
            }

            return new ResourceDto
            {
                Id = resource.Id,
                FirstName = resource.FirstName,
                LastName = resource.LastName,
                FullName = resource.FullName,
                Email = resource.Email,
                ResourceType = resource.ResourceType.ToString(),
                LCATId = resource.LCATId,
                LCATName = resource.LCAT?.Name,
                HourlyRate = resource.HourlyRate,
                BurdenedCost = burdenedCost,
                BillRate = billRate,
                StartDate = resource.StartDate,
                IsActive = resource.IsActive
            };
        }
    }
}