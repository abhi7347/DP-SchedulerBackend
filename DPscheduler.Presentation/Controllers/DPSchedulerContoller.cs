using Azure.Core;
using DPScheduler.BAL.Interface;
using DPScheduler.DAL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DPscheduler.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DPSchedulerContoller : ControllerBase
    {
        private readonly IService _service;
        private readonly ILogger _logger;
        public DPSchedulerContoller(IService service, ILogger<DPSchedulerContoller> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("locations")]
        public async Task<IActionResult> GetActionResult() {
            try
            {
                var locations = await _service.GetAllLocations();
                _logger.LogInformation("Locations retrieved successfully and returned to the client.");
                return Ok(locations);
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "An error occurred while fetching locations in the controller.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
           
        }

        [HttpGet("ProvidersByLocation")]
        public async Task<IActionResult> ProvidersByLocations([FromQuery] ProvidersByLocations request)
        {
            try
            {
                if (request == null || !request.LocationIds.Any())
                {
                    return BadRequest("Invalid request");
                }
                else
                {
                    var providers = await _service.GetProvidersByLocations(request.DayOfWeek, request.LocationIds);

                    _logger.LogInformation("Providers By Location and returned to the client.");

                    return Ok(providers);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching Providers By Location in the controller.");

                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("ProvidersInToggleLoacion")]
        public async Task<IActionResult> ProvidersByToggelLocations(string dayOfWeek)
        {
            try
            {
                var providers = await _service.ProvidersByToggelLocations(dayOfWeek);

                if (providers == null)
                {
                    return NotFound("No providers found for the given location.");
                }
                _logger.LogInformation("ProvidersInToggleLoacion and returned to the client.");

                return Ok(providers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching ProvidersInToggleLoacion in the controller.");

                return StatusCode(500, "Internal server error");
            }
        }

        



    }
}
