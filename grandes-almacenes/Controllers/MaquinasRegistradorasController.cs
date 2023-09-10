using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using grandesAlmacenes.Data;
using grandesAlmacenes.Models;

namespace grandes_almacenes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaquinasRegistradorasController : ControllerBase
    {
        private readonly grandesAlmacenesContext _context;

        public MaquinasRegistradorasController(grandesAlmacenesContext context)
        {
            _context = context;
        }

        // GET: api/MaquinasRegistradoras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaquinasRegistradora>>> GetMaquinasRegistradoras()
        {
          if (_context.MaquinasRegistradoras == null)
          {
              return NotFound();
          }
            return await _context.MaquinasRegistradoras.ToListAsync();
        }

        // GET: api/MaquinasRegistradoras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaquinasRegistradora>> GetMaquinasRegistradora(int id)
        {
          if (_context.MaquinasRegistradoras == null)
          {
              return NotFound();
          }
            var maquinasRegistradora = await _context.MaquinasRegistradoras.FindAsync(id);

            if (maquinasRegistradora == null)
            {
                return NotFound();
            }

            return maquinasRegistradora;
        }

        // PUT: api/MaquinasRegistradoras/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMaquinasRegistradora(int id, MaquinasRegistradora maquinasRegistradora)
        {
            if (id != maquinasRegistradora.Codigo)
            {
                return BadRequest();
            }

            _context.Entry(maquinasRegistradora).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinasRegistradoraExists(id))
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

        // POST: api/MaquinasRegistradoras
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MaquinasRegistradora>> PostMaquinasRegistradora(MaquinasRegistradora maquinasRegistradora)
        {
          if (_context.MaquinasRegistradoras == null)
          {
              return Problem("Entity set 'grandesAlmacenesContext.MaquinasRegistradoras'  is null.");
          }
            _context.MaquinasRegistradoras.Add(maquinasRegistradora);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MaquinasRegistradoraExists(maquinasRegistradora.Codigo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMaquinasRegistradora", new { id = maquinasRegistradora.Codigo }, maquinasRegistradora);
        }

        // DELETE: api/MaquinasRegistradoras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMaquinasRegistradora(int id)
        {
            if (_context.MaquinasRegistradoras == null)
            {
                return NotFound();
            }
            var maquinasRegistradora = await _context.MaquinasRegistradoras.FindAsync(id);
            if (maquinasRegistradora == null)
            {
                return NotFound();
            }

            _context.MaquinasRegistradoras.Remove(maquinasRegistradora);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MaquinasRegistradoraExists(int id)
        {
            return (_context.MaquinasRegistradoras?.Any(e => e.Codigo == id)).GetValueOrDefault();
        }
    }
}
