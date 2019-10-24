/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Startup Description:
 * 程序启动类
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NGP.Framework.Core;
using NGP.Framework.Core.HealthCheck;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Swashbuckle.AspNetCore.Swagger;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGP.ApiGateway.Base
{
    /// <summary>
    /// 程序启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="hostingEnvironment"></param>
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// config
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Hosting Environment 
        /// </summary>
        public IHostingEnvironment HostingEnvironment { get; }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 配置mvc
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
            services.AddMvcCore();

            // 注入单例
            services.AddSingleton<IHealthCheck, UseMemoryHealthCheck>();
            services.AddSingleton(Configuration);

            // 配置AppMetrics
            if (Configuration.GetValue<bool>("HasAppMetrics"))
            {
                services.RegisterAppMetrics(Configuration);                
            }
            // 配置健康检查
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddCheck<UseMemoryHealthCheck>("ngp_machine_check");
            // 配置健康检查Ui
            var hcList = new Dictionary<string, string>();
            hcList.Add("apigateway", "hc");
            hcList.Add("identity", "identity/hc");
            hcList.Add("debtbase", "debtbase/hc");
            hcList.Add("ngpanalysis", "ngpanalysis/hc");
            services.AddHealthChecksUI(setupSettings: (set) =>
            {
                var host = Configuration.GetValue<string>("HostServer");
                foreach (var item in hcList)
                {
                    set.AddHealthCheckEndpoint(item.Key, host + item.Value);
                }
                set.SetEvaluationTimeInSeconds(10);
                set.SetMinimumSecondsBetweenFailureNotifications(60);
            });

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials());
            });

            // 配置认证
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = Configuration.GetValue<string>("IdentityServer");
                options.RequireHttpsMetadata = false;
                var scopes = Configuration.GetValue<string>("ApiScopes");
                var scopeList = scopes.Split(",");
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidAudiences = scopeList
                };
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = ctx =>
                    {
                        return Task.CompletedTask;
                    },
                    OnTokenValidated = ctx =>
                    {
                        return Task.CompletedTask;
                    },

                    OnMessageReceived = ctx =>
                    {
                        return Task.CompletedTask;
                    }
                };
            });
            // 配置swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("LoanDev", new Info
                {
                    Version = "v1",
                    Title = "LoanDev2.0 API"
                });
            });
            var ocelotBuilder = services.AddOcelot(Configuration).AddPolly();
            if (Configuration.GetValue<bool>("GlobalConfiguration:UseServiceDiscovery"))
            {
                ocelotBuilder.AddConsul();
            }
        }

        /// <summary>
        /// 配置http请求管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // 配置日志
            loggerFactory.AddLog4Net();
            var configuration = app.ApplicationServices.GetService<IConfiguration>();

            // 配置AppMetrics
            if (configuration.GetValue<bool>("HasAppMetrics"))
            {
                app.ConfigureAppMetrics();
            }

            app.UseMvc();

            // 配置健康检查
            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
            app.UseHealthChecksUI(config =>
            {
                config.UIPath = "/hc-ui";
            });

            // 配置swagger
            var apiList = new List<string>()
            {
                "debtbase",
                "ngpanalysis"
            };
            app.UseSwagger()
                .UseSwaggerUI(options =>
                {
                    apiList.ForEach(apiItem =>
                    {
                        options.SwaggerEndpoint($"/{apiItem}/swagger/swagger.json", apiItem);
                    });
                });

            app.UseCors("CorsPolicy");

            // 配置网关
            app.UseOcelot().Wait();
        }
    }
}
