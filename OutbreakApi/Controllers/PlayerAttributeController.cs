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
    [Route("api/PlayerAttributes")]
    [ApiController]
    public class PlayerAttributeController : ControllerBase
    {
        private readonly IPlayerAttributeRepository _playerAttributeRepository;

        public PlayerAttributeController(IPlayerAttributeRepository playerAttributeRepository)
        {
            _playerAttributeRepository = playerAttributeRepository;
        }

        //Search
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<PlayerAttribute>>> Search(string search)
        {
            try
            {
                var result = await _playerAttributeRepository.SearchPlayerAttributes(search);

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
        public async Task<ActionResult> GetPlayerAttributes()
        {
            try
            {
                return Ok(await _playerAttributeRepository.GetPlayerAttributes());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Get By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerAttribute>> GetPlayerAttribute(int id)
        {
            try
            {
                var result = await _playerAttributeRepository.GetPlayerAttribute(id);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound("Id/Attribute Mismatch");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Create New
        [HttpPost]
        public async Task<ActionResult<PlayerAttribute>> CreatePlayerAttribute(PlayerAttribute playerAttribute)
        {
            try
            {
                if (playerAttribute != null)
                {
                    var newPlayerAttribute = await _playerAttributeRepository.AddPlayerAttribute(playerAttribute);

                    return CreatedAtAction(nameof(GetPlayerAttribute), new { id = newPlayerAttribute.Id }, newPlayerAttribute);
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
        public async Task<ActionResult<PlayerAttribute>> UpdatePlayerAttribute(int id, PlayerAttribute playerAttribute)
        {
            try
            {
                if (id != playerAttribute.Id)
                {
                    return BadRequest($"Attribute ID mismatch. Specified ID = {id} | Attribute ID = {playerAttribute.Id}");
                }

                var playerAttributeToUpdate = await _playerAttributeRepository.GetPlayerAttribute(id);

                if (playerAttributeToUpdate == null)
                {
                    return NotFound($"Could not find Attribute with ID = {id}.");
                }

                return await _playerAttributeRepository.UpdatePlayerAttribute(playerAttribute);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PlayerAttribute>> DeletePlayerAttribute(int id, PlayerAttribute playerAttribute)
        {
            try
            {
                var playerAttributeToDelete = await _playerAttributeRepository.GetPlayerAttribute(id);

                if (playerAttributeToDelete == null)
                {
                    return BadRequest($"Could not find Attribute with ID = {id}.");
                }

                return await _playerAttributeRepository.DeletePlayerAttribute(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
            }
        }
    }

}
