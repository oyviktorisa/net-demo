using System.Threading.Tasks;
using Abp.Application.Services;
using techtest.project.Authorization.Accounts.Dto;

namespace techtest.project.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
