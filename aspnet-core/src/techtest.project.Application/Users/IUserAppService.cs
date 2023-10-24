using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using techtest.project.Roles.Dto;
using techtest.project.Users.Dto;

namespace techtest.project.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);

        Task<bool> ChangePassword(ChangePasswordDto input);
    }
}
