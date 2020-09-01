using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WPFTestApp.Rest.Models;

namespace WPFTestApp.Rest.Controllers
{
    [ODataRoutePrefix("PersonContact")]
    public class PersonContactController : ODataController
    {
        private readonly test_dbContext _context;

        public PersonContactController(test_dbContext context)
        {
            _context = context;
        }

        [EnableQuery(PageSize = 50)]
        [ODataRoute]
        public IQueryable<PersonContact> Get()
        {
            return _context.PersonContact;
        }

        // GET: api/personcontact(5, 5)
        [EnableQuery]
        [ODataRoute("({personId}, {personContactId})")]
        public async Task<IActionResult> GetPerson([FromODataUri] int personId, int personContactId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.PersonContact.FindAsync(personId, personContactId);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // POST: api/personcontact
        [ODataRoute]
        public async Task<IActionResult> Post([FromBody] PersonContact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.PersonContact.Add(contact);

            await _context.SaveChangesAsync();
            return Created(contact);
        }

        // PATCH: api/personcontact(5, 5)
        [ODataRoute("({personId}, {personContactId})")]
        public async Task<IActionResult> Patch([FromODataUri] int personId, int personContactId, [FromBody] Delta<PersonContact> updatedContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbContact = await _context.PersonContact.FindAsync(personId, personContactId);
            if (dbContact == null)
            {
                return NotFound();
            }
            updatedContact.Patch(dbContact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(personId, personContactId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Updated(dbContact);
        }

        // DELETE: api/personcontact(5, 5)
        [ODataRoute("({personId}, {personContactId})")]
        public async Task<IActionResult> Delete([FromODataUri] int personId, int personContactId)
        {
            var site = await _context.PersonContact.FindAsync(personId, personContactId);
            if (site == null)
            {
                return NotFound();
            }

            _context.PersonContact.Remove(site);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int personId, int personContactId)
        {
            return _context.PersonContact.Any(e => e.PersonId == personId && e.PersonContactId == personContactId);
        }
    }
}
