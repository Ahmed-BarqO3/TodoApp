namespace Todo.Api.Data.Config;

public class TodoConfiguration : IEntityTypeConfiguration<Models.Todo>
{
    public void Configure(EntityTypeBuilder<Models.Todo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(x => x.Title).HasColumnType("NVARCHAR(255)");
        builder.Property(x => x.IsComplete)
            .HasDefaultValue(false);

        builder.Property(x => x.CreateAt)
            .HasColumnType("smalldatetime")
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();
        
        builder.HasOne(x => x.User)
            .WithMany(c => c.Todo);

        builder.ToTable("TodoItems");
    }
}
