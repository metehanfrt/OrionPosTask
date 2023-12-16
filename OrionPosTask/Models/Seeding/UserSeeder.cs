using Microsoft.EntityFrameworkCore;
using OrionPosTask.Models.Data;

namespace OrionPosTask.Models.Seeding
{
    public class UserSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            var initialUsers = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    UserName = "metahanfrt",
                    Password = "Mk@1977!",
                    Email = "metehan.firat@orionpos.com.tr",
                    Name = "Metehan Fırat"
                },
                new User()
                {
                    Id = 2,
                    UserName = "muratars",
                    Password = "Mk@1977!",
                    Email = "murat.arsu@orionpos.com.tr",
                    Name = "Murat Arsu"
                },
                new User()
                {
                    Id=3,
                    UserName = "cansukrl",
                    Password = "Mk@1977!",
                    Email = "cansu.karlı@orionpos.com.tr",
                    Name = "Cansu Karlı"
                }
            };
        }
    }
}
