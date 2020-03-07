using MeetSport.Business.Events;
using MeetSport.Dbo;
using MeetSport.Dto.Events;
using MeetSport.Exceptions;
using MeetSport.Services.Authorizations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : AuthorizedController
    {
        private readonly IEventBusiness<Event> _business;

        public EventsController(IEventBusiness<Event> business, ILogger<UsersController> logger) : base(logger)
        {
            _business = business;
        }

        /// <summary>
        /// Delete an Event by Id
        /// </summary>
        /// <remarks>
        /// You must be an admin to use it or the creator
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="401">If unauthorized</response>
        [HttpDelete("{id}")]
        [Authorize(ClaimNames.EventWrite)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<bool>> DeleteEvent(ulong id)
        {
            try
            {
                await _business.Delete(id, (eventEntity => IsAdmin() || eventEntity.Creator == Convert.ToUInt64(UserId)));
            }
            catch (ConditionFailedException)
            {
                return Unauthorized("You can not delete this event.");
            }

            return NoContent();
        }

        /// <summary>
        /// Get an Event by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Role with given Id</returns>
        /// <response code="200">Returns the Event with given Id</response>
        /// <response code="404">If the Event does not exist</response> 
        [HttpGet("{id}")]
        [Authorize(ClaimNames.EventRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<EventDto>> GetEvent(ulong id)
        {
            EventDto eventDto = await _business.GetFirstOrDefault<EventDto>((eventEntity => eventEntity.Id == id));

            if (eventDto == null)
            {
                return NotFound($"An event with id \"{id}\" was not found.");
            }

            return Ok(eventDto);
        }

        /// <summary>
        /// Get All Events
        /// </summary>
        /// <returns>All Events</returns>
        /// <response code="200">Returns all events</response>        
        [HttpGet]
        [Authorize(ClaimNames.EventRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<EventDto>>> GetEvents()
        {
            ICollection<EventDto> events = await _business.GetAll<EventDto>();
            return Ok(events);
        }

        /// <summary>
        /// Create an Event
        /// </summary>
        /// <param name="createEventDto"></param>
        /// <returns>Created Event</returns>
        /// <response code="201">Returns the Created Event</response>
        /// <response code="400">If the creator does not exist</response>
        /// <response code="401">If unauthorized</response>
        [HttpPost]
        [Authorize(ClaimNames.EventWrite)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> PostEvent(CreateEventDto createEventDto)
        {
            if (!IsAdmin() && createEventDto.Creator != Convert.ToUInt64(UserId))
            {
                return Unauthorized("You can not create an event for another user.");
            }

            try
            {
                EventDto eventDto = await _business.CreateEvent<EventDto>(createEventDto);
                return Created(eventDto.Id.ToString(), eventDto);
            }
            catch (DbUpdateException)
            {
                return BadRequest($"The creator does not exist.");
            }

        }

        /// <summary>
        /// Update an Event by Id
        /// </summary>
        /// <remarks>
        /// You must be an admin to use it or the creator
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="updateEventDto"></param>
        /// <returns>Updated Event with given Id</returns>
        /// <response code="200">Returns the Updated Event with given Id</response>
        /// <response code="400">If the Event does not exist</response>
        /// <response code="401">If unauthorized</response>
        [HttpPut("{id}")]
        [Authorize(ClaimNames.RoleWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutEvent(ulong id, UpdateEventDto updateEventDto)
        {
            if (!IsAdmin() && updateEventDto.Creator != Convert.ToUInt64(UserId))
            {
                return Unauthorized("You can not update an event for another user.");
            }

            try
            {
                EventDto eventDto = await _business.Update<EventDto, UpdateEventDto>(id, updateEventDto, (eventEntity => IsAdmin() || eventEntity.Creator == Convert.ToUInt64(UserId)));
                return Ok(eventDto);
            }
            catch (ArgumentNullException)
            {
                return BadRequest($"An event with id \"{id}\" was not found.");
            }
            catch (ConditionFailedException)
            {
                return Unauthorized("You can not update an event for another user.");
            }
        }
    }
}
