using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndContacto.Data;
using BackEndContacto.Models;

namespace BackEndContacto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CiudadEstadoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CiudadEstadoController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/CiudadEstado
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CiudadEstado>>> GetCiudadEstados()
        {
            return await _context.CiudadEstados.ToListAsync();
        }

        // GET: api/CiudadEstado/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CiudadEstado>> GetCiudadEstado(int id)
        {
            var ciudadEstado = await _context.CiudadEstados.FindAsync(id);

            if (ciudadEstado == null)
            {
                return NotFound();
            }

            return ciudadEstado;
        }

        // PUT: api/CiudadEstado/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCiudadEstado(int id, CiudadEstado ciudadEstado)
        {
            if (id != ciudadEstado.Id)
            {
                return BadRequest();
            }

            _context.Entry(ciudadEstado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CiudadEstadoExists(id))
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

        // POST: api/CiudadEstado
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<CiudadEstado>> PostCiudadEstado(CiudadEstado ciudadEstado)
        {
            _context.CiudadEstados.Add(ciudadEstado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCiudadEstado", new { id = ciudadEstado.Id }, ciudadEstado);
        }

        // DELETE: api/CiudadEstado/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CiudadEstado>> DeleteCiudadEstado(int id)
        {
            var ciudadEstado = await _context.CiudadEstados.FindAsync(id);
            if (ciudadEstado == null)
            {
                return NotFound();
            }

            _context.CiudadEstados.Remove(ciudadEstado);
            await _context.SaveChangesAsync();

            return ciudadEstado;
        }

        private bool CiudadEstadoExists(int id)
        {
            return _context.CiudadEstados.Any(e => e.Id == id);
        }
    }
}
