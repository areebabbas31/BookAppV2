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
    public DbSet<New> News{ get; set; }
    public DbSet<CartProduct> CartProducts { get; set; }
    public DbSet<Cart> Carts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define the composite primary key for the CartProduct junction table
        modelBuilder.Entity<CartProduct>()
            .HasKey(cp => new { cp.CartId, cp.ProductId });  // Composite primary key

        // Define relationships between CartProduct and Cart
        modelBuilder.Entity<CartProduct>()
            .HasOne(cp => cp.Cart)  // A CartProduct has one Cart
            .WithMany(c => c.CartProducts)  // A Cart has many CartProducts
            .HasForeignKey(cp => cp.CartId);  // Foreign key to Cart

        // Define relationships between CartProduct and Product
        modelBuilder.Entity<CartProduct>()
            .HasOne(cp => cp.Product)  // A CartProduct has one Product
            .WithMany(p => p.CartProducts)  // A Product has many CartProducts
            .HasForeignKey(cp => cp.ProductId);  // Foreign key to Product

        // Optionally, you can configure cascading delete behavior if needed:
        // modelBuilder.Entity<CartProduct>()
        //    .HasOne(cp => cp.Cart)
        //    .WithMany(c => c.CartProducts)
        //    .HasForeignKey(cp => cp.CartId)
        //    .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartProduct>()
            .Property(cp => cp.CartProductId)
            .ValueGeneratedOnAdd();  // This 
    }




}

