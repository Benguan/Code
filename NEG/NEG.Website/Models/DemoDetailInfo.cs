﻿using System.ComponentModel;
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

        [StringLength(20)]
        public string DemoName { get; set; }

        public string DemoShowParts { get; set; }

        [MaxLength]
        public string DemoCode { get; set; }

        [StringLength(3000)]
        public string HtmlCode { get; set; }

        public bool Status { get; set; }

        [StringLength(100)]
        public string ShowImage { get; set; }

        [MaxLength]
        public string ExecuteStyle { get; set; }

        [MaxLength]
        public string ExecuteScript { get; set; }
    }
}