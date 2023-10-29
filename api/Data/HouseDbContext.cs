using Microsoft.EntityFrameworkCore;

public class HouseDbContext : DbContext
{
    public HouseDbContext(DbContextOptions<HouseDbContext> opts) 
        : base(opts) { }

    //// the DbSet represents the Db table itself, which will have all the columns defined in the entity
    public DbSet<HouseEntity> Houses => Set<HouseEntity>(); //// the set method creates an empty DbSet
    public DbSet<BidEntity> Bids => Set<BidEntity>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        //// connection string which specifies the file name for the db
        optionsBuilder.UseSqlite($"Data Source={Path.Join(path, "gbhouses.db")}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        SeedData.Seed(modelBuilder);
    }
}