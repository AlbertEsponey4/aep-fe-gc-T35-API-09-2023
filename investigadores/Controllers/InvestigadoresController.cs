using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using investigadores.Data;
using investigadores.Models;

namespace investigadores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestigadoresController : ControllerBase
    {
        private readonly investigadoresContext _context;

        public InvestigadoresController(investigadoresContext context)
        {
            _context = context;
        }

        // GET: api/Investigadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Investigadore>>> GetInvestigadores()
        {
          if (_context.Investigadores == null)
          {
              return NotFound();
          }
            return await _context.Investigadores.ToListAsync();
        }

        // GET: api/Investigadores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Investigadore>> GetInvestigadore(string id)
        {
          if (_context.Investigadores == null)
          {
              return NotFound();
          }
            var investigadore = await _context.Investigadores.FindAsync(id);

            if (investigadore == null)
            {
                return NotFound();
            }

            return investigadore;
        }

        // PUT: api/Investigadores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvestigadore(string id, Investigadore investigadore)
        {
            if (id != investigadore.Dni)
            {
                return BadRequest();
            }

            _context.Entry(investigadore).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvestigadoreExists(id))
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

        // POST: api/Investigadores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Investigadore>> PostInvestigadore(Investigadore investigadore)
        {
          if (_context.Investigadores == null)
          {
              return Problem("Entity set 'investigadoresContext.Investigadores'  is null.");
          }
            _context.Investigadores.Add(investigadore);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvestigadoreExists(investigadore.Dni))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvestigadore", new { id = investigadore.Dni }, investigadore);
        }

        // DELETE: api/Investigadores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvestigadore(string id)
        {
            if (_context.Investigadores == null)
            {
                return NotFound();
            }
            var investigadore = await _context.Investigadores.FindAsync(id);
            if (investigadore == null)
            {
                return NotFound();
            }

            _context.Investigadores.Remove(investigadore);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvestigadoreExists(string id)
        {
            return (_context.Investigadores?.Any(e => e.Dni == id)).GetValueOrDefault();
        }
    }
}
