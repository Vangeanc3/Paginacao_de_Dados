using Microsoft.EntityFrameworkCore;

namespace Paginacao
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opts)
            : base(opts)
        {
            
        }

        public DbSet<Todo> Todos { get; set; } = default!;
    }
} 