using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OutbreakApi.Data;
using OutbreakModels.Models;

namespace OutbreakApi.Controllers
{
    [Route("api/BaseAbilities")]
    [ApiController]
    public class BaseAbilityController : ControllerBase
    {
        private readonly IBaseAbilityRepository _baseAbilityRepository;

        public BaseAbilityController(IBaseAbilityRepository baseAbilityRepository)
        {
            _baseAbilityRepository = baseAbilityRepository;
        }

        //Search
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<BaseAbility>>> Search(string search)
        {
            try
            {
                var result = await _baseAbilityRepository.SearchBaseAbilities(search);

                if (result.Any())
                {
                    return Ok(result);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Get All
        [HttpGet]
        public async Task<ActionResult> GetBaseAbilities()
        {
            try
            {
                return Ok(await _baseAbilityRepository.GetBaseAbilities());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Get By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseAbility>> GetBaseAbility(int id)
        {
            try
            {
                var result = await _baseAbilityRepository.GetBaseAbility(id);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound("Id/Ability Mismatch");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Create New
        [HttpPost]
        public async Task<ActionResult<BaseAbility>> CreateBaseAbility(BaseAbility baseAbility)
        {
            try
            {
                if (baseAbility != null)
                {
                    var newBaseAbility = await _baseAbilityRepository.AddBaseAbility(baseAbility);

                    return CreatedAtAction(nameof(GetBaseAbility), new { id = newBaseAbility.Id }, newBaseAbility);
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error posting data to the database.");
            }
        }

        //Update by Put
        [HttpPut("{id:int}")]
        public async Task<ActionResult<BaseAbility>> UpdateBaseAbility(int id, BaseAbility baseAbility)
        {
            try
            {
                if (id != baseAbility.Id)
                {
                    return BadRequest($"Ability ID mismatch. Specified ID = {id} | Ability ID = {baseAbility.Id}");
                }

                var baseAbilityToUpdate = await _baseAbilityRepository.GetBaseAbility(id);

                if (baseAbilityToUpdate == null)
                {
                    return NotFound($"Could not find Ability with ID = {id}.");
                }

                return await _baseAbilityRepository.UpdateBaseAbility(baseAbility);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BaseAbility>> DeleteBaseAbility(int id, BaseAbility baseAbility)
        {
            try
            {
                var baseAbilityToDelete = await _baseAbilityRepository.GetBaseAbility(id);

                if (baseAbilityToDelete == null)
                {
                    return BadRequest($"Could not find Ability with ID = {id}.");
                }

                return await _baseAbilityRepository.DeleteBaseAbility(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
            }
        }
    }

}
