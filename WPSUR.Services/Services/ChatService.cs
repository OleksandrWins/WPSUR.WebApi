using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Exceptions.UserExceptions;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Chat;
using WPSUR.Services.Models.Chat.Request;
using WPSUR.Services.Models.Chat.Response;
using WPSUR.Services.Models.Messages;

namespace WPSUR.Services.Services
{
    public sealed class ChatService : IChatService
    {
        private readonly IMessageRepository _messageRepository;

        private readonly IUserRepository _userRepository;

        public ChatService(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<ICollection<ChatsResponse>> FindChats(string receiverEmail, Guid userId)
        {
            if (string.IsNullOrWhiteSpace(receiverEmail))
            {
                throw new EmailValidationException();
            }

            receiverEmail = receiverEmail.ToLower();

            ICollection<UserEntity> receivers = await _userRepository.FindAllWithSimilarEmail(receiverEmail, userId);

            ICollection<ChatsResponse> findedInterlocuters = receivers.Select(receiver => new ChatsResponse
            {
                UserEmail = receiver.Email,
                UserId = receiver.Id,
                UserToFirstName = receiver.FirstName,
                UserToLastName = receiver.LastName,
            }).ToList();

            return findedInterlocuters;
        }
        public async Task<Chat> GetChat(GetChatServiceRequest getChatRequest)
        {
            List<MessageEntity> messages = (await _messageRepository.GetChatCollectionAsync(getChatRequest.UserToId, getChatRequest.UserFromId)).ToList();

            (ChatUser UserTo, ChatUser UserFrom) = (new(), new());

            if (!messages.Any())
            {
                (UserEntity sender, UserEntity receiver) = await _userRepository.GetSenderReceiverAsync(getChatRequest.UserFromId, getChatRequest.UserToId);

                UserTo = new()
                {
                    UserFirstName = receiver.FirstName,
                    UserId = receiver.Id,
                    UserLastName = receiver.LastName,
                };

                UserFrom = new()
                {
                    UserFirstName = sender.FirstName,
                    UserLastName = sender.LastName,
                    UserId = sender.Id,
                };
            }

            messages = messages.OrderBy(message => message.CreatedDate).ToList();

            List<ChatMessage> chatMessages = new();

            foreach (MessageEntity message in messages)
            {
                if (string.IsNullOrWhiteSpace(UserTo.UserFirstName) && message.UserTo.Id == getChatRequest.UserToId)
                {
                    UserTo = new()
                    {
                        UserFirstName = message.UserTo.FirstName,
                        UserLastName = message.UserTo.LastName,
                        UserId = message.UserTo.Id
                    };

                    UserFrom = new()
                    {
                        UserFirstName = message.UserFrom.FirstName,
                        UserLastName = message.UserFrom.LastName,
                        UserId = message.UserFrom.Id
                    };
                }

                if (string.IsNullOrWhiteSpace(UserTo.UserFirstName) && message.UserTo.Id == getChatRequest.UserFromId)
                {
                    UserFrom = new()
                    {
                        UserFirstName = message.UserTo.FirstName,
                        UserLastName = message.UserTo.LastName,
                        UserId = message.UserTo.Id
                    };

                    UserTo = new()
                    {
                        UserFirstName = message.UserFrom.FirstName,
                        UserLastName = message.UserFrom.LastName,
                        UserId = message.UserFrom.Id
                    };
                }

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
                Sender = UserFrom,
                Receiver = UserTo,
            };

            return chat;
        }

        public async Task<ICollection<ChatsResponse>> GetInterlocutors(Guid senderId)
        {
            ICollection<MessageEntity> userMessagesCollection = await _messageRepository.GetUserMessagesAsync(senderId);

            List<ChatsResponse> chatResponseCollection = new();

            foreach (MessageEntity message in userMessagesCollection)
            {
                if (senderId != message.UserFrom.Id)
                {
                    chatResponseCollection.Add(new()
                    {
                        UserId = message.UserFrom.Id,
                        UserToFirstName = message.UserFrom.FirstName,
                        UserToLastName = message.UserFrom.LastName,
                        UserEmail = message.UserFrom.Email,
                    });
                }

                if (senderId != message.UserTo.Id)
                {
                    chatResponseCollection.Add(new()
                    {
                        UserId = message.UserTo.Id,
                        UserToFirstName = message.UserTo.FirstName,
                        UserToLastName = message.UserTo.LastName,
                        UserEmail = message.UserTo.Email,
                    });
                }
            }

            chatResponseCollection = chatResponseCollection.DistinctBy(chat => chat.UserId).ToList();

            return chatResponseCollection;
        }
    }
}
