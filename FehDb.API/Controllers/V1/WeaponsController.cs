using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FehDb.API.Models;
using FehDb.API.Models.Binding;
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

        #region Weapons
        /// <summary>
        /// Gets all Weapons
        /// </summary>
        /// <param name="query">The query parameters</param>
        /// <param name="filter">The filter parameters</param>
        /// <returns>A list of Weapons</returns>
        /// <response code="200">Returns the list of Weapon</response>
        [HttpGet(Name = "GetAllWeapons")]
        [ProducesResponseType(typeof(PagedResult<WeaponResource>), 200)]
        public IActionResult GetAll([FromQuery] Query query, [FromQuery] WeaponFilter filter)
        {
            var result = _service.GetWeapons(query, filter);
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
        public IActionResult GetByID(int id)
        {
            if (id < 1) return BadRequest(new Exception($"Supplied ID is below starting value (Supplied: {id}, Required: id >= 1)."));

            WeaponResource result;

            try
            {
                result = _service.GetWeaponByID(id);
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
        /// <response code="201">Weapon successfully added</response>
        /// <response code="400">If the item is null</response>
        /// <response code="403">Supplied JWT token invalid</response>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(WeaponResource), 201)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 403)]
        public async Task<IActionResult> Create([FromBody]WeaponResource resource)
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
        /// Creates a range of new Weapons
        /// </summary>
        /// <param name="resource">List of weapon objects to be added</param>
        /// <returns>A list of weapons</returns>
        /// <response code="201">Weapons successfully added</response>
        /// <response code="400">If the item is null</response>
        /// <response code="403">Supplied JWT token invalid</response>
        [Authorize]
        [HttpPost("List")]
        [ProducesResponseType(typeof(PagedResult<WeaponResource>), 201)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 403)]
        public async Task<IActionResult> CreateFromList([FromBody]IEnumerable<WeaponResource> resource)
        {
            if (resource == null) return BadRequest("Request body is null.");
            if (!ModelState.IsValid) return BadRequest("Request body is invalid.");
            
            try
            {
                await _service.CreateFromList(resource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return CreatedAtRoute("GetAllWeapons", new { });
        }

        /// <summary>
        /// Updates a previously-created Weapon
        /// </summary>
        /// <param name="id">ID of the Weapon to be updated</param>
        /// <param name="resource">Updated version of a previously-created Weapon</param>
        /// <returns>No content</returns>
        /// <response code="204">Successfully updated weapon</response>
        /// <response code="403">Supplied JWT token invalid</response>
        /// <response code="404">Given ID does not exist</response>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> Update(int id, [FromBody] WeaponResource resource)
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
        /// <response code="403">Supplied JWT token invalid</response>
        /// <response code="404">Given ID does not match a Weapon</response>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 403)]
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
        #endregion

        #region Weapon Upgrades
        /// <summary>
        /// Gets all Weapon Upgrades
        /// </summary>
        /// <param name="id">The weapon id</param>
        /// <returns>A list of Weapon Upgrades for given weapon id</returns>
        /// <response code="200">Returns the list of Weapons</response>
        [HttpGet("{id}/Upgrades", Name = "GetAllWeaponUpgrades")]
        [ProducesResponseType(typeof(List<WeaponResource>), 200)]
        public IActionResult GetAllWeaponUpgrades(int id)
        {
            List<WeaponResource> result;

            try
            {
                result = _service.GetWeaponUpgrades(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok(result);
        }

        /// <summary>
        /// Creates a new Weapon Upgrade
        /// </summary>
        /// <param name="resource">Weapon Upgrade object to be added</param>
        /// <returns>The list of weapons upgraded from the Base Weapon ID</returns>
        /// <response code="201">Returns the list of weapons upgraded from the Base Weapon ID</response>
        /// <response code="400">If the item is null</response>
        /// <response code="403">Supplied JWT token invalid</response>
        [Authorize]
        [HttpPost("Upgrades")]
        [ProducesResponseType(typeof(WeaponUpgradeResource), 201)]
        [ProducesResponseType(typeof(void), 400)]
        [ProducesResponseType(typeof(void), 403)]
        public async Task<IActionResult> CreateWeaponUpgrades([FromBody]WeaponUpgradeResource resource)
        {
            if (resource == null) return BadRequest("Request body is null.");
            if (!ModelState.IsValid) return BadRequest("Request body is invalid.");

            WeaponUpgradeResource result;

            try
            {
                result = await _service.CreateWeaponUpgrades(resource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return CreatedAtRoute("GetAllWeaponUpgrades", new { id = result.PreviousWeaponID }, result);
        }

        /// <summary>
        /// Updates a previously-created Weapon Upgrade
        /// </summary>
        /// <param name="id">ID of the Weapon Upgrade to be updated</param>
        /// <param name="resource">Updated version of a previously-created Weapon Upgrade</param>
        /// <returns>No content</returns>
        /// <response code="204">Successfully updated weapon upgrade</response>
        /// <response code="403">Supplied JWT token invalid</response>
        /// <response code="404">Given ID does not exist</response>
        [Authorize]
        [HttpPut("Upgrades/{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> UpdateWeaponUpgrades(int id, [FromBody] WeaponUpgradeResource resource)
        {
            if (id < 1) return BadRequest(new Exception($"Supplied ID is below starting value (Supplied: {id}, Required: id >= 1)."));
            if (resource == null) return BadRequest(new ArgumentNullException("resource", "Supplied WeaponResource is null."));
            if (!ModelState.IsValid) return BadRequest(new Exception("Request body is invalid."));
            if (id != resource.ID) return BadRequest(new Exception($"resource.ID does not equal id. resource.ID: {resource.ID}, id: {id}"));

            try
            {
                await _service.UpdateWeaponUpgrades(id, resource);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a Weapon Upgrade
        /// </summary>
        /// <param name="id">The ID of the Weapon Upgrade to be deleted</param>
        /// <returns>No content</returns>
        /// <response code="200">Successfully deleted Weapon Upgrade</response>
        /// <response code="403">Supplied JWT token invalid</response>
        /// <response code="404">Given ID does not match a Weapon Upgrade</response>
        [Authorize]
        [HttpDelete("Upgrades/{id}")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> DeleteWeaponUpgrades(int id)
        {
            if (id < 1) return BadRequest(new Exception($"Supplied ID is below starting value (Supplied: {id}, Required: id >= 1)."));

            try
            {
                await _service.DeleteWeaponUpgrades(id);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes a Weapon Upgrade
        /// </summary>
        /// <param name="weaponUpgrade">The resource defining which weapon upgrade previous/next pair to delete</param>
        /// <returns>No content</returns>
        /// <response code="200">Successfully deleted Weapon Upgrade</response>
        /// <response code="403">Supplied JWT token invalid</response>
        /// <response code="404">Given weaponUpgrade does not map to a Weapon Upgrade</response>
        [Authorize]
        [HttpDelete("Upgrades")]
        [ProducesResponseType(typeof(void), 204)]
        [ProducesResponseType(typeof(void), 403)]
        [ProducesResponseType(typeof(void), 404)]
        public async Task<IActionResult> DeleteWeaponUpgradeByWeapons([FromBody] WeaponUpgradeResource weaponUpgrade)
        {
            if (weaponUpgrade == null) return BadRequest(new ArgumentNullException("Supplied weaponUpgrade is null."));

            try
            {
                await _service.DeleteWeaponUpgradesByWeapon(weaponUpgrade);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return NoContent();
        }
        #endregion
    }
}
