using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEG.Website.Models
{
    public class APICategory 
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int categoryID { get; set; }

        [StringLength(50)]
        public string categoryName { get; set; }

        public int? priority { get; set; }
    }
}