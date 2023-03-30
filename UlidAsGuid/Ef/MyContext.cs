using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UlidAsGuid.Ef;
public class MyContext : DbContext
{

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var builder = modelBuilder.Entity<Customer>();

        builder.HasKey(a => new { a.Id });

        builder.Property(b => b.Id).HasColumnType("uuid");

        builder.Property(t => t.CustomerName)
           .HasMaxLength(200);

        var guidConverter = new UlidToGuidConverter();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(Ulid) || property.ClrType == typeof(Ulid?))
                {
                    property.SetValueConverter(guidConverter);
                }
            }
        }


    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        string connstring = "Server=localhost;Database=ULID_AS_GUID;User Id=postgres;Password=1234;Application Name=CobaUlid;";
        optionsBuilder.UseNpgsql(connstring);
    }

    public class UlidToGuidConverter : ValueConverter<Ulid, Guid>
    {
        private static readonly ConverterMappingHints defaultHints = new ConverterMappingHints(size: 16);

        public UlidToGuidConverter(ConverterMappingHints mappingHints = null)
            : base(
                    convertToProviderExpression: x => x.ToGuid(),
                    convertFromProviderExpression: x => new Ulid(x),
                    mappingHints: defaultHints.With(mappingHints))
        {
        }
    }

}