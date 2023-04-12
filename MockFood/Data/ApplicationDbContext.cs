using Microsoft.EntityFrameworkCore;
using MockFood.Models;

namespace MockFood.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<WingOrder> WingOrders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)    
        {
            
        }
    }
}
