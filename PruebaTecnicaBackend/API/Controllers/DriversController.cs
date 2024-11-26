using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public DriversController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]  //api/drivers
        public async Task<ActionResult<IEnumerable<Drivers>>> GetDrivers()
        {
            var drivers = await _db.drivers.ToListAsync();
            if (drivers == null)
            {
                return NotFound();
            }
            return Ok(drivers);
        }

        [HttpGet("{id}")] //api/drivers/1
        public async Task<ActionResult<Drivers>> GetDriver(int id)
        {
            var driver = await _db.drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        [HttpPost]  // POST api/drivers
        public async Task<ActionResult<Drivers>> PostDrivers(Drivers driver)
        {
            if (ModelState.IsValid)
            {
                if (_db.drivers.Any(d => d.ssn == driver.ssn))
                {
                    return Conflict(new { message = "A driver with the same SSN already exists." });
                }

                _db.drivers.Add(driver);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetDriver), new { id = driver.id }, driver);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")] // PUT api/drivers/1
        public async Task<IActionResult> PutDriver(int id, Drivers updateDriver)
        {
            if (id != updateDriver.id)
            {
                return BadRequest("ID mismatch");
            }

            var driver = await _db.drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            // Actualizar los campos del conductor con los valores recibidos
            driver.last_name = updateDriver.last_name;
            driver.first_name = updateDriver.first_name;
            driver.ssn = updateDriver.ssn;
            driver.dob = updateDriver.dob;
            driver.address = updateDriver.address;
            driver.city = updateDriver.city;
            driver.zip = updateDriver.zip;
            driver.phone = updateDriver.phone;
            driver.active = updateDriver.active;

            _db.Entry(driver).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(id))
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

        private bool DriverExists(int id)
        {
            return _db.drivers.Any(e => e.id == id);
        }

        [HttpDelete("{id}")] // DELETE api/drivers/1
        public async Task<IActionResult> DeleteDriver(int id)
        {
            var driver = await _db.drivers.FindAsync(id);

            if (driver == null)
            {
                return NotFound();
            }

            _db.drivers.Remove(driver);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}
