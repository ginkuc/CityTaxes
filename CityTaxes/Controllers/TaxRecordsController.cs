using CityTaxes.Models;
using CityTaxes.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CityTaxes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxRecordsController : ControllerBase
    {
        private readonly ICityTaxService _cityTaxService;

        public TaxRecordsController(ICityTaxService cityTaxService)
        {
            _cityTaxService = cityTaxService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxRecord>>> GetTaxRecords()
        {
            return await _cityTaxService.GetAllTaxRecords();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaxRecord>> GetTaxRecord(int id)
        {
            var taxRecord = await _cityTaxService.GetTaxRecordById(id);

            if (taxRecord == null)
            {
                return NotFound();
            }

            return taxRecord;
        }

        [HttpGet("{municipality}/{date}")]
        public async Task<IActionResult> GetScheduledTax(string municipality, DateTime date)
        {
            if (string.IsNullOrEmpty(municipality))
            {
                return BadRequest("Municipality must be provided.");
            }

            var scheduledTaxValue = await _cityTaxService.GetScheduledTax(municipality, date);

            if (scheduledTaxValue == null)
            {
                return NotFound();
            }

            return Ok(scheduledTaxValue);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaxRecord(int id, TaxRecord taxRecord)
        {
            if (id != taxRecord.Id)
            {
                return BadRequest();
            }

            try
            {
                await _cityTaxService.UpdateTaxRecord(taxRecord);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<TaxRecord>> PostTaxRecord(TaxRecord taxRecord)
        {
            try
            {
                var createdRecord = await _cityTaxService.CreateTaxRecord(taxRecord);

                return CreatedAtAction("GetTaxRecord", new { id = createdRecord.Id }, createdRecord);
            }
            catch (InvalidOperationException exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTaxRecord(int id)
        {
            await _cityTaxService.DeleteTaxRecord(id);

            return NoContent();
        }
    }
}
