namespace Todo.Api.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<Models.User>
{
    public void Configure(EntityTypeBuilder<Models.User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.FirstName)
            .HasColumnType("NVARCHAR(25)");
        
        builder.Property(x => x.LastName)
            .HasColumnType("NVARCHAR(30)");
        
        builder.Property(x => x.Email).HasColumnType("VARCHAR(255)");
        builder.Property(x => x.Password).HasColumnType("NVARCHAR(256)");

        builder.ToTable("Users");
        
    }
}
