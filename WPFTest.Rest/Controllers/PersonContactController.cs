using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPFTest.Rest.DBContext;
using WPFTest.Rest.Models;

namespace WPFTest.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonContactController : ControllerBase
    {
        private readonly test_dbContext _context;

        public PersonContactController(test_dbContext context)
        {
            _context = context;
        }

        // GET: api/PersonContact
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonContact>>> GetPersonContact()
        {
            return await _context.PersonContact.ToListAsync();
        }

        // GET: api/PersonContact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonContact>> GetPersonContact(int id)
        {
            var personContact = await _context.PersonContact.FindAsync(id);

            if (personContact == null)
            {
                return NotFound();
            }

            return personContact;
        }

        // PUT: api/PersonContact/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonContact(int id, PersonContact personContact)
        {
            if (id != personContact.PersonId)
            {
                return BadRequest();
            }

            _context.Entry(personContact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonContactExists(id))
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

        // POST: api/PersonContact
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PersonContact>> PostPersonContact(PersonContact personContact)
        {
            _context.PersonContact.Add(personContact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PersonContactExists(personContact.PersonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonContact", new { id = personContact.PersonId }, personContact);
        }

        // DELETE: api/PersonContact/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PersonContact>> DeletePersonContact(int id)
        {
            var personContact = await _context.PersonContact.FindAsync(id);
            if (personContact == null)
            {
                return NotFound();
            }

            _context.PersonContact.Remove(personContact);
            await _context.SaveChangesAsync();

            return personContact;
        }

        private bool PersonContactExists(int id)
        {
            return _context.PersonContact.Any(e => e.PersonId == id);
        }
    }
}
