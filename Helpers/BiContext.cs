using Microsoft.EntityFrameworkCore;
using api_bi.Models;

namespace api_bi;

public partial class BiContext : DbContext
{
    public DbSet<Library> Libraries { get; set; }

    public DbSet<Book> Books { get; set; }

    public DbSet<User> Users { get; set; }

    public BiContext(DbContextOptions<BiContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Library>(library =>
        {
            library.ToTable("library");
            library.HasKey(p => p.id);
            library.Property(p => p.name).IsRequired();
            library.Property(p => p.location);
        });

        modelBuilder.Entity<Book>(book =>
        {
            book.ToTable("book");
            book.HasKey(p => p.id);
            book.HasOne(p => p.Library).WithMany(p => p.Books).HasForeignKey(p => p.libraryId);
            book.Property(p => p.name).IsRequired();
            book.Property(p => p.category);
            book.Property(p => p.createdAt);
        });

        modelBuilder.Entity<User>(user =>
        {
            user.ToTable("users");
            user.HasKey(p => p.id);
            user.Property(p => p.name).IsRequired();
            user.Property(p => p.email).IsRequired();
            user.Property(p => p.password).IsRequired();
            user.Property(p => p.createdAt);
        });
    }

}