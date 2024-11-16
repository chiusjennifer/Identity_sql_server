using Identity_sql_server.Models;
using Microsoft.EntityFrameworkCore;

namespace Identity_sql_server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Customers> customers { get; set; }
        public DbSet<Meals> Meals { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Order_items> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 設定 Orders 和 Order_items 之間的一對多關係
            modelBuilder.Entity<Order_items>()
                .HasOne(oi => oi.Order)           // 導航屬性
                .WithMany(o => o.Order_Items)      // Orders 中的集合
                .HasForeignKey(oi => oi.OrderId)   // 外來鍵
                .OnDelete(DeleteBehavior.Cascade); // 刪除訂單時，會刪除相關訂單細項

            // 設定 Meals 和 Order_items 之間的一對多關係
            modelBuilder.Entity<Order_items>()
                .HasOne(oi => oi.Meal)             // 導航屬性
                .WithMany()                        // Meal 不需要導航到 Order_items
                .HasForeignKey(oi => oi.meal_id)    // 外來鍵
                .OnDelete(DeleteBehavior.Restrict); // 禁止刪除 Meal 時，刪除相關的 Order_items
        }
    }
}
