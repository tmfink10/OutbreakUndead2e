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
    [Route("api/BaseSkills")]
    [ApiController]
    public class BaseSkillController : ControllerBase
    {
        private readonly IBaseSkillRepository _baseSkillRepository;

        public BaseSkillController(IBaseSkillRepository baseSkillRepository)
        {
            _baseSkillRepository = baseSkillRepository;
        }

        //Search
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<BaseSkill>>> Search(string search)
        {
            try
            {
                var result = await _baseSkillRepository.SearchBaseSkills(search);

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
        public async Task<ActionResult> GetBaseSkills()
        {
            try
            {
                return Ok(await _baseSkillRepository.GetBaseSkills());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Get By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseSkill>> GetBaseSkill(int id)
        {
            try
            {
                var result = await _baseSkillRepository.GetBaseSkill(id);

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
        public async Task<ActionResult<BaseSkill>> CreateBaseSkill(BaseSkill baseSkill)
        {
            try
            {
                if (baseSkill != null)
                {
                    var newBaseSkill = await _baseSkillRepository.AddBaseSkill(baseSkill);

                    return CreatedAtAction(nameof(GetBaseSkill), new { id = newBaseSkill.Id }, newBaseSkill);
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
        public async Task<ActionResult<BaseSkill>> UpdateBaseSkill(int id, BaseSkill baseSkill)
        {
            try
            {
                if (id != baseSkill.Id)
                {
                    return BadRequest($"Skill ID mismatch. Specified ID = {id} | Skill ID = {baseSkill.Id}");
                }

                var baseSkillToUpdate = await _baseSkillRepository.GetBaseSkill(id);

                if (baseSkillToUpdate == null)
                {
                    return NotFound($"Could not find Skill with ID = {id}.");
                }

                return await _baseSkillRepository.UpdateBaseSkill(baseSkill);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BaseSkill>> DeleteBaseSkill(int id, BaseSkill baseSkill)
        {
            try
            {
                var baseSkillToDelete = await _baseSkillRepository.GetBaseSkill(id);

                if (baseSkillToDelete == null)
                {
                    return BadRequest($"Could not find Skill with ID = {id}.");
                }

                return await _baseSkillRepository.DeleteBaseSkill(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
            }
        }
    }

}
