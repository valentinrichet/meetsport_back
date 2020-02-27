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

namespace MeetSport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IBusiness<Role> _business;

        public RolesController(IBusiness<Role> business)
        {
            _business = business;
        }

        // GET: api/Roles
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ICollection<RoleDto>>> GetRoles()
        {
            ICollection<RoleDto> roles = await _business.GetAll<RoleDto>();
            return Ok(roles);
        }

        // GET: api/Roles/5
        [HttpGet("{id}")]
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

        // PUT: api/Roles/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

        // POST: api/Roles
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult> PostRole(CreateRoleDto createRoleDto)
        {
            RoleDto roleDto = await _business.Add<RoleDto, CreateRoleDto>(createRoleDto);
            return Created(roleDto.Id.ToString(), roleDto);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<bool>> DeleteRole(ulong id)
        {
            await _business.Delete(id);
            return NoContent();
        }
    }
}
