using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractTracker.Api.DTOs.LCAT;
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
                .Include(l => l.PositionTitles)
                .Where(l => l.IsActive)
                .ToListAsync();

            var dtos = lcats.Select(l => new LCATDto
            {
                Id = l.Id,
                Name = l.Name,
                Description = l.Description,
                CurrentPublishedRate = l.GetCurrentPublishedRate()?.Rate,
                CurrentDefaultBillRate = l.GetCurrentDefaultRate()?.Rate,
                PositionTitles = l.PositionTitles.Where(p => p.IsActive).Select(p => p.Title).ToList(),
                IsActive = l.IsActive,
                CreatedDate = l.CreatedDate,
                ModifiedDate = l.ModifiedDate
            });

            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LCATDto>> GetLCAT(Guid id)
        {
            var lcat = await _context.LCATs
                .Include(l => l.Rates)
                .Include(l => l.PositionTitles)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lcat == null)
                return NotFound();

            var dto = new LCATDto
            {
                Id = lcat.Id,
                Name = lcat.Name,
                Description = lcat.Description,
                CurrentPublishedRate = lcat.GetCurrentPublishedRate()?.Rate,
                CurrentDefaultBillRate = lcat.GetCurrentDefaultRate()?.Rate,
                PositionTitles = lcat.PositionTitles.Where(p => p.IsActive).Select(p => p.Title).ToList(),
                IsActive = lcat.IsActive,
                CreatedDate = lcat.CreatedDate,
                ModifiedDate = lcat.ModifiedDate
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<LCATDto>> CreateLCAT(CreateLCATDto dto)
        {
            // Check for duplicate name
            if (await _context.LCATs.AnyAsync(l => l.Name == dto.Name))
                return BadRequest($"LCAT with name '{dto.Name}' already exists");

            var lcat = new LCAT(dto.Name, dto.Description, "System");

            // Add initial rates
            var publishedRate = new LCATRate(
                lcat.Id,
                RateType.Published,
                dto.PublishedRate,
                DateTime.UtcNow,
                "System",
                "Initial rate");

            var defaultRate = new LCATRate(
                lcat.Id,
                RateType.DefaultBill,
                dto.DefaultBillRate,
                DateTime.UtcNow,
                "System",
                "Initial rate");

            lcat.Rates.Add(publishedRate);
            lcat.Rates.Add(defaultRate);

            // Add position titles
            if (dto.PositionTitles != null)
            {
                foreach (var title in dto.PositionTitles)
                {
                    var position = new PositionTitle(lcat.Id, title, "System");
                    lcat.PositionTitles.Add(position);
                }
            }

            _context.LCATs.Add(lcat);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLCAT), new { id = lcat.Id }, new LCATDto
            {
                Id = lcat.Id,
                Name = lcat.Name,
                Description = lcat.Description,
                CurrentPublishedRate = dto.PublishedRate,
                CurrentDefaultBillRate = dto.DefaultBillRate,
                PositionTitles = dto.PositionTitles,
                IsActive = true,
                CreatedDate = lcat.CreatedDate
            });
        }
        [HttpPost("batch-update-rates")]
public async Task<IActionResult> BatchUpdateRates(BatchUpdateRatesDto dto)
{
    using var transaction = await _context.Database.BeginTransactionAsync();

    try
    {
        foreach (var update in dto.RateUpdates)
        {
            var lcat = await _context.LCATs
                .Include(l => l.Rates)
                .FirstOrDefaultAsync(l => l.Id == update.LCATId);

            if (lcat == null)
                continue;

            // Update published rate if provided
            if (update.PublishedRate.HasValue)
            {
                var currentPublished = lcat.GetCurrentPublishedRate();
                if (currentPublished != null)
                {
                    currentPublished.SetEndDate(DateTime.UtcNow);
                }

                var newPublishedRate = new LCATRate(
                    lcat.Id,
                    RateType.Published,
                    update.PublishedRate.Value,
                    DateTime.UtcNow,
                    "System",
                    dto.Notes);

                _context.LCATRates.Add(newPublishedRate);
            }

            // Update default bill rate if provided
            if (update.DefaultBillRate.HasValue)
            {
                var currentDefault = lcat.GetCurrentDefaultRate();
                if (currentDefault != null)
                {
                    currentDefault.SetEndDate(DateTime.UtcNow);
                }

                var newDefaultRate = new LCATRate(
                    lcat.Id,
                    RateType.DefaultBill,
                    update.DefaultBillRate.Value,
                    DateTime.UtcNow,
                    "System",
                    dto.Notes);

                _context.LCATRates.Add(newDefaultRate);
            }
        }

        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return Ok(new { message = $"Updated rates for {dto.RateUpdates.Count} LCATs" });
    }
    catch (Exception ex)
    {
        await transaction.RollbackAsync();
        return StatusCode(500, new { error = "Failed to update rates", details = ex.Message });
    }
}
    }
}