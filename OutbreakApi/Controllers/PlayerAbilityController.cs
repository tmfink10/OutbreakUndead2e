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
    [Route("api/PlayerAbilities")]
    [ApiController]
    public class PlayerAbilityController : ControllerBase
    {
        private readonly IPlayerAbilityRepository _playerAbilityRepository;

        public PlayerAbilityController(IPlayerAbilityRepository playerAbilityRepository)
        {
            _playerAbilityRepository = playerAbilityRepository;
        }

        //Search
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<PlayerAbility>>> Search(string search)
        {
            try
            {
                var result = await _playerAbilityRepository.SearchPlayerAbilities(search);

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
        public async Task<ActionResult> GetPlayerAbilities()
        {
            try
            {
                return Ok(await _playerAbilityRepository.GetPlayerAbilities());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Get By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerAbility>> GetPlayerAbility(int id)
        {
            try
            {
                var result = await _playerAbilityRepository.GetPlayerAbility(id);

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
        public async Task<ActionResult<PlayerAbility>> CreatePlayerAbility(PlayerAbility playerAbility)
        {
            try
            {
                if (playerAbility != null)
                {
                    var newPlayerAbility = await _playerAbilityRepository.AddPlayerAbility(playerAbility);

                    return CreatedAtAction(nameof(GetPlayerAbility), new { id = newPlayerAbility.Id }, newPlayerAbility);
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
        public async Task<ActionResult<PlayerAbility>> UpdatePlayerAbility(int id, PlayerAbility playerAbility)
        {
            try
            {
                if (id != playerAbility.Id)
                {
                    return BadRequest($"Ability ID mismatch. Specified ID = {id} | Ability ID = {playerAbility.Id}");
                }

                var playerAbilityToUpdate = await _playerAbilityRepository.GetPlayerAbility(id);

                if (playerAbilityToUpdate == null)
                {
                    return NotFound($"Could not find Ability with ID = {id}.");
                }

                return await _playerAbilityRepository.UpdatePlayerAbility(playerAbility);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PlayerAbility>> DeletePlayerAbility(int id, PlayerAbility playerAbility)
        {
            try
            {
                var playerAbilityToDelete = await _playerAbilityRepository.GetPlayerAbility(id);

                if (playerAbilityToDelete == null)
                {
                    return BadRequest($"Could not find Ability with ID = {id}.");
                }

                return await _playerAbilityRepository.DeletePlayerAbility(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
            }
        }
    }

}
