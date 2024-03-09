using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyVatTuCungUngg.Models;
using QuanLyVatTuCungUngg.services.implement;
using QuanLyVatTuCungUngg.util;

namespace QuanLyVatTuCungUngg.services
{
    public class WorkService : IWorkService
    {
        private readonly ApplicationDbContext context;
        private IUserService userService;
        public WorkService(ApplicationDbContext context, IUserService userService){
            this.context = context;
            this.userService = userService;
        }

        public bool CreateWork(Work work)
        {
            context.Work.Add(work);
            context.SaveChanges();
            //notificate
            var listUser = userService.GetAll();
            string title = "Thêm mới Công việc";
            for(int i=0; i<listUser.Count; i++){
                string body = $@"{work.Name} được thêm mới";
                Util.NotificateAsync(title, body, listUser[i].DeviceToken);
            }
            return true;
        }

        public bool DeleteWork(long id)
        {
            var work = context.Work.Where(w=>w.Id==id).FirstOrDefault();
            if(work!=null){
                context.Work.Remove(work);
                context.SaveChanges();
                //notificate
                var listUser = userService.GetAll();
                string title = "Xóa Công việc";
                for(int i=0; i<listUser.Count; i++){
                    string body = $@"{work.Name} đã được xóa";
                    Util.NotificateAsync(title, body, listUser[i].DeviceToken);
                }
                return true;
            }else{
                return false;
            }
        }

        public List<Work> GetAll()
        {
            var result = context.Work.ToList();
            return result;
        }

        public Work GetById(long id)
        {
            var work = context.Work.Where(work=>work.Id==id).FirstOrDefault();
            return work;
        }

        public bool UpdateWork(Work work)
        {
           var workSelected = context.Work.Where(w=>w.Id==work.Id).FirstOrDefault();
           if(workSelected==null) return false;
           workSelected.Name = work.Name;
           context.SaveChanges();
           //notificate
            var listUser = userService.GetAll();
            string title = "Cập nhật Công việc";
            for(int i=0; i<listUser.Count; i++){
                string body = $@"{work.Name} được thêm mới";
                Util.NotificateAsync(title, body, listUser[i].DeviceToken);
            }
           return true;
        }
    }
}