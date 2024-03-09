using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyVatTuCungUngg.util.Imp
{
    public interface IUtil
    {
        Task<ResultNotificate> NotificateAsync(string title, string body);
    }
    //resquest
    public class ParamNotificate{
        public string to{get;set;}
        public string priority = "high";
        public Notificate notification{get;set;}
        public Data data{get;set;}
        public ParamNotificate(string deviceToken, string title, string body){
            this.to = deviceToken;
            Notificate notificated = new Notificate(title, body);
            this.notification = notificated;
        }
    }
    public class Data{
        public string id{get;set;}
        public string name{get;set;}
    }
    public class Notificate{
        public string title{get;set;}
        public string body{get;set;}
        public Notificate(string title, string body){
            this.title = title;
            this.body = body;
        }
    }
    //response
    public class ResultNotificate{
        public string multicast_id{get;set;}
        public string success{get;set;}
        public string failure{get;set;}
        public string canonical_ids{get;set;}
        public List<MessageId> results {get;set;}
    }
    public class MessageId{
        public string message_id{get;set;}
    }
}