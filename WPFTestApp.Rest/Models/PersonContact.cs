using System;
using System.Collections.Generic;

namespace WPFTestApp.Rest.Models
{
    public partial class PersonContact
    {
        public int PersonId { get; set; }
        public int PersonContactId { get; set; }
        public int ContactTypeId { get; set; }
        public string Txt { get; set; }
        public string Notes { get; set; }
        public byte Active { get; set; }

        public virtual Person Person { get; set; }
    }
}
