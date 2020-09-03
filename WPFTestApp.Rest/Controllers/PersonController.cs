using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using WPFTestApp.Rest.DBContext;
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
    }
}
