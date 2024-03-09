using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QuanLyVatTuCungUngg.Models;

namespace QuanLyVatTuCungUngg.services.implement
{
    public interface IWorkService
    {
        List<Work> GetAll();
        Work GetById(long id);
        bool CreateWork(Work work);
        bool UpdateWork(Work work);
        bool DeleteWork(long id);
    }
}