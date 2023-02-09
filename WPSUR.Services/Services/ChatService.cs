using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Exceptions.MessagesExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Chat.Request;
using WPSUR.Services.Models.Chat.Response;
using WPSUR.Services.Models.Messages;

namespace WPSUR.Services.Services
{
    public sealed class ChatService : IChatService
    {
        private readonly IMessageRepository _messageRepository;

        public ChatService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
        }
        public async Task<Chat> GetChat(GetChatServiceRequest getChatRequest)
        {
            List<MessageEntity> messages = (await _messageRepository.GetChatCollectionAsync(getChatRequest.UserToId, getChatRequest.UserFromId)).ToList();

            if (!messages.Any())
            {
                throw new MessageDoesNotExistException("Chat was not found");
            }

            List<ChatMessage> chatMessages = new();

            foreach (var message in messages)
            {
                chatMessages.Add(new ChatMessage()
                {
                    Id = message.Id,
                    Content = message.Content,
                    UserFrom = message.UserFrom.Id,
                    UserTo = message.UserTo.Id,
                    UpdatedDate = message.UpdatedData,
                    CreatedDate = message.CreatedDate,
                });
            }

            Chat chat = new()
            {
                Messages = chatMessages,
                UserFrom = messages[0].UserFrom.Id,
                UserTo = messages[0].UserTo.Id,
            };

            return chat;
        }

        public async Task<ICollection<Guid>> GetInterlocutors(Guid senderId)
        {
            List<Guid> interlocutors = (await _messageRepository.GetUserChats(senderId)).ToList();

            return interlocutors;
        }
    }
}
