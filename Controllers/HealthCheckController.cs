namespace Cars.Reservation.Api.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Diagnostics.HealthChecks;
    using Microsoft.Extensions.Logging;

    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly ILogger<HealthCheckController> _logger;
        private readonly HealthCheckService _healthCheckService;

        #region .ctor
        public HealthCheckController(
            HealthCheckService healthCheckService,
            ILogger<HealthCheckController> logger)
        {
            _healthCheckService = healthCheckService;
            _logger = logger;
        }

        #endregion

        #region Get: /hc

        /// <summary>
        /// Предоставляет информацию о работоспособности API.
        /// </summary>
        /// <response code="200">API находится в работоспособном состоянии.</response>
        /// <response code="503">API находится в неработоспособном состоянии.</response>
        [HttpGet]
        [Route("hc")]
        public async Task<IActionResult> HealthCheck()
        {
            _logger.LogInformation($"[HEALTHCHECK] Start health check...");
            _logger.LogInformation($"[HEALTHCHECK] Start get health report...");

            var report = await _healthCheckService
                .CheckHealthAsync(_ => true, HttpContext.RequestAborted);

            _logger.LogInformation($"[HEALTHCHECK] Get health report completed successfully.");
            _logger.LogInformation($"[HEALTHCHECK] Health check completed successfully.");

            return report.Status == HealthStatus.Healthy
                ? Ok(report)
                : StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
        }

        #endregion

        #region Get: /liveness

        /// <summary>
        /// Предоставляет информацию о работоспособности API.
        /// </summary>
        /// <response code="200">API находится в работоспособном состоянии.</response>
        /// <response code="503">API находится в неработоспособном состоянии.</response>
        [HttpGet]
        [Route("liveness")]
        public async Task<IActionResult> Liveness()
        {
            _logger.LogInformation($"[HEALTHCHECK] Start liveness...");
            _logger.LogInformation($"[HEALTHCHECK] Start get health report...");

            var report = await _healthCheckService
                .CheckHealthAsync(x => x.Name.Contains("self"), HttpContext.RequestAborted);

            _logger.LogInformation($"[HEALTHCHECK] Get health report completed successfully.");
            _logger.LogInformation($"[HEALTHCHECK] Liveness completed successfully.");

            return report.Status == HealthStatus.Healthy
                ? Ok(Enum.GetName(typeof(HealthStatus), report.Status))
                : StatusCode((int)HttpStatusCode.ServiceUnavailable, Enum.GetName(typeof(HealthStatus), report.Status));
        }

        #endregion
    }
}
