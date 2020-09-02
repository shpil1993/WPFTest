using Default;
using Microsoft.OData.Client;
using System;
using System.Collections.Generic;
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

        public IEnumerable<Person> GetPeopleForTableAsync()
        {

        }
    }
}
