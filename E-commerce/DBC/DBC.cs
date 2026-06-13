
using E_commerce.Model;
using Microsoft.EntityFrameworkCore;


namespace DataBase.DBcon
{
    public class DBC :DbContext
    {
        public DBC(DbContextOptions<DBC> options) : base(options) { }
        public DbSet<User> users { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<ChildCategory> childCategories { get; set; }
        public DbSet<Item> items { get; set; }
        public DbSet<ItemSize> itemsSize { get; set; }
        public DbSet<ItemPhoto> itemsPhoto { get; set; }
        public DbSet<CartUser> cartUsers { get; set; }
        public DbSet<OrderHistory> orderHistory { get; set; } 
        public DbSet<FileSize> fileSizes { get; set; } 
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileSize>()
               .HasOne(w => w.itemSize)
               .WithMany(f => f.fileSize)
               .HasForeignKey(w => w.itemSizeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartUser>()
               .HasOne(w => w.User)
               .WithMany(f => f.cartUser)
               .HasForeignKey(w => w.UserId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderHistory>()
              .HasOne(w => w.user)
              .WithMany(f => f.orderHistory)
              .HasForeignKey(w => w.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartUser>()
            .HasOne(w => w.itmeSize)
            .WithMany(f => f.cartUser)
            .HasForeignKey(w => w.itmeSizeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemSize>()
            .HasOne(w => w.item)
            .WithMany(f => f.itemSize)
            .HasForeignKey(w => w.itmeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemPhoto>()
            .HasOne(w => w.item)
            .WithMany(f => f.itemPhoto)
            .HasForeignKey(w => w.itmeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderHistory>()
              .HasOne(w => w.itemSize)
              .WithMany(f => f.orderHistory)
              .HasForeignKey(w => w.itemSizeId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
              .HasOne(w => w.childCategory)
              .WithMany(f => f.item)
              .HasForeignKey(w => w.childCategoryId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
               .HasOne(w => w.category)
               .WithMany(f => f.item)
               .HasForeignKey(w => w.categoryId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ChildCategory>()
              .HasOne(w => w.category)
              .WithMany(f => f.childCategory)
              .HasForeignKey(w => w.categoryId)
              .OnDelete(DeleteBehavior.Restrict);
        }
       
    }
}