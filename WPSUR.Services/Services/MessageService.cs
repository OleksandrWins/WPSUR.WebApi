using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Exceptions.MessagesExceptions;
using WPSUR.Services.Exceptions.UserExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Messages;
using WPSUR.Services.Models.Messages.Requests;
using WPSUR.Services.Models.Messages.Response;

namespace WPSUR.Services.Services
{
    public sealed class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatHubService _chatHubService;
        private readonly IUserRepository _userRepository;

        public MessageService(IMessageRepository messageRepository, IChatHubService chatHubService, IUserRepository userRepository)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _chatHubService = chatHubService ?? throw new ArgumentNullException(nameof(chatHubService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Guid> CreateAsync(ChatMessage chatMessage)
        {
            if (chatMessage == null)
            {
                throw new MessageValidationException($"An error occured, income data is equal to null. {nameof(chatMessage)}");
            }

            if (string.IsNullOrEmpty(chatMessage.Content))
            {
                throw new MessageValidationException("Message content is invalid.");
            }

            (UserEntity sender, UserEntity receiver) = await _userRepository.GetSenderReceiverAsync(chatMessage.UserFrom, chatMessage.UserTo);

            if (sender == null)
            {
                throw new UserDoesNotExistException("An error occured while getting user from database. Author of the message is null");
            }

            if (receiver == null)
            {
                throw new UserDoesNotExistException("An error occured while getting user form database. Message receiver is null");
            }

            Guid messageId = Guid.NewGuid();

            MessageEntity message = new()
            {
                Content = chatMessage.Content,
                Id = messageId,
                UserFrom = sender,
                UserTo = receiver,
                CreatedBy = sender,
                CreatedDate = DateTime.UtcNow,
            };

            chatMessage.CreatedDate = message.CreatedDate;
            chatMessage.Id = message.Id;

            await _messageRepository.CreateAsync(message);

            await _chatHubService.SendMessageAsync(chatMessage);

            return messageId;
        }

        public async Task DeleteMessagesAsync(ICollection<Guid> Ids)
        {
            List<MessageEntity> messages = (await _messageRepository.GetMessagesCollectionAsync(Ids)).ToList();

            if (!Ids.Any())
            {
                throw new MessageDoesNotExistException("Message doesn't exist");
            }

            UserEntity sender = messages[0].UserFrom;
            UserEntity receiver = messages[0].UserTo;

            await _messageRepository.DeleteCollectionAsync(messages, sender);

            MessageDeletionNotification deletionNotification = new()
            {
                MessageIds = Ids,
                ReceiverId = receiver.Id,
                SenderId = sender.Id,
            };

            await _chatHubService.NotifyMessageDeletion(deletionNotification);
        }

        public async Task UpdateAsync(MessageToUpdate messageToUpdate)
        {
            if (messageToUpdate == null)
            {
                throw new NullReferenceException();
            }

            if (string.IsNullOrWhiteSpace(messageToUpdate.Content))
            {
                throw new MessageValidationException("Input data is invalid.");
            }

            try
            {
                MessageEntity message = await _messageRepository.GetMessage(messageToUpdate.Id);

                MessageUpdateNotification updateNotification = new()
                {
                    MessageId = message.Id,
                    ReceiverId = message.UserTo.Id,
                    SenderId = message.UserFrom.Id,
                    Content = messageToUpdate.Content,
                    UpdatedDate = DateTime.UtcNow,
                };

                await _messageRepository.UpdateAsync(message, messageToUpdate.Content, updateNotification.UpdatedDate);

                await _chatHubService.NotifyMessageUpdate(updateNotification);
            }
            catch (NullReferenceException)
            {
                throw new MessageDoesNotExistException("Message doesn't exist.");
            }
        }
    }
}
