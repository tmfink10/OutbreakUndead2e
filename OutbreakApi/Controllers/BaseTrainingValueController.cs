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
    [Route("api/BaseTrainingValues")]
    [ApiController]
    public class BaseTrainingValueController : ControllerBase
    {
        private readonly IBaseTrainingValueRepository _baseTrainingValueRepository;

        public BaseTrainingValueController(IBaseTrainingValueRepository baseTrainingValueRepository)
        {
            _baseTrainingValueRepository = baseTrainingValueRepository;
        }

        //Search
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<BaseTrainingValue>>> Search(string search)
        {
            try
            {
                var result = await _baseTrainingValueRepository.SearchBaseTrainingValues(search);

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
        public async Task<ActionResult> GetBaseTrainingValues()
        {
            try
            {
                return Ok(await _baseTrainingValueRepository.GetBaseTrainingValues());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Get By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseTrainingValue>> GetBaseTrainingValue(int id)
        {
            try
            {
                var result = await _baseTrainingValueRepository.GetBaseTrainingValue(id);

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
        public async Task<ActionResult<BaseTrainingValue>> CreateBaseTrainingValue(BaseTrainingValue baseTrainingValue)
        {
            try
            {
                if (baseTrainingValue != null)
                {
                    var newBaseTrainingValue = await _baseTrainingValueRepository.AddBaseTrainingValue(baseTrainingValue);

                    return CreatedAtAction(nameof(GetBaseTrainingValue), new { id = newBaseTrainingValue.Id }, newBaseTrainingValue);
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
        public async Task<ActionResult<BaseTrainingValue>> UpdateBaseTrainingValue(int id, BaseTrainingValue baseTrainingValue)
        {
            try
            {
                if (id != baseTrainingValue.Id)
                {
                    return BadRequest($"TrainingValue ID mismatch. Specified ID = {id} | TrainingValue ID = {baseTrainingValue.Id}");
                }

                var baseTrainingValueToUpdate = await _baseTrainingValueRepository.GetBaseTrainingValue(id);

                if (baseTrainingValueToUpdate == null)
                {
                    return NotFound($"Could not find TrainingValue with ID = {id}.");
                }

                return await _baseTrainingValueRepository.UpdateBaseTrainingValue(baseTrainingValue);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BaseTrainingValue>> DeleteBaseTrainingValue(int id, BaseTrainingValue baseTrainingValue)
        {
            try
            {
                var baseTrainingValueToDelete = await _baseTrainingValueRepository.GetBaseTrainingValue(id);

                if (baseTrainingValueToDelete == null)
                {
                    return BadRequest($"Could not find TrainingValue with ID = {id}.");
                }

                return await _baseTrainingValueRepository.DeleteBaseTrainingValue(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
            }
        }
    }

}
