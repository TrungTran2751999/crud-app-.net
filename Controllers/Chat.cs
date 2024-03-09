using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
public class Chat:Hub{
    public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    public async Task UpdateStudent(string student)
        {
            await Clients.All.SendAsync("ReiceiveData", student);
        }
}
