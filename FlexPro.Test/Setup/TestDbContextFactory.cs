using FlexPro.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlexPro.Test.Setup;

public class TestDbContextFactory
{
    public static AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }
}