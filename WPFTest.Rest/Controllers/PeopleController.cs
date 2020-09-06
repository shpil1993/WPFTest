using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPFTest.Rest.DBContext;
using WPFTest.Rest.Helpers;
using WPFTest.Rest.Models;

namespace WPFTest.Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly test_dbContext _context;

        public PeopleController(test_dbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("[action]")]
        public object GetPeopleForTable(int take, int skip, int lang, string search)
        {
            var people = (from person in FilterPeople(search)
                          join country in _context.Country on person.CountryCode equals country.Code
                          join greeting in _context.Greeting on person.GreetingId equals greeting.Id
                          join contact in _context.PersonContact.Where(e => e.ContactTypeId == 1) on person.Id equals contact.PersonId into pcs
                          from pc in pcs.DefaultIfEmpty()
                          select new
                          {
                              AddressNo = person.Id,
                              Birthday = person.DateOfBirth.HasValue ? person.DateOfBirth.Value.ToString("MMMM dd, yyyy") : string.Empty,
                              Street = person.Street,
                              City = person.City,
                              Company = person.Cpny,
                              FirstName = person.Fname,
                              LastName = person.Lname,
                              PostalCode = person.Zip,
                              Registration = person.FirstContact.ToString("MMMM dd, yyyy"),
                              Title = person.Title,
                              Greeting =  new { Id = greeting.Id, Text = LocalizationChecker.CheckLocalization(lang, greeting) },
                              GreetingText = LocalizationChecker.CheckLocalization(lang, greeting),
                              Country = new { CountryCode = country.Code, CountryName = LocalizationChecker.CheckLocalization(lang, country) },
                              CountryName = LocalizationChecker.CheckLocalization(lang, country),
                              Phone = pc.Txt,
                              Gender = person.GenderId
                          });
            var count = people.Count();
            var result = people.Skip(skip * take).Take(take).ToList();
            return new { Count = count, People = result};
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.ToListAsync();
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.Person.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/People/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.Person.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Person>> DeletePerson(int id)
        {
            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.Person.Remove(person);
            await _context.SaveChangesAsync();

            return person;
        }

        // DELETE: api/People/5
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<List<Person>>> DeletePeople([FromBody] List<int> ids)
        {
            var people = await _context.Person.Where(e => ids.Contains(e.Id)).ToListAsync();
            if (people == null)
            {
                return NotFound();
            }

            _context.Person.RemoveRange(people);
            await _context.SaveChangesAsync();

            return people;
        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.Id == id);
        }

        private IQueryable<Person> FilterPeople(string search)
        {
            var people = _context.Person;

            if (string.IsNullOrEmpty(search))
            {
                return people;
            }

            return people.Where(e => e.Fname.Contains(search, System.StringComparison.OrdinalIgnoreCase)
            || e.Lname.Contains(search, System.StringComparison.OrdinalIgnoreCase)
            || e.Cpny.Contains(search, System.StringComparison.OrdinalIgnoreCase)
            || e.City.Contains(search, System.StringComparison.OrdinalIgnoreCase));
        }
    }
}
