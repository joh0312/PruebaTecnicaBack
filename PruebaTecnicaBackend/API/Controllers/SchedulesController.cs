using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Entidades;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulesController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SchedulesController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]  //api/schedules
        public async Task<ActionResult<IEnumerable<Schedules>>> GetSchedules()
        {
            var schedules = await _db.schedules.ToListAsync();
            if (schedules == null)
            {
                return NotFound();
            }
            return Ok(schedules);
        }

        [HttpGet("{id}")] //api/schedules/1
        public async Task<ActionResult<Schedules>> GetSchedules(int id)
        {
            var schedules = await _db.schedules.FindAsync(id);
            if (schedules == null)
            {
                return NotFound();
            }
            return Ok(schedules);
        }

        [HttpPost]  // POST api/schedules
        public async Task<ActionResult<Schedules>> PostSchedules(Schedules schedules)
        {
            if (ModelState.IsValid)
            {
                // Verificar si el route_id existe en la tabla de rutas
                if (!_db.route.Any(r => r.id == schedules.route_id))
                {
                    return BadRequest("Invalid route_id. The specified route does not exist.");
                }

                _db.schedules.Add(schedules);
                await _db.SaveChangesAsync();
                return CreatedAtAction(nameof(GetSchedules), new { id = schedules.id }, schedules);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }







        [HttpPut("{id}")] // PUT api/schedules/1
        public async Task<IActionResult> PutSchedules(int id, Schedules updateSchedules)
        {
            if (id != updateSchedules.id)
            {
                return BadRequest("ID mismatch");
            }

            var schedules = await _db.schedules.FindAsync(id);

            if (schedules == null)
            {
                return NotFound();
            }

            // Actualizar los campos del conductor con los valores recibidos
            schedules.route_id = updateSchedules.route_id;
            schedules.week_num = updateSchedules.week_num;
            schedules.from = updateSchedules.from;
            schedules.to = updateSchedules.to;
            schedules.active = updateSchedules.active;

            _db.Entry(schedules).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SchedulesExists(id))
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

        private bool SchedulesExists(int id)
        {
            return _db.schedules.Any(e => e.id == id);
        }

        [HttpDelete("{id}")] // DELETE api/schedules/1
        public async Task<IActionResult> DeleteSchedules(int id)
        {
            var schedules = await _db.schedules.FindAsync(id);

            if (schedules == null)
            {
                return NotFound();
            }

            _db.schedules.Remove(schedules);
            await _db.SaveChangesAsync();

            return NoContent();
        }
    }
}

