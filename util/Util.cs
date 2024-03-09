using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuanLyVatTuCungUngg.util.Imp;

namespace QuanLyVatTuCungUngg.util
{
    public class Util
    {
        public static async Task<string> NotificateAsync(string title, string body, string deviceToken)
        {
            ParamNotificate param = new ParamNotificate(deviceToken, title, body);
        using (HttpClient client = new HttpClient())
            {
                try
                {
                    Console.WriteLine("OKOKKKKKKKKKKKKKKKKKKKKKKKKKKKKk");
                    string apiUrl = "https://fcm.googleapis.com/fcm/send";
                    string requestBody = JsonConvert.SerializeObject(param);
                    Console.WriteLine(requestBody);
                    string accessToken = "key=AAAA0yy1RGE:APA91bFw4_v36gef3tp1_Om2_sX1T5ESri8Qi04RRgTq2OcgF_jyqFAbGe-uBEBqQPQe8Zp3fDtIPMF1jL1S71dPHudbBcJzJA-9mqMRXGbspcaXMWZRbI2lCPtFd293aitM9SaJoQ0u";
                    
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", accessToken);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");


                    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    try{
                        var response = await client.PostAsync(apiUrl, content);
                        Console.WriteLine(response);
                        return "OK";
                    }catch(Exception e){
                        return "OKCATCH";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return "catch";
                }
            }
        }
    }
}