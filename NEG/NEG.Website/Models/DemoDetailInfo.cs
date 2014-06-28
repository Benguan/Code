using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace NEG.Website.Models
{
    public class DemoDetailInfo:DbContext
    {
        [Key]
        public int DemoID { get; set; }

        [StringLength(150)]
        public string DemoName { get; set; }

        [StringLength(1000)]
        public string DemoShowParts { get; set; }

        [StringLength(3000)]
        public string DemoCode { get; set; }

        [StringLength(3000)]
        public string HtmlCode { get; set; }
    }
}