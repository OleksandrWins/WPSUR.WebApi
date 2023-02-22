﻿using WPSUR.Repository.Entities;

namespace WPSUR.Repository.Interfaces
{
    public interface IUserRepository : IRepositotyBase<UserEntity>
    {
        public Task<bool> IsUserExistAsync(string email);

        public Task CreateAsync(UserEntity user);

        public Task<UserEntity> GetByEmailAsync(string email);
        public Task<(UserEntity sender, UserEntity receiver)> GetSenderReceiverAsync(Guid userFromId, Guid userToId);
        Task<ICollection<UserEntity>> FindAllWithSimilarEmail(string email, Guid userId);
    }
}
