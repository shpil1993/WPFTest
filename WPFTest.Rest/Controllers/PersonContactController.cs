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
        
        [HttpGet]
        [Route("[action]/{personId}")]
        public async Task<ActionResult<IEnumerable<PersonContact>>> GetPersonContactsByPersonId(int personId)
        {
            return await _context.PersonContact.Where(e => e.PersonId == personId).ToListAsync();
        }

        // GET: api/PersonContact/5
        [HttpGet("{id}/{personId}")]
        public async Task<ActionResult<PersonContact>> GetPersonContact(int id, int personId)
        {
            var personContact = await _context.PersonContact.FirstOrDefaultAsync(e => e.PersonContactId == id && e.PersonId == personId);

            if (personContact == null)
            {
                return NotFound();
            }

            return personContact;
        }

        // PUT: api/PersonContact/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}/{personId}")]
        public async Task<IActionResult> PutPersonContact(int id, int personId, PersonContact personContact)
        {
            if (id != personContact.PersonContactId)
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
                if (!PersonContactExists(id, personId))
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
                if (PersonContactExists(personContact.PersonContactId, personContact.PersonId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPersonContact", new { id = personContact.PersonId, contactId = personContact.PersonContactId }, personContact);
        }

        // DELETE: api/PersonContact/5
        [HttpDelete("{id}/{personId}")]
        public async Task<ActionResult<PersonContact>> DeletePersonContact(int id, int personId)
        {
            var personContact = await _context.PersonContact.FirstOrDefaultAsync(e => e.PersonContactId == id && e.PersonId == personId);
            if (personContact == null)
            {
                return NotFound();
            }

            _context.PersonContact.Remove(personContact);
            await _context.SaveChangesAsync();

            return personContact;
        }

        [HttpPost]
        [Route("[action]/{id}")]
        public async Task<ActionResult<List<PersonContact>>> DeleteContacts(int id, [FromBody] List<int> ids)
        {
            var people = await _context.PersonContact.Where(e => ids.Contains(e.PersonContactId) && e.PersonId == id).ToListAsync();
            if (people == null)
            {
                return NotFound();
            }

            _context.PersonContact.RemoveRange(people);
            await _context.SaveChangesAsync();

            return people;
        }

        private bool PersonContactExists(int id, int personId)
        {
            return _context.PersonContact.Any(e => e.PersonContactId == id && e.PersonId == personId);
        }
    }
}
