using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Hermes
{
    public class ChatHub : Hub
    {

    }
    
    public interface IHubClient
    {
        Task BroadcastMessage();
        Task BroadcastLike();
        Task BroadcastUpdate();
        Task BroadcastDelete();
    }
    public class BroadcastHub : Hub<IHubClient>
    {
    }
}
