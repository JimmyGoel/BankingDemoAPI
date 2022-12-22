using ApplicationCore.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<clsUserEntity> Users { get; private set; }
        //  public DbSet<Photo> photos { get; private set; }
    }
}
