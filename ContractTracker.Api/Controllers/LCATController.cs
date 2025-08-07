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
    public class LCATController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LCATController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LCATDto>>> GetLCATs()
        {
            var lcats = await _context.LCATs
                .Include(l => l.Rates)
                .ToListAsync();

            var lcatDtos = lcats.Select(l => MapToDto(l)).ToList();
            return Ok(lcatDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LCATDto>> GetLCAT(Guid id)
        {
            var lcat = await _context.LCATs
                .Include(l => l.Rates)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lcat == null)
                return NotFound();

            return Ok(MapToDto(lcat));
        }

        [HttpPost]
        public async Task<ActionResult<LCATDto>> CreateLCAT(CreateLCATDto dto)
        {
            // Check for duplicate code
            var codeExists = await _context.LCATs.AnyAsync(l => l.Code == dto.Code);
            if (codeExists)
                return BadRequest("An LCAT with this code already exists");

            var lcat = new LCAT(
                dto.Code,
                dto.Name,
                dto.Description ?? "",
                dto.Category ?? "General",
                "System" // CreatedBy - will be replaced with actual user
            );

            // Add initial rates
            if (dto.PublishedRate > 0 || dto.DefaultBillRate > 0)
            {
                lcat.AddRate(dto.PublishedRate, dto.DefaultBillRate, DateTime.UtcNow, "System");
            }

            _context.LCATs.Add(lcat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLCAT), new { id = lcat.Id }, MapToDto(lcat));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLCAT(Guid id, UpdateLCATDto dto)
        {
            var lcat = await _context.LCATs.FindAsync(id);
            if (lcat == null)
                return NotFound();

            // Check for duplicate code (excluding current LCAT)
            var codeExists = await _context.LCATs
                .AnyAsync(l => l.Code == dto.Code && l.Id != id);
            if (codeExists)
                return BadRequest("An LCAT with this code already exists");

            lcat.UpdateDetails(
                dto.Code,
                dto.Name,
                dto.Description ?? "",
                dto.Category ?? "General",
                "System" // ModifiedBy - will be replaced with actual user
            );

            await _context.SaveChangesAsync();
            return Ok(MapToDto(lcat));
        }

        [HttpPost("{id}/rates")]
        public async Task<IActionResult> AddRate(Guid id, AddRateDto dto)
        {
            var lcat = await _context.LCATs
                .Include(l => l.Rates)
                .FirstOrDefaultAsync(l => l.Id == id);
                
            if (lcat == null)
                return NotFound();

            lcat.AddRate(dto.PublishedRate, dto.DefaultBillRate, dto.EffectiveDate, "System");
            
            await _context.SaveChangesAsync();
            return Ok(new { message = "Rate added successfully" });
        }

        [HttpPost("{id}/deactivate")]
        public async Task<IActionResult> DeactivateLCAT(Guid id)
        {
            var lcat = await _context.LCATs.FindAsync(id);
            if (lcat == null)
                return NotFound();

            lcat.Deactivate("System");
            await _context.SaveChangesAsync();
            
            return Ok(new { message = "LCAT deactivated successfully" });
        }

        [HttpPost("{id}/reactivate")]
        public async Task<IActionResult> ReactivateLCAT(Guid id)
        {
            var lcat = await _context.LCATs.FindAsync(id);
            if (lcat == null)
                return NotFound();

            lcat.Reactivate("System");
            await _context.SaveChangesAsync();
            
            return Ok(new { message = "LCAT reactivated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLCAT(Guid id)
        {
            var lcat = await _context.LCATs
                .Include(l => l.Rates)
                .FirstOrDefaultAsync(l => l.Id == id);
                
            if (lcat == null)
                return NotFound();

            // Check if LCAT is assigned to any resources
            var hasResources = await _context.Resources.AnyAsync(r => r.LCATId == id);
            if (hasResources)
                return BadRequest("Cannot delete LCAT that is assigned to resources");

            _context.LCATs.Remove(lcat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("{id}/position-titles")]
        public async Task<IActionResult> AddPositionTitle(Guid id, [FromBody] string title)
        {
            var lcat = await _context.LCATs
                .Include(l => l.PositionTitles)
                .FirstOrDefaultAsync(l => l.Id == id);
                
            if (lcat == null)
                return NotFound();

            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Position title cannot be empty");

            lcat.AddPositionTitle(title, "System");
            await _context.SaveChangesAsync();
            
            return Ok(new { message = "Position title added successfully" });
        }

        private LCATDto MapToDto(LCAT lcat)
        {
            var currentPublishedRate = lcat.GetCurrentPublishedRate();
            var currentDefaultRate = lcat.GetCurrentDefaultRate();

            return new LCATDto
            {
                Id = lcat.Id,
                Code = lcat.Code,
                Name = lcat.Name,
                Description = lcat.Description,
                Category = lcat.Category,
                IsActive = lcat.IsActive,
                CurrentPublishedRate = currentPublishedRate?.Rate ?? 0,
                CurrentDefaultBillRate = currentDefaultRate?.Rate ?? 0,
                CreatedAt = lcat.CreatedAt,
                UpdatedAt = lcat.UpdatedAt
            };
        }
    }
}