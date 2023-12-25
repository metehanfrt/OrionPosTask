using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrionPosTask.Models.Data;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User"); // Tablo adını belirleme

        builder
    .Property(x => x.Name)

    .HasMaxLength(50);


        builder
            .Property(x => x.UserName) 
            .IsRequired() 
            .HasMaxLength(50); 

        builder
            .Property(x => x.Password) 
            .IsRequired() 
            .HasMaxLength(100); 

        builder
            .Property(x => x.Email)
            .IsRequired() 
            .HasMaxLength(100)
            .IsUnicode(false); // Unicode desteğini kapatma
    }
}

