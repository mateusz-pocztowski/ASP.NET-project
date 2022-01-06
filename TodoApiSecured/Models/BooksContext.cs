using Microsoft.EntityFrameworkCore;

namespace BooksApi.Models
{
    public class BooksContext : DbContext
    {
        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public DbSet<BooksItem> BooksItems { get; set; }
    }
}