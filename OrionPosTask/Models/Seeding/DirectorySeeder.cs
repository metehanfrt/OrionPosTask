using Microsoft.EntityFrameworkCore;
using OrionPosTask.Models.Data;

namespace OrionPosTask.Models.Seeding
{
    public class DirectorySeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            var initialDirectory = new List<Directorys>()
            {
                new Directorys()
                {
                    Id = 1,
                    Name = "Metehan",
                    Surname = "Fırat",
                    Telephone = "05555555555"
                },
                new Directorys()
                {
                    Id = 2,
                    Name = "Kazım",
                    Surname = "Karabekir",
                    Telephone = "05455555555"
                },
                new Directorys()
                {
                    Id = 3,
                    Name = "Refet",
                    Surname = "Bele",
                    Telephone = "05355555555"
                },
                new Directorys()
                {
                    Id = 4,
                    Name = "İlber",
                    Surname = "Ortaylı",
                    Telephone = "05255555555"
                }
            };
            modelBuilder.Entity<Directorys>().HasData(initialDirectory);
        }
    }
}
