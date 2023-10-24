using Abp.Authorization;
using techtest.project.Authorization.Roles;
using techtest.project.Authorization.Users;

namespace techtest.project.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
