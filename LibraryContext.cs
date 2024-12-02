using Microsoft.EntityFrameworkCore;

namespace LibraryApplication
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        // Parametreli constructor (uygulama için)
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        // Parametresiz constructor (design-time için)
        public LibraryContext() : base(new DbContextOptionsBuilder<LibraryContext>()
            .UseSqlServer("Server=DESKTOP-O0CU96P\\LIBRARY;Database=KutuphaneDB;Trusted_Connection=True;TrustServerCertificate=True;")
            .Options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().ToTable("Books");
        }
    }
}