using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) 
        {         
        }

        public DbSet<Activity> Activity { get; set; }
        public DbSet<Result> Result { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ResultStatus> ResultStatus { get; set; }
    }
}
