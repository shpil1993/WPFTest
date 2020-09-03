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
    public class GreetingsController : ControllerBase
    {
        private readonly test_dbContext _context;

        public GreetingsController(test_dbContext context)
        {
            _context = context;
        }

        // GET: api/Greetings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Greeting>>> GetGreeting()
        {
            return await _context.Greeting.ToListAsync();
        }

        // GET: api/Greetings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Greeting>> GetGreeting(int id)
        {
            var greeting = await _context.Greeting.FindAsync(id);

            if (greeting == null)
            {
                return NotFound();
            }

            return greeting;
        }

        // PUT: api/Greetings/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGreeting(int id, Greeting greeting)
        {
            if (id != greeting.Id)
            {
                return BadRequest();
            }

            _context.Entry(greeting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GreetingExists(id))
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

        // POST: api/Greetings
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Greeting>> PostGreeting(Greeting greeting)
        {
            _context.Greeting.Add(greeting);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGreeting", new { id = greeting.Id }, greeting);
        }

        // DELETE: api/Greetings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Greeting>> DeleteGreeting(int id)
        {
            var greeting = await _context.Greeting.FindAsync(id);
            if (greeting == null)
            {
                return NotFound();
            }

            _context.Greeting.Remove(greeting);
            await _context.SaveChangesAsync();

            return greeting;
        }

        private bool GreetingExists(int id)
        {
            return _context.Greeting.Any(e => e.Id == id);
        }
    }
}
