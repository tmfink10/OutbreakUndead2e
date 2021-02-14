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
    [Route("api/BaseAttributes")]
    [ApiController]
    public class BaseAttributeController : ControllerBase
    {
        private readonly IBaseAttributeRepository _baseAttributeRepository;

        public BaseAttributeController(IBaseAttributeRepository baseAttributeRepository)
        {
            _baseAttributeRepository = baseAttributeRepository;
        }

        //Search
        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<BaseAttribute>>> Search(string search)
        {
            try
            {
                var result = await _baseAttributeRepository.SearchBaseAttributes(search);

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
        public async Task<ActionResult> GetBaseAttributes()
        {
            try
            {
                return Ok(await _baseAttributeRepository.GetBaseAttributes());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        //Get By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<BaseAttribute>> GetBaseAttribute(int id)
        {
            try
            {
                var result = await _baseAttributeRepository.GetBaseAttribute(id);

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
        public async Task<ActionResult<BaseAttribute>> CreateBaseAttribute(BaseAttribute baseAttribute)
        {
            try
            {
                if (baseAttribute != null)
                {
                    var newBaseAttribute = await _baseAttributeRepository.AddBaseAttribute(baseAttribute);

                    return CreatedAtAction(nameof(GetBaseAttribute), new { id = newBaseAttribute.Id }, newBaseAttribute);
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
        public async Task<ActionResult<BaseAttribute>> UpdateBaseAttribute(int id, BaseAttribute baseAttribute)
        {
            try
            {
                if (id != baseAttribute.Id)
                {
                    return BadRequest($"Attribute ID mismatch. Specified ID = {id} | Attribute ID = {baseAttribute.Id}");
                }

                var baseAttributeToUpdate = await _baseAttributeRepository.GetBaseAttribute(id);

                if (baseAttributeToUpdate == null)
                {
                    return NotFound($"Could not find Attribute with ID = {id}.");
                }

                return await _baseAttributeRepository.UpdateBaseAttribute(baseAttribute);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in the database.");
            }
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<BaseAttribute>> DeleteBaseAttribute(int id, BaseAttribute baseAttribute)
        {
            try
            {
                var baseAttributeToDelete = await _baseAttributeRepository.GetBaseAttribute(id);

                if (baseAttributeToDelete == null)
                {
                    return BadRequest($"Could not find Attribute with ID = {id}.");
                }

                return await _baseAttributeRepository.DeleteBaseAttribute(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database.");
            }
        }
    }

}
