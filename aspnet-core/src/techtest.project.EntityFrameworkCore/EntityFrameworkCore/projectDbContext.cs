using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using techtest.project.Authorization.Roles;
using techtest.project.Authorization.Users;
using techtest.project.MultiTenancy;

namespace techtest.project.EntityFrameworkCore
{
    public class projectDbContext : AbpZeroDbContext<Tenant, Role, User, projectDbContext>
    {
        /* Define a DbSet for each entity of the application */
        
        public projectDbContext(DbContextOptions<projectDbContext> options)
            : base(options)
        {
        }
    }
}
