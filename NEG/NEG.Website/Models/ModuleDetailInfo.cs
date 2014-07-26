﻿using System.ComponentModel.DataAnnotations;
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

        [StringLength(3000)]
        public string ParameterInfo { get; set; }

        [StringLength(3000)]
        public string EventInfo { get; set; }

        [Column(TypeName = "Text")]
        public string Example { get; set; }

        public int? DemoID { get; set; }

        [StringLength(1000)]
        public string ReturnValue { get; set; }

        public int CategoryID { get; set; }
    }
}