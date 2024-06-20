using MemoirLane.Model;
using MemoirLane.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MemoirLane.DataConnection
{
    public class DbContextClass : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DbContextClass(DbContextOptions<DbContextClass> options, IConfiguration configuration)
            : base(options)
        {
            Configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Login>().HasNoKey(); // Configure Login as a keyless entity

            modelBuilder.Entity<EntryTag>()
                .HasKey(et => new { et.EntryId, et.TagId });

            modelBuilder.Entity<EntryTag>()
                .HasOne(et => et.Entry)
                .WithMany(e => e.EntryTags)
                .HasForeignKey(et => et.EntryId);

            modelBuilder.Entity<EntryTag>()
                .HasOne(et => et.Tag)
                .WithMany(t => t.EntryTags)
                .HasForeignKey(et => et.TagId);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<EntryTag> EntryTags { get; set; }
        public DbSet<Login> Logins { get; set; }
    }
}
