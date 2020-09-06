using System;
using System.Collections.Generic;
using System.Text;

namespace WPFTest.Client.Model
{
    public class PeopleWithCount
    {
        public int Count { get; set; }

        public List<Person> People { get; set; }
    }
}
