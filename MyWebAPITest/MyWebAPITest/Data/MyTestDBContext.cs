using Microsoft.EntityFrameworkCore;

namespace MyWebAPITest.Data
{
    public class MyTestDBContext:DbContext
    {
        public MyTestDBContext(DbContextOptions options):base(options) { }

        #region Dbset
        public DbSet<Book> books {  get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<OrderDetail> ordersDetail { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<RefreshTokens> refreshTokens { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // định nghĩa đơn hàng
            modelBuilder.Entity<Order>(e =>
            {
                e.ToTable("Orders");
                e.HasKey(o => o.OrderID);
                e.Property(o => o.OrderDay).HasDefaultValueSql("getutcdate()");
                e.Property(o => o.ReceiverName).IsRequired().HasMaxLength(100);
            });

            // định nghĩa đơn đặt hàng
            modelBuilder.Entity<OrderDetail>(
                e =>
                {
                    e.ToTable("OrderDetail");
                    e.HasKey(od => new { od.OrderID, od.BookID });
                    e.HasOne(od => od.order)
                    .WithMany(o => o.OrderDetails)
                    .HasForeignKey(od => od.OrderID)
                    .HasConstraintName("FK_OderDetail_Order");

                    e.HasOne(od => od.book)
                      .WithMany(b => b.OrderDetails)
                      .HasForeignKey(od => od.BookID)
                      .HasConstraintName("FK_OrderDetail_Book");
                      
                });

            // định nghĩa người dùng 
            modelBuilder.Entity<User>(
                e =>
                {
                    e.HasIndex(e => e.UserName).IsUnique();
                    e.HasIndex(e => e.Email).IsUnique();
                    e.Property(e => e.FullName).IsRequired().HasMaxLength(150);
                    e.Property(e => e.Email).IsRequired().HasMaxLength(150);
                }

                );
        }
    }
}
