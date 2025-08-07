// ContractTracker.Api/Controllers/ContractController.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContractTracker.Api.DTOs;
using ContractTracker.Domain.Entities;
using ContractTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        // GET: api/Contract
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContractDto>>> GetContracts()
        {
            var contracts = await _context.Contracts.ToListAsync();

            var contractDtos = contracts.Select(c => new ContractDto
            {
                Id = c.Id,
                ContractNumber = c.ContractNumber,
                ContractName = c.ContractName,
                CustomerName = c.CustomerName,
                PrimeContractor = c.PrimeContractor,
                IsPrime = c.IsPrime,
                ContractType = c.ContractType.ToString(),
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                TotalValue = c.TotalValue,
                FundedValue = c.FundedValue,
                StandardFullTimeHours = c.StandardFullTimeHours,
                Description = c.Description,
                Status = c.Status.ToString(),
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                IsActive = c.Status == ContractStatus.Active
            }).ToList();

            return Ok(contractDtos);
        }

        // GET: api/Contract/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ContractDto>> GetContract(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
            {
                return NotFound($"Contract with ID {id} not found");
            }

            var contractDto = new ContractDto
            {
                Id = contract.Id,
                ContractNumber = contract.ContractNumber,
                ContractName = contract.ContractName,
                CustomerName = contract.CustomerName,
                PrimeContractor = contract.PrimeContractor,
                IsPrime = contract.IsPrime,
                ContractType = contract.ContractType.ToString(),
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                TotalValue = contract.TotalValue,
                FundedValue = contract.FundedValue,
                StandardFullTimeHours = contract.StandardFullTimeHours,
                Description = contract.Description,
                Status = contract.Status.ToString(),
                CreatedAt = contract.CreatedAt,
                UpdatedAt = contract.UpdatedAt,
                IsActive = contract.Status == ContractStatus.Active
            };

            return Ok(contractDto);
        }

        // POST: api/Contract
        [HttpPost]
        public async Task<ActionResult<ContractDto>> CreateContract(CreateContractDto dto)
        {
            // Check if contract number already exists
            var existingContract = await _context.Contracts
                .FirstOrDefaultAsync(c => c.ContractNumber == dto.ContractNumber);
            
            if (existingContract != null)
            {
                return Conflict($"Contract with number {dto.ContractNumber} already exists");
            }

            // Parse contract type
            if (!Enum.TryParse<ContractType>(dto.ContractType, true, out var contractType))
            {
                return BadRequest($"Invalid contract type: {dto.ContractType}");
            }

            try
            {
                var contract = new Contract(
                    dto.ContractNumber,
                    dto.ContractName,
                    dto.CustomerName,
                    dto.PrimeContractor,
                    dto.IsPrime,
                    contractType,
                    dto.StartDate,
                    dto.EndDate,
                    dto.TotalValue,
                    dto.Description
                );

                // Set initial funding if provided
                if (dto.FundedValue > 0)
                {
                    contract.UpdateFunding(dto.FundedValue, "Initial", "Initial funding");
                }

                _context.Contracts.Add(contract);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetContract), new { id = contract.Id }, new ContractDto
                {
                    Id = contract.Id,
                    ContractNumber = contract.ContractNumber,
                    ContractName = contract.ContractName,
                    CustomerName = contract.CustomerName,
                    PrimeContractor = contract.PrimeContractor,
                    IsPrime = contract.IsPrime,
                    ContractType = contract.ContractType.ToString(),
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    TotalValue = contract.TotalValue,
                    FundedValue = contract.FundedValue,
                    StandardFullTimeHours = contract.StandardFullTimeHours,
                    Description = contract.Description,
                    Status = contract.Status.ToString(),
                    CreatedAt = contract.CreatedAt,
                    UpdatedAt = contract.UpdatedAt
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Contract/{id}/activate
        [HttpPost("{id}/activate")]
        public async Task<IActionResult> ActivateContract(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound($"Contract with ID {id} not found");
            }

            try
            {
                contract.Activate();
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Contract/{id}/close
        [HttpPost("{id}/close")]
        public async Task<IActionResult> CloseContract(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound($"Contract with ID {id} not found");
            }

            try
            {
                contract.Close();
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Contract/{id}/update-funding
        [HttpPost("{id}/update-funding")]
        public async Task<IActionResult> UpdateFunding(Guid id, UpdateFundingDto dto)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null)
            {
                return NotFound($"Contract with ID {id} not found");
            }

            try
            {
                contract.UpdateFunding(dto.FundedAmount, dto.ModificationNumber, dto.Justification);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Contract/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContract(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);
                
            if (contract == null)
            {
                return NotFound($"Contract with ID {id} not found");
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}