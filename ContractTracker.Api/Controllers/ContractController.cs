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
    public class ContractController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContractController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractDto>>> GetContracts()
        {
            var contracts = await _context.Contracts
                .Include(c => c.ContractResources)
                .ToListAsync();

            var contractDtos = contracts.Select(c => MapToDto(c)).ToList();
            return Ok(contractDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ContractDto>> GetContract(Guid id)
        {
            var contract = await _context.Contracts
                .Include(c => c.ContractResources)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contract == null)
                return NotFound();

            return Ok(MapToDto(contract));
        }

        [HttpPost]
        public async Task<ActionResult<ContractDto>> CreateContract(CreateContractDto dto)
        {
            var contract = new Contract(
                dto.ContractNumber,
                dto.CustomerName,
                dto.PrimeContractorName,
                dto.IsPrime,
                dto.Type,
                dto.StartDate,
                dto.EndDate,
                dto.TotalValue,
                dto.FundedValue,
                dto.StandardFullTimeHours,
                dto.Description ?? "",
                "System" // CreatedBy - will be replaced with actual user
            );

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContract), new { id = contract.Id }, MapToDto(contract));
        }

        [HttpPost("{id}/activate")]
        public async Task<IActionResult> ActivateContract(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
                return NotFound();

            try
            {
                contract.Activate();
                await _context.SaveChangesAsync();
                return Ok(MapToDto(contract));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/close")]
        public async Task<IActionResult> CloseContract(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
                return NotFound();

            try
            {
                contract.Close();
                await _context.SaveChangesAsync();
                return Ok(MapToDto(contract));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/update-funding")]
        public async Task<IActionResult> UpdateFunding(Guid id, UpdateFundingDto dto)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
                return NotFound();

            try
            {
                contract.UpdateFunding(dto.NewFundedValue, dto.Justification, "System");
                await _context.SaveChangesAsync();
                return Ok(MapToDto(contract));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("assign-resource")]
        public async Task<IActionResult> AssignResource(AssignResourceDto dto)
        {
            var contract = await _context.Contracts.FindAsync(dto.ContractId);
            if (contract == null)
                return NotFound("Contract not found");

            var resource = await _context.Resources.FindAsync(dto.ResourceId);
            if (resource == null)
                return NotFound("Resource not found");

            try
            {
                contract.AssignResource(
                    dto.ResourceId,
                    dto.AllocationPercentage,
                    dto.StartDate,
                    dto.AnnualHours
                );
                
                await _context.SaveChangesAsync();
                return Ok(new { message = "Resource assigned successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{contractId}/resources/{resourceId}")]
        public async Task<IActionResult> RemoveResource(Guid contractId, Guid resourceId)
        {
            var contract = await _context.Contracts.FindAsync(contractId);
            if (contract == null)
                return NotFound("Contract not found");

            try
            {
                contract.RemoveResource(resourceId, DateTime.UtcNow);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Resource removed successfully" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}/resources")]
        public async Task<ActionResult<IEnumerable<object>>> GetContractResources(Guid id)
        {
            var resources = await _context.ContractResources
                .Include(cr => cr.Resource)
                    .ThenInclude(r => r.LCAT)
                .Where(cr => cr.ContractId == id && cr.IsActive)
                .Select(cr => new
                {
                    cr.Id,
                    cr.ResourceId,
                    ResourceName = cr.Resource.FirstName + " " + cr.Resource.LastName,
                    LCATName = cr.Resource.LCAT != null ? cr.Resource.LCAT.Name : "Unassigned",
                    cr.AllocationPercentage,
                    cr.AnnualHours,
                    cr.StartDate,
                    cr.IsActive,
                    PayRate = cr.Resource.PayRate,
                    BurdenedCost = cr.Resource.BurdenedCost
                })
                .ToListAsync();

            return Ok(resources);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
                return NotFound();

            if (contract.Status == ContractStatus.Active)
                return BadRequest("Cannot delete an active contract");

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private ContractDto MapToDto(Contract contract)
        {
            var burnAnalysis = contract.AnalyzeBurnRate();
            
            return new ContractDto
            {
                Id = contract.Id,
                ContractNumber = contract.ContractNumber,
                CustomerName = contract.CustomerName,
                PrimeContractorName = contract.PrimeContractorName,
                IsPrime = contract.IsPrime,
                Type = contract.Type,
                Status = contract.Status,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                TotalValue = contract.TotalValue,
                FundedValue = contract.FundedValue,
                StandardFullTimeHours = contract.StandardFullTimeHours,
                Description = contract.Description,
                MonthlyBurnRate = burnAnalysis.CurrentMonthlyBurn,
                AnnualBurnRate = burnAnalysis.CurrentAnnualBurn,
                BurnedAmount = contract.GetTotalBurned(),
                WarningLevel = burnAnalysis.WarningLevel,
                CreatedAt = contract.CreatedAt,
                UpdatedAt = contract.UpdatedAt,
                CreatedBy = contract.CreatedBy,
                ModifiedBy = contract.ModifiedBy
            };
        }
    }
}