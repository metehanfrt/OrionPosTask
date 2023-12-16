using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionPosTask.Models.Data;

namespace OrionPosTask.Models.Seeding //Codefirst propery özellikleri tanımlama
{
    public class DirectorysMap : IEntityTypeConfiguration<Directorys>
    {
        public void Configure(EntityTypeBuilder<Directorys> builder)
        {
            builder.ToTable("Directorys"); // Tablo adını belirleme

            builder
                .Property(x => x.Id) // Id özelliğini belirleme
                .ValueGeneratedOnAdd(); // Otomatik artan değer oluşturma

            builder
                .Property(x => x.Name) 
                .IsRequired() 
                .HasMaxLength(50); 

            builder
                .Property(x => x.Surname) 
                .IsRequired() 
                .HasMaxLength(50); 

            builder
                .Property(x => x.Telephone) 
                .HasMaxLength(20); 

            builder
                .HasIndex(x => x.Telephone) 
                .IsUnique(); 
        }
    }
}


