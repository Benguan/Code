using System.ComponentModel.DataAnnotations;

namespace NEG.Website.Models
{
    public class APICategory 
    {
        [Key]
        public int categoryID { get; set; }

        [StringLength(50)]
        public string categoryName { get; set; }

        public int? priority { get; set; }
    }
}