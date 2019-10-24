using IdentityServer4;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Hosting.LocalApiAuthentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGP.Framework.Core;
using NGP.Identity.Api.Configs;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NGP.Identity.Api.IdentityExtensions
{
    /// <summary>
    /// Extension methods for the IdentityServer builder
    /// </summary>
    public static class IdentityServerBuilderExtensions
    {
        /// <summary>
        /// Add ngp identity server
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IIdentityServerBuilder AddNGPIdentityServer(this IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {

            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            var connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DbConnection");
            services.AddLocalApiAuthentication(op =>
            {
                op.SetWorkEmployee();
                return Task.FromResult(op);
            });
            var builder = services.AddIdentityServer(options =>
                {
                    options.Discovery.CustomEntries.Add("send_verification_code_endpoint", "~/connect/sendverificationcode");
                    options.Discovery.CustomEntries.Add("reset_password_endpoint", "~/connect/resetpassword");
                    options.Discovery.CustomEntries.Add("company_token_endpoint", "~/connect/companytoken");
                })
                .AddProfileService<NGPUserProfileService>()
                .AddResourceOwnerValidator<NGPResourceOwnerPasswordValidator>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString
                        , sql => sql.MigrationsAssembly(migrationsAssembly)
                        );
                    options.ApiClaim.Name = "SysAuth_ApiClaim";
                    options.ApiResourceProperty.Name = "SysAuth_ApiProperty";
                    options.ApiResource.Name = "SysAuth_ApiResource";
                    options.ApiScopeClaim.Name = "SysAuth_ApiScopeClaim";
                    options.ApiScope.Name = "SysAuth_ApiScope";
                    options.ApiSecret.Name = "SysAuth_ApiSecret";
                    options.ClientClaim.Name = "SysAuth_ClientClaim";
                    options.ClientCorsOrigin.Name = "SysAuth_ClientCorsOrigin";
                    options.ClientGrantType.Name = "SysAuth_ClientGrantType";
                    options.ClientIdPRestriction.Name = "SysAuth_ClientIdpRestriction";
                    options.ClientPostLogoutRedirectUri.Name = "SysAuth_ClientPostLogoutRedirectUri";
                    options.ClientProperty.Name = "SysAuth_ClientProperty";
                    options.ClientRedirectUri.Name = "SysAuth_ClientRedirectUri";
                    options.Client.Name = "SysAuth_Client";
                    options.ClientScopes.Name = "SysAuth_ClientScope";
                    options.ClientSecret.Name = "SysAuth_ClientSecret";
                    options.IdentityClaim.Name = "SysAuth_IdentityClaim";
                    options.IdentityResourceProperty.Name = "SysAuth_IdentityProperty";
                    options.IdentityResource.Name = "SysAuth_IdentityResource";
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString
                        , sql => sql.MigrationsAssembly(migrationsAssembly)
                        );
                    options.DeviceFlowCodes.Name = "SysAuth_DeviceCode";
                    options.PersistedGrants.Name = "SysAuth_PersistedGrant";

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                })
                .AddSigninCredentialFromConfig(configuration.GetSection("SigninKeyCredentials"), hostingEnvironment);

            return builder;
        }

        /// <summary>
        /// init identity data base
        /// </summary>
        /// <param name="app"></param>
        public static void InitializeIdentityDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                foreach (var item in context.Clients)
                {
                    context.Clients.Remove(item);
                }
                foreach (var item in context.IdentityResources)
                {
                    context.IdentityResources.Remove(item);
                }
                foreach (var item in context.ApiResources)
                {
                    context.ApiResources.Remove(item);
                }
                context.SaveChanges();
                if (!context.Clients.Any())
                {
                    foreach (var client in IdentityConfig.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }
                
                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in IdentityConfig.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
               
                if (!context.ApiResources.Any())
                {
                    foreach (var resource in IdentityConfig.GetApis())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}