using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EquipmentAPI.DataAccessLayer;
using EquipmentAPI.Models;

namespace EquipmentAPI.Controllers
{
    /// <summary>
    /// Controller for managing the status of equipment.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public StatusController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Status
        /// <summary>
        /// Retrieves all equipment.
        /// </summary>
        /// <returns>A list of equipment.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Equipment>>> GetEquipment()
        {
            return await _context.Equipment.ToListAsync();
        }

        // GET: api/Status/5
        /// <summary>
        /// Retrieves a specific equipment by its ID.
        /// </summary>
        /// <param name="id">The ID of the equipment.</param>
        /// <returns>The equipment with the specified ID.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Equipment>> GetEquipment(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);

            if (equipment == null)
            {
                return NotFound();
            }

            return equipment;
        }

        // PUT: api/Status/5
        /// <summary>
        /// Updates the status of a specific equipment.
        /// </summary>
        /// <param name="id">The ID of the equipment.</param>
        /// <param name="equipment">The updated equipment object.</param>
        /// <returns>No content if successful.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipment(int id, Equipment equipment)
        {
            if (id != equipment.Id)
            {
                return BadRequest();
            }

            _context.Entry(equipment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipmentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Status
        /// <summary>
        /// Creates a new equipment.
        /// </summary>
        /// <param name="equipment">The equipment object to create.</param>
        /// <returns>The created equipment.</returns>
        [HttpPost]
        public async Task<ActionResult<Equipment>> PostEquipment(Equipment equipment)
        {
            _context.Equipment.Add(equipment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEquipment", new { id = equipment.Id }, equipment);
        }

        // DELETE: api/Status/5
        /// <summary>
        /// Deletes a specific equipment by its ID.
        /// </summary>
        /// <param name="id">The ID of the equipment to delete.</param>
        /// <returns>No content if successful.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            var equipment = await _context.Equipment.FindAsync(id);
            if (equipment == null)
            {
                return NotFound();
            }

            _context.Equipment.Remove(equipment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipmentExists(int id)
        {
            return _context.Equipment.Any(e => e.Id == id);
        }
    }
}
