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
        public DPSchedulerContoller(IService service)
        {
            _service = service;
        }
        [HttpGet("locations")]
        public async Task<IActionResult> GetActionResult() {

            var locations = await _service.GetAllLocations();
            return Ok(locations);
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
                    return Ok(providers);
                }
            }
            catch (Exception ex)
            {
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

                return Ok(providers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("bookedAppointments")]
        public async Task<IActionResult> GetBookedAppointments([FromQuery] DateTime selectedDate, [FromQuery] IEnumerable<int> LocationIds)
        {
            try
            {
                if (selectedDate == default(DateTime) || LocationIds == null || !LocationIds.Any())
                {
                    return BadRequest("Invalid request parameters.");
                }

                var bookedAppointments = await _service.GetBookedAppointments(selectedDate, LocationIds);

                if (bookedAppointments == null)
                {
                    return NotFound("No appointments found.");
                }

                return Ok(bookedAppointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }



    }
}
