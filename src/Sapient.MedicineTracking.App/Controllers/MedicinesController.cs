using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sapient.MedicineTracking.App.Models;

namespace Sapient.MedicineTracking.App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly MedicineContext _context;
        private readonly ILogger _logger;

        public MedicinesController(MedicineContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger<MedicinesController>();
        }

        // GET: api/Medicines
        [HttpGet]
        public async Task<IEnumerable<Medicine>> GetMedicinesAsync()
        {
            _logger.LogInformation("Retrieve all the Medicines");
            return await _context.Medicines.ToListAsync();
        }

        // GET: api/Medicines/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedicine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stopwatch = Stopwatch.StartNew();
            var medicine = await _context.Medicines.FindAsync(id);
            _logger.LogInformation($"Searching medicine with id# {id}");

            if (medicine == null)
            {
                _logger.LogError($"Medicine with id# {id} is not found. TimeElapsedInMilliSeconds: {stopwatch.ElapsedMilliseconds}");
                return NotFound();
            }

            _logger.LogInformation($"Medicine with id# {id} is found. TimeElapsedInMilliSeconds: {stopwatch.ElapsedMilliseconds}");
            return Ok(medicine);
        }

        // PUT: api/Medicines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicine([FromRoute] int id, [FromBody] Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicine.Id)
            {
                return BadRequest();
            }

            var stopwatch = Stopwatch.StartNew();
            _context.Entry(medicine).State = EntityState.Modified;

            try
            {
                _logger.LogInformation($"Medicine with id# {id} is PUT. TimeElapsedInMilliSeconds: {stopwatch.ElapsedMilliseconds}");
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
                {
                    _logger.LogError($"Medicine with id# {id} is not found. TimeElapsedInMilliSeconds: {stopwatch.ElapsedMilliseconds}");
                    return NotFound();
                }

                throw;
            }

            return NoContent();
        }

        // POST: api/Medicines
        [HttpPost]
        public async Task<IActionResult> PostMedicine([FromBody] Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stopwatch = Stopwatch.StartNew();
            _context.Medicines.Add(medicine);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"New Medicine with is created. TimeElapsedInMilliSeconds: {stopwatch.ElapsedMilliseconds}");
            return CreatedAtAction(nameof(GetMedicine), new { id = medicine.Id }, medicine);
        }

        // DELETE: api/Medicines/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedicine([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var stopwatch = Stopwatch.StartNew();
            var medicine = await _context.Medicines.FindAsync(id);
            if (medicine == null)
            {
                _logger.LogError($"Medicine with id# {id} is not found. TimeElapsedInMilliSeconds: {stopwatch.ElapsedMilliseconds}");
                return NotFound();
            }

            _context.Medicines.Remove(medicine);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Medicine with {id} is deleted. TimeElapsedInMilliSeconds: {stopwatch.ElapsedMilliseconds}");

            return Ok(medicine);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return BadRequest(); // "Error Loading the Medicine Tracking System page"
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private bool MedicineExists(int id)
        {
            return _context.Medicines.Any(e => e.Id == id);
        }
    }
}