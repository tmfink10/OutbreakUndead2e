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
    [Route("api/PlayerCharacters")]
    [ApiController]
    public class PlayerCharacterController : ControllerBase
    {
        private readonly IPlayerCharacterRepository _playerCharacterRepository;

        public PlayerCharacterController(IPlayerCharacterRepository playerCharacterRepository)
        {
            _playerCharacterRepository = playerCharacterRepository;
        }

        //Search
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<PlayerCharacter>>> Search(string search)
        {
            try
            {
                var result = await _playerCharacterRepository.SearchPlayerCharacters(search);

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
        public async Task<ActionResult> GetPlayerCharacters()
        {
            try
            {
                return Ok(await _playerCharacterRepository.GetPlayerCharacters());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Get By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerCharacter>> GetPlayerCharacter(int id)
        {
            try
            {
                var result = await _playerCharacterRepository.GetPlayerCharacter(id);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound("Id/TrainingValue Mismatch");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Create New
        [HttpPost]
        public async Task<ActionResult<PlayerCharacter>> CreatePlayerCharacter(PlayerCharacter playerCharacter)
        {


            if (playerCharacter != null)
            {
                var newPlayerCharacter = await _playerCharacterRepository.AddPlayerCharacter(playerCharacter);

                return CreatedAtAction(nameof(GetPlayerCharacter), new { id = newPlayerCharacter.Id }, newPlayerCharacter);
            }

            return BadRequest();
        }

        //Update by Put
        [HttpPut("{id:int}")]
        public async Task<ActionResult<PlayerCharacter>> UpdatePlayerCharacter(int id, PlayerCharacter playerCharacter)
        {
            try
            {
                if (id != playerCharacter.Id)
                {
                    return BadRequest($"TrainingValue ID mismatch. Specified ID = {id} | TrainingValue ID = {playerCharacter.Id}");
                }

                var playerCharacterToUpdate = await _playerCharacterRepository.GetPlayerCharacter(id);

                if (playerCharacterToUpdate == null)
                {
                    return NotFound($"Could not find TrainingValue with ID = {id}.");
                }

                return await _playerCharacterRepository.UpdatePlayerCharacter(playerCharacter);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PlayerCharacter>> DeletePlayerCharacter(int id, PlayerCharacter playerCharacter)
        {
            try
            {
                var playerCharacterToDelete = await _playerCharacterRepository.GetPlayerCharacter(id);

                if (playerCharacterToDelete == null)
                {
                    return BadRequest($"Could not find TrainingValue with ID = {id}.");
                }

                return await _playerCharacterRepository.DeletePlayerCharacter(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
            }
        }
    }

}
