using System.ComponentModel.DataAnnotations.Schema;

namespace WPFTestApp.Rest.Model
{
    public abstract class BaseEntity
    {
        [Column("ID")]
        public int Id { get; set; }
    }
}
