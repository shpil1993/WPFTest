using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WPFTestApp.Rest.Models;

namespace WPFTestApp.Rest.Controllers
{
    [ODataRoutePrefix("Country")]
    public class CountryController : ODataController
    {
        private readonly test_dbContext _context;

        public CountryController(test_dbContext context)
        {
            _context = context;
        }

        [EnableQuery(PageSize = 50)]
        [ODataRoute]
        public IQueryable<Country> Get()
        {
            return _context.Country;
        }

        // GET: api/country(5)
        [EnableQuery]
        [ODataRoute("({id})")]
        public async Task<IActionResult> GetCountry([FromODataUri] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var contact = await _context.Country.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return Ok(contact);
        }
    }
}
