using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace techtest.project.EntityFrameworkCore
{
    public static class projectDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<projectDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<projectDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
