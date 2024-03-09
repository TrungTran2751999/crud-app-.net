using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.ObjectPool;
using QuanLyVatTuCungUngg.Models;
using QuanLyVatTuCungUngg.services.implement;
using QuanLyVatTuCungUngg.util;

namespace QuanLyVatTuCungUngg.services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext context;
        public UserService(ApplicationDbContext context){
            this.context = context;
        }

        public string CreateUser(User user)
        {
            var checkUser = context.User.Where(u=>u.UserName == user.UserName).FirstOrDefault();
            if(checkUser!= null) return "User is existed"; 
            context.User.Add(user);
            context.SaveChanges();
            //notificate
            var listUser = GetAll();
            string title = "Thêm mới User";
            for(int i=0; i<listUser.Count; i++){
                string body = $@"{user.UserName} được thêm mới";
                Util.NotificateAsync(title, body, listUser[i].DeviceToken);
            }
            return "OK";
        }

        public bool DeleteUser(long id)
        {
            var user = context.User.Where(user=>user.Id == id).FirstOrDefault();
            if(user==null) return false;
            context.User.Remove(user);
            context.SaveChanges();
            //notificate
            var listUser = GetAll();
            string title = "Xoa User";
            for(int i=0; i<listUser.Count; i++){
                string body = $@"{user.UserName} đã bị xóa";
                Util.NotificateAsync(title, body, listUser[i].DeviceToken);
            }
            return true;
        }

        public List<User> GetAll()
        {
            var result = context.User.ToList();
            return result;
        }

        public User GetById(long id)
        {
             var user = context.User.Where(user=>user.Id == id).FirstOrDefault();
             return user;
        }

        public bool UpdateUser(User user)
        {
            var userSelected = context.User.Where(u=>u.Id == user.Id).FirstOrDefault();
            if(userSelected==null) return false;
            userSelected.Name = user.Name;
            context.SaveChanges();
             //notificate
            var listUser = GetAll();
            string title = "Cập nhật User";
            for(int i=0; i<listUser.Count; i++){
                string body = $@"{userSelected.Name} đã cập nhật thành {user.Name}";
                Util.NotificateAsync(title, body, listUser[i].DeviceToken);
            }
            return true;
        }
        public string Login(User user){
            var checkUser = context.User.Where(u=>u.UserName==user.UserName && u.Password==user.Password).FirstOrDefault();
            if(checkUser==null) return "User invalid";
            return "OK";
        }

        public string UpdateDeviceToken(User user)
        {
            var checkUser = GetById((long)user.Id);
            if(checkUser==null) return "User Not Found";
            checkUser.DeviceToken = user.DeviceToken;
            context.SaveChanges();
            return "OK";
        }
    }
}