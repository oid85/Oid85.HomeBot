using Microsoft.EntityFrameworkCore;
using Oid85.HomeBot.Common.KnownConstants;
using Oid85.HomeBot.DataAccess.Entities;
using Oid85.HomeBot.DataAccess.Schemas;

namespace Oid85.HomeBot.DataAccess;

public class HomeBotContext(DbContextOptions<HomeBotContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
    {
        optionBuilder.UseSnakeCaseNamingConvention();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .HasDefaultSchema(KnownDatabaseSchemas.Default)
            .ApplyConfigurationsFromAssembly(
                typeof(HomeBotContext).Assembly,
                type => type
                    .GetInterface(typeof(IHomeBotSchema).ToString()) != null)
            .UseIdentityAlwaysColumns();
    }    
}