using System.Threading.Tasks;
using techtest.project.Configuration.Dto;

namespace techtest.project.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
