namespace MobileBG.Web.Hubs;

using Microsoft.AspNetCore.SignalR;

[Authorize]
public class ChatHub : Hub
{
    public async Task SendMessage(string message)
    {
        await this.Clients.Caller.SendAsync("ReceiveMessage", this.Context.User.Identity.Name, message, "out");
        await this.Clients.Others.SendAsync("ReceiveMessage", this.Context.User.Identity.Name, message, "in");
    }
}
