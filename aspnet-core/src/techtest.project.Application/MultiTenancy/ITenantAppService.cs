using Abp.Application.Services;
using techtest.project.MultiTenancy.Dto;

namespace techtest.project.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

