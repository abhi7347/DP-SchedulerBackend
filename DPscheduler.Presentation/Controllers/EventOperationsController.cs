using DPScheduler.BAL.Implementation;
using DPScheduler.BAL.Interface;
using DPScheduler.DAL.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;

namespace DPscheduler.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventOperationsController : ControllerBase
    {
        private readonly IEventOperationsService _eventServiceOps;
        private readonly ILogger _logger;
        public EventOperationsController(IEventOperationsService eventServiceOps, ILogger<EventOperationsController> logger)
        {
            _eventServiceOps = eventServiceOps;
            this._logger = logger;
        }

        [HttpPost("CreateEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] EventDTO EventModel)
        {
            if (EventModel == null)
            {
                return BadRequest("Event model is null.");
            }

            try
            {

                await _eventServiceOps.CreateEvent(EventModel);

                _logger.LogInformation("successfull while Creating an Event in controller.");

                return Ok(new { message = "Event created successfully." });

            }
            catch (Exception ex)
            {
                _logger.LogError("Error while Creating an Event to in controller");

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete Event Method
        [HttpDelete("DeleteEvent/{eventId}")]
        public async Task<IActionResult> DeleteEvent(string eventId)
        {
            if (eventId == null)
            {
                return BadRequest("Event ID is required.");
            }

            try
            {
                await _eventServiceOps.DeleteEvent(eventId);

                _logger.LogInformation($" Successfully Deleted {eventId} in controller");

                return Ok(true);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while Deleting Event {eventId} in controller");

                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        //Update Event Controller
        [HttpPut("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventDTO eventModel)
        {
            if (eventModel == null)
            {
                return BadRequest("Event model is null.");
            }

            try
            {
                await _eventServiceOps.UpdateEvent(eventModel);

                _logger.LogInformation($" Successfully Updated the event to client");

                return Ok( new { success = "Event updated successfully." });

            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while updating the event to client: {ex}");

                return StatusCode(500, $"Internal server error: {ex.Message}");
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

                var bookedAppointments = await _eventServiceOps.GetBookedAppointments(selectedDate, LocationIds);

                if (bookedAppointments == null)
                {
                    return NotFound("No appointments found.");
                }
                _logger.LogInformation($" Successfully Fetched the Booked Appointments to client");
                return Ok(bookedAppointments);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred while fetching Booking Appointments to client : {ex}");

                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
