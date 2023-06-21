using BookClub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookClub.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReadBook> ReadBooks { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}