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
        public EventOperationsController(IEventOperationsService eventServiceOps)
        {
            _eventServiceOps = eventServiceOps;
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
                DateTime eventDateUtc = DateTime.SpecifyKind(EventModel.EventDate, DateTimeKind.Utc);

                EventModel.EventDate = eventDateUtc;

                await _eventServiceOps.CreateEvent(EventModel);

                return Ok(new { message = "Event created successfully." });
            }
            catch (Exception ex)
            {
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
                return Ok(true);
            }
            catch (Exception ex)
            {
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
                return Ok( new { success = "Event updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
