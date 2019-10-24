/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPHealthChecksStartup.cs Description:
 * 健康检查配置启动
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NGP.Framework.Core.Data;
using NGP.Framework.Core.HealthCheck;
using NGP.Framework.Core.Infrastructure;

namespace NGP.Framework.Core.Startup
{
    /// <summary>
    /// 健康检查配置启动
    /// </summary>
    public class NGPHealthChecksStartup : INGPStartup
    {
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddCheck("self", () => HealthCheckResult.Healthy())
                .AddCheck<UseMemoryHealthCheck>("ngp_machine_check")
                .AddSqlServer(ConfigurationExtensions.GetConnectionString(configuration, "DbConnection"),
                     name: "DB-check",
                     tags: new string[] { "DB" });
        }

        /// <summary>
        /// 请求管道配置
        /// </summary>
        /// <param name="application"></param>
        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            application.UseHealthChecks("/liveness", new HealthCheckOptions
            {
                Predicate = r => r.Name.Contains("self")
            });
        }

        /// <summary>
        /// 加载顺序
        /// </summary>
        public int Order => 1002;
    }
}