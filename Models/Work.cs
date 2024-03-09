using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace QuanLyVatTuCungUngg.Models
{
    [Table("work")]
    public class Work
    {
        [Column("id")]
        public long Id{get;set;}
        [Column("name")]
        public string? Name{get;set;}
    }
}