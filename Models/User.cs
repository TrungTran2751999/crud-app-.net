using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyVatTuCungUngg.Models
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public long? Id{get;set;}
        [Column("username")]
        public string? UserName{get;set;}
        [Column("password")]
        public string? Password{get;set;}
        [Column("name")]
        public string? Name{get;set;}
        [Column("deviceToken")]
        public string? DeviceToken{get;set;}
    }
}