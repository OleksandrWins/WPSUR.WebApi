using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;
using WPSUR.Services.Interfaces;
using WPSUR.Services.Models.Account;
using WPSUR.Services.Models.Comment.Request;

namespace WPSUR.Services.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository) 
        {

            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        }

        public async Task<UserModel> CreateCommentAsync(CreateCommentRequest createComment, Guid commentId)
        {
            if (createComment == null)
            {
                throw new ArgumentNullException(nameof(createComment));
            }

            if (string.IsNullOrWhiteSpace(createComment.Content))
            {
                throw new Exception("Validation exception");
            }

            UserEntity user = await _userRepository.GetByIdAsync(createComment.CreatorId);
            PostEntity postEntity = await _postRepository.GetByIdAsync(createComment.TargetPostId);

            CommentEntity comment = new()
            {
                Content = createComment.Content,
                Id = commentId,
                CreatedBy = user,
                TargetPost = postEntity,
                CreatedDate = DateTime.UtcNow,
            };

            await _commentRepository.CreateCommentAsync(comment);

            UserModel responce = new()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
            };

            return responce;
        }
    }
}
