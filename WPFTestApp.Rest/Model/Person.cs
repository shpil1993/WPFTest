using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WPFTestApp.Rest.Model
{
    [Table("person")]
    public class Person : BaseEntity
    {
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Company { get; set; }

        public string Street { get; set; }

        public string CountryCode { get; set; }

        public string Zip { get; set; }

        public string City { get; set; }

        public int GenderId { get; set; }

        public int GreetingId { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime FirstContact { get; set; }

        public string Notes { get; set; }
    }
}
