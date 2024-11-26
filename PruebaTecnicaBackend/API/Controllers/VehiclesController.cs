using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public VehiclesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]  // GET: api/vehicles
        public async Task<ActionResult<IEnumerable<Vehicles>>> GetVehicles()
        {
            var vehicles = await _db.vehicles.ToListAsync();
            if (vehicles == null)
            {
                return NotFound();
            }
            return Ok(vehicles);
        }

        [HttpGet("{id}")] // GET: api/vehicles/1
        public async Task<ActionResult<Vehicles>> GetVehicle(int id)
        {
            var vehicle = await _db.vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return Ok(vehicle);
        }

        [HttpPost]  // POST: api/vehicles
        public async Task<ActionResult<Vehicles>> PostVehicle(Vehicles vehicle)
        {
            if (ModelState.IsValid)
            {
                _db.vehicles.Add(vehicle);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetVehicle), new { id = vehicle.id }, vehicle);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")] // PUT: api/vehicles/1
        public async Task<IActionResult> PutVehicle(int id, Vehicles updateVehicle)
        {
            if (id != updateVehicle.id)
            {
                return BadRequest("ID mismatch");
            }

            var vehicle = await _db.vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            // Actualizar los campos del vehículo con los valores recibidos
            vehicle.description = updateVehicle.description;
            vehicle.year = updateVehicle.year;
            vehicle.make = updateVehicle.make;
            vehicle.capacity = updateVehicle.capacity;
            vehicle.active = updateVehicle.active;

            _db.Entry(vehicle).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehicleExists(id))
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

        private bool VehicleExists(int id)
        {
            return _db.vehicles.Any(e => e.id == id);
        }

        [HttpDelete("{id}")] // DELETE: api/vehicles/1
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _db.vehicles.FindAsync(id);

            if (vehicle == null)
            {
                return NotFound();
            }

            _db.vehicles.Remove(vehicle);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
