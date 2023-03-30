using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace UlidAsChar26.Ef;
public class MyContext : DbContext
{

    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        var builder = modelBuilder.Entity<Customer>();

        builder.HasKey(a => new { a.Id });

        builder.Property(t => t.Id)
           .HasMaxLength(26).IsFixedLength(true);

        builder.Property(t => t.CustomerName)
           .HasMaxLength(200);

        var stringConverter = new UlidToStringConverter();

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(Ulid) || property.ClrType == typeof(Ulid?))
                {
                    property.SetValueConverter(stringConverter);
                }
            }
        }


    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        string connstring = "Server=localhost;Database=ULID_AS_CHAR26;User Id=postgres;Password=1234;Application Name=CobaUlid;";
        optionsBuilder.UseNpgsql(connstring);
    }



    public class UlidToStringConverter : ValueConverter<Ulid, string>
    {
        private static readonly ConverterMappingHints defaultHints = new ConverterMappingHints(size: 26);

        public UlidToStringConverter(ConverterMappingHints mappingHints = null)
            : base(
                    convertToProviderExpression: x => x.ToString(),
                    convertFromProviderExpression: x => Ulid.Parse(x),
                    mappingHints: defaultHints.With(mappingHints))
        {
        }
    }
}