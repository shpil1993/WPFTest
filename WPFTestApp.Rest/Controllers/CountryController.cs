using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using WPFTestApp.Rest.DBContext;
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

        [EnableQuery]
        [ODataRoute]
        public IQueryable<Country> Get()
        {
            return _context.Country;
        }
    }
}
