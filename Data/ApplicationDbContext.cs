using Microsoft.EntityFrameworkCore;
using Bulky.Models;



namespace BulkyBookWeb.Data;

public class ApplicationDbContext : DbContext


{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
   public DbSet<Order> Orders{ get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }
    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cart>()
          .HasOne(c => c.User)                 // A Cart belongs to one User
          .WithMany(u => u.Carts)              // A User can have many Carts
          .HasForeignKey(c => c.UserId);       // The foreign key in Cart is UserId

        // Cart ↔ Product (many-to-many via CartProduct)
        modelBuilder.Entity<CartProduct>()
            .HasKey(cp => new { cp.CartId, cp.ProductId }); // Composite key for CartProduct

        modelBuilder.Entity<CartProduct>()
            .HasOne(cp => cp.Cart)                      // CartProduct has one Cart
            .WithMany(c => c.CartProducts)              // Cart has many CartProducts
            .HasForeignKey(cp => cp.CartId);            // Foreign key in CartProduct is CartId

        modelBuilder.Entity<CartProduct>()
            .HasOne(cp => cp.Product)                   // CartProduct has one Product
            .WithMany(p => p.CartProducts)              // Product can appear in many CartProducts
            .HasForeignKey(cp => cp.ProductId);         // Foreign key in CartProduct is ProductId

        modelBuilder.Entity<OrderProduct>()
         .HasKey(op => new { op.OrderId, op.ProductId });

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Order)
            .WithMany(o => o.OrderProducts)
            .HasForeignKey(op => op.OrderId);

        modelBuilder.Entity<OrderProduct>()
            .HasOne(op => op.Product)
            .WithMany(p => p.OrderProducts)
            .HasForeignKey(op => op.ProductId);
    }




}

