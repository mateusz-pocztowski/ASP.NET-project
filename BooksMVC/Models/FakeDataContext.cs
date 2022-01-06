using Microsoft.EntityFrameworkCore;
using BooksApi.Models;

namespace BooksMVC.Models
{
    public class FakeContext : DbContext
    {
        public FakeContext(DbContextOptions<FakeContext> options)
            : base(options)
        {
        }

        public DbSet<BooksItem> TodoItems { get; set; }

        public DbSet<BooksApi.Models.BooksItem> BooksItem { get; set; }
    }
}
