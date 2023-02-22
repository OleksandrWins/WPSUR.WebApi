using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
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

        public DbSet<MainTagEntity> MainTags { get; set; }

        public DbSet<SubTagEntity> SubTags { get; set; }

        public DbSet<CommentEntity> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            IEnumerable<IMutableForeignKey> foreignKeys = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(entityType => entityType.GetForeignKeys());

            foreach (IMutableForeignKey foreignKey in foreignKeys)
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
