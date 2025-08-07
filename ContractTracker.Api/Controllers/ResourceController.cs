using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractTracker.Api.DTOs;
using ContractTracker.Domain.Entities;
using ContractTracker.Infrastructure.Data;

namespace ContractTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ResourceController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResources()
        {
            var resources = await _context.Resources
                .Include(r => r.LCAT)
                .Include(r => r.ContractResources)
                .ToListAsync();

            var resourceDtos = resources.Select(r => MapToDto(r)).ToList();
            return Ok(resourceDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResourceDto>> GetResource(Guid id)
        {
            var resource = await _context.Resources
                .Include(r => r.LCAT)
                .Include(r => r.ContractResources)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (resource == null)
                return NotFound();

            return Ok(MapToDto(resource));
        }

        [HttpPost]
        public async Task<ActionResult<ResourceDto>> CreateResource(CreateResourceDto dto)
        {
            // Validate LCAT exists if provided
            if (dto.LCATId.HasValue)
            {
                var lcatExists = await _context.LCATs.AnyAsync(l => l.Id == dto.LCATId.Value);
                if (!lcatExists)
                    return BadRequest("Invalid LCAT ID");
            }

            // Check for duplicate email
            var emailExists = await _context.Resources.AnyAsync(r => r.Email == dto.Email);
            if (emailExists)
                return BadRequest("A resource with this email already exists");

            var resource = new Resource(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.Type,
                dto.LCATId,
                dto.PayRate,
                dto.StartDate,
                dto.ClearanceLevel ?? "",
                dto.Location ?? "",
                dto.Notes ?? "",
                "System" // CreatedBy - will be replaced with actual user
            );

            _context.Resources.Add(resource);
            await _context.SaveChangesAsync();

            // Reload with LCAT
            await _context.Entry(resource)
                .Reference(r => r.LCAT)
                .LoadAsync();

            return CreatedAtAction(nameof(GetResource), new { id = resource.Id }, MapToDto(resource));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResource(Guid id, UpdateResourceDto dto)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
                return NotFound();

            // Validate LCAT exists if provided
            if (dto.LCATId.HasValue)
            {
                var lcatExists = await _context.LCATs.AnyAsync(l => l.Id == dto.LCATId.Value);
                if (!lcatExists)
                    return BadRequest("Invalid LCAT ID");
            }

            resource.UpdateDetails(
                dto.FirstName,
                dto.LastName,
                dto.Email,
                dto.Type,
                dto.LCATId,
                dto.PayRate,
                dto.ClearanceLevel ?? "",
                dto.Location ?? "",
                dto.Notes ?? "",
                "System" // ModifiedBy - will be replaced with actual user
            );

            await _context.SaveChangesAsync();

            // Reload with LCAT
            await _context.Entry(resource)
                .Reference(r => r.LCAT)
                .LoadAsync();

            return Ok(MapToDto(resource));
        }

        [HttpPut("batch")]
        public async Task<IActionResult> BatchUpdateResources([FromBody] List<UpdateResourceDto> updates)
        {
            var updateResults = new List<object>();

            foreach (var update in updates)
            {
                // This is a simplified batch update - in production you'd want more sophisticated handling
                if (update != null)
                {
                    // You'd implement the actual batch update logic here
                    updateResults.Add(new { success = true, message = "Batch update placeholder" });
                }
            }

            await _context.SaveChangesAsync();
            return Ok(updateResults);
        }

        [HttpPost("{id}/terminate")]
        public async Task<IActionResult> TerminateResource(Guid id, [FromBody] DateTime? endDate)
        {
            var resource = await _context.Resources
                .Include(r => r.ContractResources)
                .FirstOrDefaultAsync(r => r.Id == id);
                
            if (resource == null)
                return NotFound();

            try
            {
                resource.Terminate(endDate ?? DateTime.UtcNow, "System");
                await _context.SaveChangesAsync();
                return Ok(new { message = "Resource terminated successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/reactivate")]
        public async Task<IActionResult> ReactivateResource(Guid id)
        {
            var resource = await _context.Resources.FindAsync(id);
            if (resource == null)
                return NotFound();

            resource.Reactivate("System");
            await _context.SaveChangesAsync();
            
            return Ok(new { message = "Resource reactivated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResource(Guid id)
        {
            var resource = await _context.Resources
                .Include(r => r.ContractResources)
                .FirstOrDefaultAsync(r => r.Id == id);
                
            if (resource == null)
                return NotFound();

            // Check if resource is assigned to any active contracts
            if (resource.ContractResources.Any(cr => cr.IsActive))
                return BadRequest("Cannot delete resource that is assigned to active contracts");

            _context.Resources.Remove(resource);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private ResourceDto MapToDto(Resource resource)
        {
            return new ResourceDto
            {
                Id = resource.Id,
                FirstName = resource.FirstName,
                LastName = resource.LastName,
                Email = resource.Email,
                Type = resource.Type,
                LCATId = resource.LCATId,
                LCATName = resource.LCAT?.Name ?? "Unassigned",
                PayRate = resource.PayRate,
                BurdenedCost = resource.BurdenedCost,
                StartDate = resource.StartDate,
                EndDate = resource.EndDate,
                IsActive = resource.IsActive,
                ClearanceLevel = resource.ClearanceLevel,
                ClearanceExpirationDate = resource.ClearanceExpirationDate,
                Location = resource.Location,
                Notes = resource.Notes,
                TotalAllocation = resource.GetTotalAllocation(),
                Margin = resource.CalculateMargin(),
                IsUnderwater = resource.IsUnderwater(),
                CreatedAt = resource.CreatedAt,
                UpdatedAt = resource.UpdatedAt
            };
        }
    }
}