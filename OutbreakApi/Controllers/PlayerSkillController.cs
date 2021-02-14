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
    [Route("api/PlayerSkills")]
    [ApiController]
    public class PlayerSkillController : ControllerBase
    {
        private readonly IPlayerSkillRepository _playerSkillRepository;

        public PlayerSkillController(IPlayerSkillRepository playerSkillRepository)
        {
            _playerSkillRepository = playerSkillRepository;
        }

        //Search
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<PlayerSkill>>> Search(string search)
        {
            try
            {
                var result = await _playerSkillRepository.SearchPlayerSkills(search);

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
        public async Task<ActionResult> GetPlayerSkills()
        {
            try
            {
                return Ok(await _playerSkillRepository.GetPlayerSkills());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Get By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PlayerSkill>> GetPlayerSkill(int id)
        {
            try
            {
                var result = await _playerSkillRepository.GetPlayerSkill(id);

                if (result != null)
                {
                    return Ok(result);
                }

                return NotFound("Id/Skill Mismatch");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Create New
        [HttpPost]
        public async Task<ActionResult<PlayerSkill>> CreatePlayerSkill(PlayerSkill playerSkill)
        {
            try
            {
                if (playerSkill != null)
                {
                    var newPlayerSkill = await _playerSkillRepository.AddPlayerSkill(playerSkill);

                    return CreatedAtAction(nameof(GetPlayerSkill), new { id = newPlayerSkill.Id }, newPlayerSkill);
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
        public async Task<ActionResult<PlayerSkill>> UpdatePlayerSkill(int id, PlayerSkill playerSkill)
        {
            try
            {
                if (id != playerSkill.Id)
                {
                    return BadRequest($"Skill ID mismatch. Specified ID = {id} | Skill ID = {playerSkill.Id}");
                }

                var playerSkillToUpdate = await _playerSkillRepository.GetPlayerSkill(id);

                if (playerSkillToUpdate == null)
                {
                    return NotFound($"Could not find Skill with ID = {id}.");
                }

                return await _playerSkillRepository.UpdatePlayerSkill(playerSkill);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PlayerSkill>> DeletePlayerSkill(int id, PlayerSkill playerSkill)
        {
            try
            {
                var playerSkillToDelete = await _playerSkillRepository.GetPlayerSkill(id);

                if (playerSkillToDelete == null)
                {
                    return BadRequest($"Could not find Skill with ID = {id}.");
                }

                return await _playerSkillRepository.DeletePlayerSkill(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
            }
        }
    }

}
