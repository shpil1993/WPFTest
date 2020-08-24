using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WPFTestApp.Rest.Model
{
    [Table("greeting")]
    public class Greeting : BaseEntity
    {
        public Greeting()
        {
            People = new List<Person>();
        }

        [Column("TXT1")]
        public string Txt1 { get; set; }

        [Column("TXT2")]
        public string Txt2 { get; set; }

        [Column("TXT3")]
        public string Txt3 { get; set; }

        [Column("TXT4")]
        public string Txt4 { get; set; }
        
        [Column("TXT_LETTER1")]
        public string TxtLetter1 { get; set; }

        [Column("TXT_LETTER2")]
        public string TxtLetter2 { get; set; }

        [Column("TXT_LETTER3")]
        public string TxtLetter3 { get; set; }

        [Column("TXT_LETTER4")]
        public string TxtLetter4 { get; set; }

        [Column("ACTIVE")]
        public bool Active { get; set; }

        public ICollection<Person> People { get; set; }
    }
}
