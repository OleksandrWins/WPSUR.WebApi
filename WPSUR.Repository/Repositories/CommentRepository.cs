using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPSUR.Repository.Entities;
using WPSUR.Repository.Interfaces;

namespace WPSUR.Repository.Repositories
{
    public sealed class CommentRepository : ManageableRepositoryBase<CommentEntity>, ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentRepository(ApplicationDbContext dbContext) : base(dbContext) 
        { 
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task CreateCommentAsync(CommentEntity comment)
        {
            await _dbContext.Comments.AddAsync(comment);

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(CommentEntity commentEntity)
        {
            _dbContext.Comments.Remove(commentEntity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(CommentEntity comment)
        {
            _dbContext.Comments.Update(comment);

            await _dbContext.SaveChangesAsync();
        }
    }
}
