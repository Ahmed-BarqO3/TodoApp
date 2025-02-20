namespace Todo.Api.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<Models.User>
{
    public void Configure(EntityTypeBuilder<Models.User> builder)
    {
      builder.HasKey(x=> x.Id);

        builder.Property(x => x.FirstName)
            .HasColumnType("NVARCHAR(25)");
        
        builder.Property(x => x.LastName)
            .HasColumnType("NVARCHAR(30)");
        
    }
}
