using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MeetSport.Dbo;
using MeetSport.Repositories;
using MeetSport.Business;
using MeetSport.Dto.Roles;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using MeetSport.Services.Authorizations;

namespace MeetSport.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IBusiness<Role> _business;

        public RolesController(IBusiness<Role> business)
        {
            _business = business;
        }

        /// <summary>
        /// Delete a Role by Id
        /// </summary>
        /// <remarks>
        /// You must be an admin to use it
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="204">Returns no content</response>
        [HttpDelete("{id}")]
        [Authorize(ClaimNames.RoleWrite)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<bool>> DeleteRole(ulong id)
        {
            await _business.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Get a Role by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Role with given Id</returns>
        /// <response code="200">Returns the Role with given Id</response>
        /// <response code="404">If the Role does not exist</response> 
        [HttpGet("{id}")]
        [Authorize(ClaimNames.RoleRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDto>> GetRole(ulong id)
        {
            RoleDto roleDto = await _business.Get<RoleDto>(id);

            if (roleDto == null)
            {
                return NotFound($"A role with id \"{id}\" was not found.");
            }

            return Ok(roleDto);
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns>All roles</returns>
        /// <response code="200">Returns all roles</response>        
        [HttpGet]
        [Authorize(ClaimNames.RoleRead)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<RoleDto>>> GetRoles()
        {
            ICollection<RoleDto> roles = await _business.GetAll<RoleDto>();
            return Ok(roles);
        }

        /// <summary>
        /// Create a Role
        /// </summary>
        /// <remarks>
        /// You must be an admin to use it
        /// </remarks>
        /// <param name="createRoleDto"></param>
        /// <returns>Created Role</returns>
        /// <response code="201">Returns the Created Role</response>
        [HttpPost]
        [Authorize(ClaimNames.RoleWrite)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> PostRole(CreateRoleDto createRoleDto)
        {
            RoleDto roleDto = await _business.Add<RoleDto, CreateRoleDto>(createRoleDto);
            return Created(roleDto.Id.ToString(), roleDto);
        }

        /// <summary>
        /// Update a Role by Id
        /// </summary>
        /// <remarks>
        /// You must be an admin to use it
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="updateRoleDto"></param>
        /// <returns>Updated Role with given Id</returns>
        /// <response code="200">Returns the Updated Role with given Id</response>
        /// <response code="400">If the Role does not exist</response>
        [HttpPut("{id}")]
        [Authorize(ClaimNames.RoleWrite)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutRole(ulong id, UpdateRoleDto updateRoleDto)
        {
            try
            {
                RoleDto roleDto = await _business.Update<RoleDto, UpdateRoleDto>(updateRoleDto, id);
                return Ok(roleDto);
            }
            catch (ArgumentNullException)
            {
                return BadRequest($"A role with id \"{id}\" was not found.");
            }
        }
    }
}
