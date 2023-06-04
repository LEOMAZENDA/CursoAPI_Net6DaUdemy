using Flunt.Notifications;
using IWantoApp_Project2.Domain.Products;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IWantoApp_Project2.Infra.Data.Config;

public class IWantDBContext : IdentityDbContext<IdentityUser>
{
    public IWantDBContext(DbContextOptions<IWantDBContext> options) : base(options)
    { /*Database.EnsureCreated();*/ }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }


    private string GetStringConectionConfig()
    {
        IConfigurationRoot configurationManager = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
        string strCon = configurationManager.GetConnectionString("IWantApp");

        return strCon;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(GetStringConectionConfig());
            base.OnConfiguring(optionsBuilder);
        }
    }

    //Convençoes do EF
    protected override void ConfigureConventions(ModelConfigurationBuilder configuration)
    {
        configuration.Properties<string>().HaveMaxLength(120);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);// Nao esquecer e isso resolve a
        // questão da primary key de IdentityUser quando é cobrada na migration

        builder.Ignore<Notification>();//Ignorada

        builder.Entity<Product>().Property(p => p.Desciption).HasMaxLength(255).IsRequired(false);
        builder.Entity<Product>().Property(p => p.Name).IsRequired();
        builder.Entity<Product>().Property(p => p.Name).IsRequired();
        builder.Entity<Category>().Property(p => p.Name).IsRequired();
    }


}
