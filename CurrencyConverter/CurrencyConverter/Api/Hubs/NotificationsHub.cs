using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

using CurrencyConverter.Data;
using CurrencyConverter.Data.Models.Users;

namespace CurrencyConverter.Api.Hubs
{
    public class NotificationsHub : Hub
    {
        private IDbContext _dbContext;

        public NotificationsHub(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SendMessage(string recieverUserId, string message)
        {
            var name = Context.User.Identity.Name;

            var user = _dbContext.Set<User>()
                .Include(x => x.Connections)
                .FirstOrDefault(x => x.UserName == recieverUserId);

            if (user == null)
            {
                Clients.Caller.SendAsync("GetErrorMessage", "Could not find that user.");
                return;
            }

            if (user.Connections == null)
            {
                Clients.Caller.SendAsync("GetErrorMessage", "The user is no longer connected.");
            }
            else
            {
                foreach (var connection in user.Connections)
                {
                    Clients.Client(connection.ConnectionID).SendAsync("GetMessage", message);
                }
            }         
        }

        public void SendMessageToAllUsers(string message)
        {
            Clients.All.SendAsync("addChatMessage", message);
        }

        public override Task OnConnectedAsync()
        {
            var name = Context.User.Identity.Name;

            var user = _dbContext.Set<User>()
                .Include(u => u.Connections)
                .SingleOrDefault(u => u.UserName == name);

            if (user == null)
            {
                user = new User
                {
                    UserName = name,
                    Connections = new List<Connection>()
                };

                _dbContext.Set<User>().Add(user);
            }

            user.Connections.Add(new Connection
            {
                ConnectionID = Context.ConnectionId,
                UserAgent = Context.Connection.UserIdentifier,
                Connected = true
            });

            _dbContext.SaveChanges();

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception ex)
        {
            var connection = _dbContext.Set<Connection>()
                .First(x=> x.ConnectionID == Context.ConnectionId);

            connection.Connected = false;

            _dbContext.SaveChanges();

            return base.OnDisconnectedAsync(ex);
        }
    }
}
