using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using techtest.project.Authorization;

namespace techtest.project
{
    [DependsOn(
        typeof(projectCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class projectApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<projectAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(projectApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
