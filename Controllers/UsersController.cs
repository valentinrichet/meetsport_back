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
using MeetSport.Business.Users;
using MeetSport.Dto.Users;
using Microsoft.AspNetCore.Authorization;
using MeetSport.Services.Authorizations;
using MeetSport.Exceptions;

namespace MeetSport.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserBusiness<User> _business;

        public UsersController(IUserBusiness<User> business)
        {
            _business = business;
        }

        [Authorize(ClaimNames.RoleRead)]
        [HttpPost("authenticatetest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> AuthenticateTest()
        {
            //string token = await _business.Authenticate(userAuthenticationDto);
            return Ok("valid");
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userAuthenticationDto"></param>
        /// <returns>JWT Token</returns>
        /// <response code="200">Returns the JWT Token</response>
        /// <response code="401">If authentication failed</response> 
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Authenticate(UserAuthenticationDto userAuthenticationDto)
        {
            try
            {
                string token = await _business.Authenticate(userAuthenticationDto);
                return Ok(token);
            }
            catch (AuthenticationFailedException exception)
            {
                return Unauthorized(exception.Message);
            }
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDto>> GetRole(ulong id)
        {
            RoleDto roleDto = await _business.Get<RoleDto>(id);

            if (roleDto == null)
            {
                return NotFound($"A user with id \"{id}\" was not found.");
            }

            return Ok(roleDto);
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns>All roles</returns>
        /// <response code="200">Returns all roles</response>        
        [HttpGet]
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
