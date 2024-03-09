using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyVatTuCungUngg.Models;

namespace QuanLyVatTuCungUngg.services.implement
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(long id);
        string CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(long id);
        string Login(User user);
        string UpdateDeviceToken(User user);
    }
}