using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackEndContacto.Data;
using BackEndContacto.Models;
using EmailService;

namespace BackEndContacto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailSender _emailSender;
        public ContactoController(AppDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: api/Contacto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contacto>>> GetConcacto()
        {

            return await _context.Concacto.ToListAsync();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SendMail([FromBody] EmailData data)
        {
            List<string> list = new List<string>();
            list.Add(data.Name);
            list.Add(data.Mail);
            list.Add(data.Place);
            list.Add(data.Date);

            string[] strData = list.ToArray();

            var message = new Message(new string[] { data.To  }, "Green Leaves", strData, null);
            await _emailSender.SendEmailAsync(message);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SendMailDefault([FromBody] EmailData data)
        {
            List<string> list = new List<string>();
            list.Add(data.Name);
            list.Add(data.Mail);
            list.Add(data.Place);
            list.Add(data.Date);

            string[] strData = list.ToArray();

            var message = new Message(new string[] { data.To }, "Green Leaves", strData, null);
            await _emailSender.SendEmailAsync(message);
            return Ok();
        }

        // GET: api/Contacto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contacto>> GetContacto(int id)
        {
            var contacto = await _context.Concacto.FindAsync(id);

            if (contacto == null)
            {
                return NotFound();
            }

            return contacto;
        }

        // PUT: api/Contacto/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContacto(int id, Contacto contacto)
        {
            if (id != contacto.Id)
            {
                return BadRequest();
            }

            _context.Entry(contacto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactoExists(id))
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

        // POST: api/Contacto/PostContacto
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost("[action]")]
        public async Task<ActionResult<Contacto>> PostContacto([FromBody] Contacto contacto)
        {
            Contacto oContacto = new Contacto();

            oContacto.Nombre = contacto.Nombre;
            oContacto.Email = contacto.Email;
            oContacto.Telefono = contacto.Telefono;
            oContacto.Fehca = contacto.Fehca;
            oContacto.CiudadEst = contacto.CiudadEst;

            _context.Concacto.Add(oContacto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContacto", new { id = contacto.Id }, contacto);
        }

        // DELETE: api/Contacto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Contacto>> DeleteContacto(int id)
        {
            var contacto = await _context.Concacto.FindAsync(id);
            if (contacto == null)
            {
                return NotFound();
            }

            _context.Concacto.Remove(contacto);
            await _context.SaveChangesAsync();

            return contacto;
        }

        private bool ContactoExists(int id)
        {
            return _context.Concacto.Any(e => e.Id == id);
        }
    }
}
