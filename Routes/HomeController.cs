using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace QuanLyVatTuCungUngg.Routes
{
    public class HomeController:Controller
    {
        public ActionResult Index(){
            return View();
        }
    }
}