using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FehDb.API.Models;
using FehDb.API.Models.Resource.WeaponModel;
using FehDb.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FehDb.API.V1.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class WeaponsController : Controller
    {
        private readonly IWeaponService _service;

        public WeaponsController(IWeaponService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets all Weapons
        /// </summary>
        /// <param name="page">The desired page</param>
        /// <param name="pageSize">The desired number of items to return per page</param>
        /// <returns>A list of Weapons</returns>
        /// <response code="200">Returns the list of Weapon</response>
        [HttpGet(Name = "GetAllWeapons")]
        [ProducesResponseType(typeof(PagedResult<WeaponResource>), 200)]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = await _service.GetWeapons(page, pageSize);
            return Ok(result);
        }

        /// <summary>
        /// Get a Weapon by ID
        /// </summary>
        /// <param name="id">The ID for the desired Weapon</param>
        /// <returns>A weapon with a matching ID</returns>
        /// <response code="200">Returns the Weapon</response>
        /// <response code="400">Supplied ID is invalid</response>
        /// <response code="404">No matching entry for given ID</response>
        [HttpGet("{id}", Name = "GetWeapon")]
        [ProducesResponseType(typeof(WeaponResource), 200)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest(new Exception($"Supplied ID is below starting value (Supplied: {id}, Required: id >= 1)."));

            WeaponResource result;

            try
            {
                result = await _service.GetWeaponByID(id);
            }
            catch(Exception e)
            {
                return NotFound(e);
            }

            return Ok(result);
        }

        /// <summary>
        /// Creates a new Weapon
        /// </summary>
        /// <param name="resource">Weapon object to be added</param>
        /// <returns>The newly created Weapon</returns>
        /// <response code="201">Returns the newly-created Weapon</response>
        /// <response code="400">If the item is null</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(WeaponResource), 201)]
        [ProducesResponseType(typeof(void), 400)]
        public async Task<IActionResult> Post([FromBody]WeaponResource resource)
        {
            if (resource == null) return BadRequest("Request body is null.");
            if (!ModelState.IsValid) return BadRequest("Request body is invalid.");

            WeaponResource result;

            try
            {
                result = await _service.Create(resource);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
            
            return CreatedAtRoute("GetWeapon", new { id = result.ID }, result);
        }

        /// <summary>
        /// Updates a previously-created Weapon
        /// </summary>
        /// <param name="id">ID of the Weapon to be updated</param>
        /// <param name="resource">Updated version of a previously-created Weapon</param>
        /// <returns>No content</returns>
        /// <response code="204">Successfully updated weapon</response>
        /// <response code="404">Given ID does not exist</response>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> Put(int id, [FromBody] WeaponResource resource)
        {
            if (id < 1) return BadRequest(new Exception($"Supplied ID is below starting value (Supplied: {id}, Required: id >= 1)."));
            if (resource == null) return BadRequest(new ArgumentNullException("resource", "Supplied WeaponResource is null."));
            if (!ModelState.IsValid) return BadRequest("Request body is invalid.");
            if (id != resource.ID) return BadRequest(new ArgumentNullException("resource.ID", "Supplied WeaponResource.ID is null."));

            try
            {
                await _service.Update(id, resource);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a Weapon
        /// </summary>
        /// <param name="id">The ID of the Weapon to be deleted</param>
        /// <returns>No content</returns>
        /// <response code="200">Successfully deleted Weapon</response>
        /// <response code="404">Given ID does not match a Weapon</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest(new Exception($"Supplied ID is below starting value (Supplied: {id}, Required: id >= 1)."));

            try
            {
                await _service.Delete(id);
            }
            catch(Exception e)
            {
                return BadRequest(e);
            }

            return NoContent();
        }
    }
}
