using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NEG.Website.Models
{
    public class APIDetailInfo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int APIID { get; set; }

        [StringLength(150)]
        public string APIName { get; set; }

        [StringLength(300)]
        public string Summary { get; set; }

        [StringLength(300)]
        public string Syntax { get; set; }

        [Column(TypeName = "Text")]
        public string ParameterInfo { get; set; }

        [Column(TypeName = "Text")]
        public string EventInfo { get; set; }

        [Column(TypeName = "Text")]
        public string Example { get; set; }

        [StringLength(20)]
        public string DemoKey { get; set; }

        [StringLength(1000)]
        public string ReturnValue { get; set; }

        public int? CategoryID { get; set; }

        [StringLength(20)]
        public string APIKey { get; set; }

        public int Status { get; set; }
    }
}