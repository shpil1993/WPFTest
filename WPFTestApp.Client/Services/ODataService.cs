using Default;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WPFTestApp.Models;

namespace WPFTestApp.Client.Services
{
    public class ODataService
    {
        private readonly Container _context;

        public ODataService()
        {
            _context = new Container(new Uri("https://localhost:5001/odata"));
        }

        public async Task<(DataServiceQueryContinuation<Person> token, IEnumerable<Model.Person> people)> GetPeopleForTableAsync(DataServiceQueryContinuation<Person> token = null)
        {
            // Get the first page
            QueryOperationResponse<Person> response;

            if (token != null)
            {
                response = await _context.ExecuteAsync(token) as QueryOperationResponse<Person>;
            }
            else
            {
                response = await _context.Person.ExecuteAsync() as QueryOperationResponse<Person>;
            }

            return (token, response.Select(e => new Model.Person()
            {
                AddressNo = e.Id,
                Birthday = e.DateOfBirth,
                Street = e.Street,
                City = e.City,
                Company = e.Cpny,
                FirstName = e.Fname,
                LastName = e.Lname,
                PostalCode = e.Zip,
                Registration = e.FirstContact,
                Title = e.Title
                //Greeting = e.Greeting.Txt1,
                //Country = e.CountryCodeNavigation.Txt1,
                //Phone = e.PersonContact.FirstOrDefault(i => i.ContactTypeId == 1)?.Txt ?? string.Empty
            }));
        }
    }
}
