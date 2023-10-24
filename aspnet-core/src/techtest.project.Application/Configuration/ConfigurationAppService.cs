using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using techtest.project.Configuration.Dto;

namespace techtest.project.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : projectAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
