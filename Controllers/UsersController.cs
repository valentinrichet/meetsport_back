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
using MeetSport.Services.JwtGenerator;
using Microsoft.Extensions.Logging;

namespace MeetSport.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : AuthorizedController
    {
        private readonly IUserBusiness<User> _business;

        public UsersController(IUserBusiness<User> business, ILogger<UsersController> logger) : base(logger)
        {
            _business = business;
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="authenticationUserDto"></param>
        /// <returns>JWT Token</returns>
        /// <response code="200">Returns the JWT Token</response>
        /// <response code="401">If authentication failed</response> 
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Authenticate(AuthenticateUserDto authenticationUserDto)
        {
            try
            {
                string token = await _business.Authenticate(authenticationUserDto);
                return Ok(token);
            }
            catch (AuthenticationFailedException exception)
            {
                return Unauthorized(exception.Message);
            }
        }

        /// <summary>
        /// Delete a User by Id
        /// </summary>
        /// <remarks>
        /// You must be an admin to use it or you must be the user
        /// </remarks>
        /// <param name="id"></param>
        /// <response code="204">Returns no content</response>
        /// <response code="401">If unauthorized</response> 
        [Authorize(ClaimNames.UserWrite)]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<bool>> DeleteUser(ulong id)
        {
            if (!IsAdmin() && !IsUser(id))
            {
                return Unauthorized("You can not delete this user.");
            }

            await _business.Delete(id);
            return NoContent();
        }

        /// <summary>
        /// Get a User by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User with given Id</returns>
        /// <response code="200">Returns the User with given Id</response>
        /// <response code="404">If the User does not exist</response> 
        [Authorize(ClaimNames.UserRead)]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUser(ulong id)
        {
            UserDto userDto = await _business.GetFirstOrDefault<UserDto>((user => user.Id == id));

            if (userDto == null)
            {
                return NotFound($"A user with id \"{id}\" was not found.");
            }

            return Ok(userDto);
        }

        /// <summary>
        /// Create a User
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns>Created Role</returns>
        /// <response code="201">Returns the Created Role</response>
        /// <response code="400">If a user with the same mail already exists</response>
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> PostUser(CreateUserDto createUserDto)
        {
            try
            {
                UserDto userDto = await _business.CreateUser<UserDto>(createUserDto);
                return Created(userDto.Id.ToString(), userDto);
            } catch(DbUpdateException)
            {
                return BadRequest($"A user with the mail \"{createUserDto.Mail}\" already exists.");
            }
        }

        /// <summary>
        /// Update a User by Id
        /// </summary>
        /// <remarks>
        /// You must be an admin to use it or you must be the user
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="updateUserDto"></param>
        /// <returns>Updated User with given Id</returns>
        /// <response code="200">Returns the Updated User with given Id</response>
        /// <response code="400">If the User does not exist</response>
        /// <response code="401">If unauthorized</response>
        [Authorize(ClaimNames.UserWrite)]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutUser(ulong id, UpdateUserDto updateUserDto)
        {
            if (!IsAdmin() && !IsUser(id))
            {
                return Unauthorized("You can not update this user.");
            }

            try
            {
                UserDto userDto = await _business.UpdateUser<UserDto>(updateUserDto, id);
                return Ok(userDto);
            }
            catch (ArgumentNullException)
            {
                return BadRequest($"A user with id \"{id}\" was not found.");
            }
        }
    }
}
