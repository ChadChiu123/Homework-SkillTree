using Homework.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.Data
{
    public class MyBlogContext : DbContext
    {
        public MyBlogContext(DbContextOptions<MyBlogContext> options)
            : base(options)
        {
        }
        // 對應 AccountBook 資料表
        public DbSet<TestData> AccountBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestData>(entity =>
            {
                entity.ToTable("AccountBook");

                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            base.OnModelCreating(modelBuilder);

            // 額外設定（如果需要）
            modelBuilder.Entity<TestData>().ToTable("AccountBook");

        }

        public DbSet<TestData> Posts { get; set; }
    }
}