using System.Data.Common;
using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Exceptions.MessagesExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models;

namespace WPSUR.Services.Services
{
    public sealed class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatHubService _chatHubService;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IChatHubService chatHubService, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _chatHubService = chatHubService;
            _userRepository = userRepository;
        }

        public async Task CreateMessageAsync(ChatMessage chatMessage)
        {
            if (chatMessage == null)
            {
                throw new MessageValidationException($"An error occured, income data is equal to null. {nameof(chatMessage)}");
            }

            if (string.IsNullOrEmpty(chatMessage.Content))
            {
                throw new MessageValidationException("Message content is invalid.");
            }

            (UserEntity sender, UserEntity receiver) = await _userRepository.GetSenderReceiver(chatMessage.UserFrom, chatMessage.UserTo);

            if (sender == null)
            {
                throw new NullReferenceException("An error occured while getting user from database. Author of the message is null");
            }

            if (receiver == null)
            {
                throw new NullReferenceException("An error occured while getting user form database. Message receiver is null");
            }

            MessageEntity message = new()
            {
                Content = chatMessage.Content,
                Id = Guid.NewGuid(),
                UserFrom = sender,
                UserTo = receiver,
                CreatedBy = sender,
                CreatedDate = DateTime.UtcNow,
            };

            await _messageRepository.CreateMessageAsync(message);

            await _chatHubService.SendMessage(chatMessage);
        }
    }
}
