namespace Emperor_Backend
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DatabaseModel : DbContext
    {
        public DatabaseModel()
            : base("name=DatabaseModel")
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>()
                .HasMany(e => e.CartItems)
                .WithRequired(e => e.Cart1)
                .HasForeignKey(e => e.Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Cart>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Cart1)
                .HasForeignKey(e => e.Cart)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.ItemName)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.ItemDescription)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .Property(e => e.ProductImage)
                .IsUnicode(false);

            modelBuilder.Entity<MenuItem>()
                .HasMany(e => e.CartItems)
                .WithRequired(e => e.MenuItem)
                .HasForeignKey(e => e.Item)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.Zip)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.CardName)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.CardNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.ExpirationDate)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .Property(e => e.CVV)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.SessionKey)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Carts)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.User1)
                .HasForeignKey(e => e.User);
        }
    }
}
