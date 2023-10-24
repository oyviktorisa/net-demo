using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace techtest.project.Controllers
{
    public abstract class projectControllerBase: AbpController
    {
        protected projectControllerBase()
        {
            LocalizationSourceName = projectConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
