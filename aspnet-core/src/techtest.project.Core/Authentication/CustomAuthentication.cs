using Abp;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using techtest.project.Authorization.Users;
using techtest.project.MultiTenancy;

namespace techtest.project.Authentication
{
    public class CustomAuthentication : DefaultExternalAuthenticationSource<Tenant, User>, ITransientDependency
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomAuthentication(IConfiguration configuration, IHttpClientFactory httpClientFactory)
            : base()
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
        }

        public override string Name => "CustomAuthentication";

        public override async Task<bool> TryAuthenticateAsync(string userNameOrEmailAddress, string plainPassword, Tenant tenant)
        {
            string api = _configuration.GetSection("AuthAPI").Value;

            try
            {
                HttpClient httpClient = _httpClientFactory.CreateClient();

                JObject obj = new JObject();
                obj.Add("email", userNameOrEmailAddress);
                obj.Add("password", plainPassword);
                HttpContent content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(api, content);

                if(response.IsSuccessStatusCode)
                {
                    return true;
                }

            }
            catch (Exception ex)
            {
                throw new AbpException(ex.Message);
            }

            return false;
        }
    }
}
