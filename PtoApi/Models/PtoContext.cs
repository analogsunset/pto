using Microsoft.EntityFrameworkCore;

namespace PtoApi.Models
{
    public class PtoContext : DbContext
    {
        public PtoContext(DbContextOptions<PtoContext> options)
            : base(options)
        {
        }

        public DbSet<PtoItem> PtoItems { get; set; }
    }
}