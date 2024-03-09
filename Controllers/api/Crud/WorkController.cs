using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Presentation;
using Microsoft.AspNetCore.Mvc;
using QuanLyVatTuCungUngg.Models;
using QuanLyVatTuCungUngg.services.implement;

namespace QuanLyVatTuCungUngg.Controllers.api.Crud
{
    [ApiController]
    [Route("api/work")]
    public class WorkController:ControllerBase
    {
        private IWorkService workService;
        public WorkController(IWorkService workService){
            this.workService = workService;
        }
        [HttpGet]
        public ActionResult GetAll(){
            var result = workService.GetAll();
            return Ok(result);
        }
        [HttpGet]
        [Route("detail")]
        public ActionResult GetById(long id){
            var result = workService.GetById(id);
            if(result==null) return BadRequest("Work Not Found");
            return Ok(result); 
        }
        [HttpPost]
        public ActionResult CreateWork([FromBody] Work work){
            var result = workService.CreateWork(work);
            return Ok();
        }
        [HttpPost]
        [Route("update")]
        public ActionResult UpdateWork([FromBody] Work work){
            var result = workService.UpdateWork(work);
            if(result) return BadRequest("Work Not Found");
            return Ok();
        }
        [HttpGet]
        [Route("delete")]
        public ActionResult DeleteWork(long id){
            var result = workService.DeleteWork(id);
            if(!result) return BadRequest("Work Not Found");
            return Ok();
        }
    }
}