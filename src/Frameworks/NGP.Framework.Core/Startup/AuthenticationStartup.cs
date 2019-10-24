/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * AuthenticationStartup Description:
 * 身份认证启动器
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGP.Framework.Core.Startup
{
    /// <summary>
    /// 身份认证启动器
    /// </summary>
    public class AuthenticationStartup : INGPStartup
    {
        /// <summary>
        /// 添加和配置身份认证
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<string>("ApiConfig:DocName") == "identity")
            {
                return;
            }
            var identityUrl = configuration.GetValue<string>("IdentityServer");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudiences = new[] {
                           configuration.GetValue<string>("ApiConfig:Audience")
                       }
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = ctx =>
                    {
                        ctx.Principal.SetWorkEmployee();
                        //ctx.HttpContext.SetWorkRequest();
                        return Task.CompletedTask;
                    },
                };
            });
        }

        /// <summary>
        /// 配置添加中间件的使用
        /// </summary>
        /// <param name="application"></param>
        public void Configure(IApplicationBuilder application)
        {
            // 配置身份认证并且添加身份认证中间件
            application.UseAuthentication();
        }

        /// <summary>
        /// 配置启动顺序
        /// </summary>
        public int Order
        {
            //应在api之前加载身份验证
            get { return 500; }
        }
    }
}
