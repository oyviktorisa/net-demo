using System.Threading.Tasks;
using Abp.Application.Services;
using techtest.project.Sessions.Dto;

namespace techtest.project.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
