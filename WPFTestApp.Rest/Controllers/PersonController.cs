using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPFTestApp.Rest.Models;

namespace WPFTestApp.Rest.Controllers
{
    [ODataRoutePrefix("Person")]
    public class PersonController : ODataController
    {
        private readonly test_dbContext _context;

        public PersonController(test_dbContext context)
        {
            _context = context;
        }

        [EnableQuery(PageSize = 50)]
        [ODataRoute]
        public IQueryable<Person> Get()
        {
            return _context.Person;
        }

        // GET: api/person(5)
        [EnableQuery]
        [ODataRoute("({id})")]
        public async Task<IActionResult> GetPerson([FromODataUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Person.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }

        // POST: api/person
        [ODataRoute]
        public async Task<IActionResult> Post([FromBody] Person contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Person.Add(contact);

            await _context.SaveChangesAsync();
            return Created(contact.Id.ToString(), contact);
        }

        // PATCH: api/person(5)
        [ODataRoute("({id})")]
        public async Task<IActionResult> Patch([FromODataUri] int id, [FromBody] Delta<Person> updatedContact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var dbContact = await _context.Person.FindAsync(id);
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
                if (!ContactExists(id))
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

        // DELETE: api/person(5)
        [ODataRoute("({id})")]
        public async Task<IActionResult> Delete([FromODataUri] int id)
        {
            var site = await _context.Person.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.Person.Remove(site);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }
    }
}
