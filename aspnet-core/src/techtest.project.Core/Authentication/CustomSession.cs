using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.MultiTenancy;
using Abp.Runtime.Session;
using Abp.Runtime;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace techtest.project.Authentication
{
    public class CustomSession : ClaimsAbpSession, ITransientDependency
    {
        public CustomSession(
            IPrincipalAccessor principalAccessor,
            IMultiTenancyConfig multiTenancy,
            ITenantResolver tenantResolver,
            IAmbientScopeProvider<SessionOverride> sessionOverrideScopeProvider) :
            base(principalAccessor, multiTenancy, tenantResolver, sessionOverrideScopeProvider)
        {
        }

        public string ExtToken
        {
            get
            {
                var extToken = PrincipalAccessor.Principal?.Claims.FirstOrDefault(c => c.Type == "External_Token");
                if (string.IsNullOrEmpty(extToken?.Value))
                {
                    return null;
                }

                return extToken.Value;
            }
        }
    }

}
