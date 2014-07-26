using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEG.Website.Models
{
    public class ModuleCategory 
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CategoryID { get; set; }

        [StringLength(50)]
        public string CategoryName { get; set; }

        public int? Priority { get; set; }

        public ModuleCategory()
        {
        }

        public ModuleCategory(string categroyName, int priority)
        {
            this.CategoryName = categroyName;
            this.Priority = priority;
        }
    }
}