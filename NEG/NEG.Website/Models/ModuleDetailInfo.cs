using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEG.Website.Models
{
    public class ModuleDetailInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ModuleID { get; set; }

        [StringLength(150)]
        public string ModuleName { get; set; }

        [StringLength(300)]
        public string Summary { get; set; }

        [StringLength(300)]
        public string Syntax { get; set; }

        [MaxLength]
        public string ParameterInfo { get; set; }

        [MaxLength]
        public string EventInfo { get; set; }

        [MaxLength]
        public string Example { get; set; }

        [StringLength(20)]
        public string Demokey { get; set; }

        [StringLength(1000)]
        public string ReturnValue { get; set; }

        public int CategoryID { get; set; }

        [StringLength(20)]
        public string ModuleKey { get; set; }

        public bool Status { get; set; }

        [StringLength(5)]
        public string LANG { get; set; }

        public int Priority { get; set; }
    }
}