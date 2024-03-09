using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuanLyVatTuCungUngg.Models;
using QuanLyVatTuCungUngg.services.implement;
using QuanLyVatTuCungUngg.util;
using QuanLyVatTuCungUngg.util.Imp;

namespace QuanLyVatTuCungUngg.Controllers.api
{
    [Route("api/user")]
    public class UserController:ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService){
            this.userService = userService;
        }
        [HttpGet]
        public ActionResult GetAll(){
            var result = userService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("detail")]
        public ActionResult GetById([FromQuery]long id){
            var result = userService.GetById(id);
            if(result==null) return BadRequest("User Not Found");
            return Ok(result);
        }
        [HttpPost]
        public ActionResult CreateUser([FromBody] User user){
            var result = userService.CreateUser(user);
            if(result!="OK") return BadRequest(result);
            return Ok(result);
        }
        [HttpPost]
        [Route("update")]
        public ActionResult UpdateUser([FromBody] User user){
            var result = userService.UpdateUser(user);
            if(!result) return BadRequest("User Not Found");
            return Ok(result);
        }
        [HttpGet]
        [Route("delete")]
        public ActionResult DeleteUser(long id){
            var result = userService.DeleteUser(id);
            if(!result) return BadRequest("User Not Found");
            return Ok(result);
        }
        [HttpPost]
        [Route("login")]
        public ActionResult Login([FromBody] User user){
            var result = userService.Login(user);
            if(result!="OK") return BadRequest(result);
            return Ok(result);
        }
        [HttpPost]
        [Route("deviceToken")]
        public ActionResult UpdateDeviceToken([FromBody] User user){
            var result = userService.UpdateDeviceToken(user);
            if(result==null) return BadRequest("User not found");
            return Ok(result);
        }
        [HttpPost]
        [Route("notificate")]
        public ActionResult Notificate([FromBody]NotificateParam notificate){
             var result = Util.NotificateAsync(notificate.title, notificate.body, notificate.deviceToken);
             return Ok(result);               
        }
        public class NotificateParam{
            public string title{get;set;}
            public string body{get;set;}
            public string deviceToken{get;set;}
        }   
    }
}