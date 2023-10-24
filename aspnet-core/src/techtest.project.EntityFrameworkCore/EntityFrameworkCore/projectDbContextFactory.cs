using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using techtest.project.Configuration;
using techtest.project.Web;

namespace techtest.project.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class projectDbContextFactory : IDesignTimeDbContextFactory<projectDbContext>
    {
        public projectDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<projectDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            projectDbContextConfigurer.Configure(builder, configuration.GetConnectionString(projectConsts.ConnectionStringName));

            return new projectDbContext(builder.Options);
        }
    }
}
