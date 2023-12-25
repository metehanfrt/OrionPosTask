using Microsoft.EntityFrameworkCore;
using OrionPosTask.Models.Seeding;

namespace OrionPosTask.Models.Data
{
    public class ApplicationDbContext : DbContext
    {
        //uygulamanın veritabanıyla nasıl etkileşimde bulunacağını belirler.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        //veri tabanı ilişkilendirmesi yapıyoruz
        public DbSet<User> Users { get; set; }

        public DbSet<Directorys> Directorys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Model Configurations
            new UserMap().Configure(modelBuilder.Entity<User>());
            new DirectorysMap().Configure(modelBuilder.Entity<Directorys>());

            //Model Seeder
            //UserSeeder.SeedData(modelBuilder);
            //DirectorySeeder.SeedData(modelBuilder);
        }
    }


    
}
