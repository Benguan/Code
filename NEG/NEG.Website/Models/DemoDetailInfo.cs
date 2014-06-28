using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace NEG.Website.Models
{
    public class DemoDetailInfo : DbContext
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DemoID { get; set; }

        [StringLength(150)]
        public string DemoName { get; set; }

        [StringLength(MaxLengthAttribute)]
        public string DemoShowParts { get; set; }

        [StringLength(3000)]
        public string DemoCode { get; set; }

        [StringLength(3000)]
        public string HtmlCode { get; set; }

        public bool Status { get; set; }
    }
}