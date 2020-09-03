using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WPFTestApp.Rest.DBContext;
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

        [EnableQuery]
        [ODataRoute]
        public IQueryable<PersonContact> Get()
        {
            return _context.PersonContact;
        }
    }
}
