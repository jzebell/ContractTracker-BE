using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ContractTracker.Application.Services;

namespace ContractTracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardAnalyticsService _dashboardService;

        public DashboardController(IDashboardAnalyticsService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Get overall dashboard metrics
        /// </summary>
        [HttpGet("metrics")]
        public async Task<ActionResult<DashboardMetrics>> GetMetrics()
        {
            try
            {
                var metrics = await _dashboardService.GetDashboardMetricsAsync();
                return Ok(metrics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to load dashboard metrics", details = ex.Message });
            }
        }

        /// <summary>
        /// Get contract health cards for dashboard display
        /// </summary>
        [HttpGet("contracts/health")]
        public async Task<ActionResult<List<ContractHealthCard>>> GetContractHealth()
        {
            try
            {
                var healthCards = await _dashboardService.GetContractHealthCardsAsync();
                return Ok(healthCards);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to load contract health data", details = ex.Message });
            }
        }

        /// <summary>
        /// Get resource utilization metrics
        /// </summary>
        [HttpGet("resources/utilization")]
        public async Task<ActionResult<ResourceUtilizationMetrics>> GetResourceUtilization()
        {
            try
            {
                var utilization = await _dashboardService.GetResourceUtilizationAsync();
                return Ok(utilization);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to load resource utilization", details = ex.Message });
            }
        }

        /// <summary>
        /// Get financial projections
        /// </summary>
        [HttpGet("projections")]
        public async Task<ActionResult<FinancialProjections>> GetProjections([FromQuery] int months = 12)
        {
            try
            {
                if (months < 1 || months > 36)
                {
                    return BadRequest(new { error = "Projection months must be between 1 and 36" });
                }

                var projections = await _dashboardService.GetFinancialProjectionsAsync(months);
                return Ok(projections);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to generate projections", details = ex.Message });
            }
        }

        /// <summary>
        /// Get critical alerts and notifications
        /// </summary>
        [HttpGet("alerts")]
        public async Task<ActionResult<List<AlertNotification>>> GetAlerts()
        {
            try
            {
                var alerts = await _dashboardService.GetCriticalAlertsAsync();
                return Ok(alerts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to load alerts", details = ex.Message });
            }
        }

        /// <summary>
        /// Get complete dashboard data in one call
        /// </summary>
        [HttpGet("complete")]
        public async Task<ActionResult<object>> GetCompleteDashboard()
        {
            try
            {
                var metrics = await _dashboardService.GetDashboardMetricsAsync();
                var contracts = await _dashboardService.GetContractHealthCardsAsync();
                var resources = await _dashboardService.GetResourceUtilizationAsync();
                var alerts = await _dashboardService.GetCriticalAlertsAsync();

                return Ok(new
                {
                    metrics,
                    contracts,
                    resources,
                    alerts,
                    timestamp = DateTime.UtcNow
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Failed to load dashboard", details = ex.Message });
            }
        }
    }
}