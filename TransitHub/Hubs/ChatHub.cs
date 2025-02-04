using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransitHubRepo;
using TransitHubRepo.Models;

namespace TransitHubEFCore.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IUnitOfWork _unitOfWork;
        public ChatHub(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task OnConnectedAsync()
        {
            var userId = Context.UserIdentifier;
            if (userId != null)
            {
                var connection = _unitOfWork.Connections.FindOne(x => x.UserId == userId);
                if (connection == null)
                {
                    connection = new UserConnection
                    {
                        UserId = userId,
                        ConnectionId = Context.ConnectionId
                    };
                    _unitOfWork.Connections.Create(connection);
                }
                else 
                {
                    connection.ConnectionId = Context.ConnectionId;
                    _unitOfWork.Connections.Modifing(connection);
                }
                _unitOfWork.Commit();
            }
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.UserIdentifier;
            if (userId != null)
            {
                var connection = _unitOfWork.Connections.FindOne(uc => uc.UserId == userId);
                if (connection != null)
                {
                    _unitOfWork.Connections.Delete(connection);
                    _unitOfWork.Commit();
                }
            }
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string receiverId, string messageContent)
        {
            var senderId = Context.UserIdentifier;
            var newMessage = new Message
                {
                    Sender = senderId, 
                    MessageContent = messageContent,
                    Recever = receiverId,
                    Time = DateTime.Now,
                    IsRead = false,
                    IsDeleted = false
                };
            _unitOfWork.Messages.Create(newMessage);
            _unitOfWork.Commit();
            var receiverConnection = _unitOfWork.Connections.FindOne(uc => uc.UserId == receiverId);
            if (receiverConnection != null)
            {
                await Clients.Client(receiverConnection.ConnectionId!).SendAsync("ReceiveMessage", senderId, messageContent, newMessage.Time);
            }
        }
    }
}
