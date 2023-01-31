using Microsoft.EntityFrameworkCore;
using WPSUR.Repository.Entities;

namespace WPSUR.Repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<UserEntity> Users { get; set; }

        public DbSet<PostEntity> Posts { get; set; }

        public DbSet<MessageEntity> Messages { get; set; }

        public DbSet<MainTagEntity>  MainTags { get; set; }

        public DbSet<SubTagEntity> SubTags { get; set; }
    }
}
