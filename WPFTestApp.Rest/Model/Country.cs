using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFTestApp.Rest.Model
{
    [Table("country")]
    public class Country
    {
        public Country()
        {
            People = new List<Person>();
        }

        [Key]
        [Column("CODE")]
        public string Code { get; set; }

        [Column("TXT1")]
        public string Txt1 { get; set; }

        [Column("TXT2")]
        public string Txt2 { get; set; }

        [Column("TXT3")]
        public string Txt3 { get; set; }

        [Column("TXT4")]
        public string Txt4 { get; set; }

        [Column("INT_DIAL_CODE")]
        public string IntDialCode { get; set; }

        [Column("ADDR_FORMAT_ID")]
        public int AddrFormatId { get; set; }

        [Column("IS_VAT_INCLUDED")]
        public bool IsVatIncluded { get; set; }

        [Column("ACTIVE")]
        public bool Active { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
