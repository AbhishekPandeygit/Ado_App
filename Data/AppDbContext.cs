using Microsoft.EntityFrameworkCore;

namespace ado_app.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
     
    }
}
