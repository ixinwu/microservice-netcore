/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPHangFireStartup.cs Description:
 * 任务配置启动
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-10-23   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGP.Framework.Core.Data;
using NGP.Framework.Core.Extensions;
using NGP.Framework.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NGP.Framework.Core.Startup
{
    /// <summary>
    /// 启动时配置数据库
    /// </summary>
    public class NGPHangFireStartup : INGPStartup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<string>("ApiConfig:DocName") == "identity")
            {
                return;
            }
            services.AddHangfire( x => x.UseSqlServerStorage(ConfigurationExtensions.GetConnectionString(configuration, "DbConnection")));
        }

        /// <summary>
        /// 配置请求管道
        /// </summary>
        /// <param name="application"></param>
        public void Configure(IApplicationBuilder application)
        {
            var configuration = application.ApplicationServices.GetService<IConfiguration>();
            if (configuration.GetValue<string>("ApiConfig:DocName") == "identity")
            {
                return;
            }
            // add these
            application.UseHangfireDashboard("/ngpjob",new DashboardOptions
            {
                Authorization = new [] {new HangfireDashboardAuthorizationFilter()}
            });
            application.UseHangfireServer();

            // 启动任务
            var startupConfigurations = EngineContext.Current.Resolve<ITypeFinder>().FindClassesOfType<INGPJobStartup>();

            var instances = startupConfigurations
                .Select(startup => (INGPJobStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            foreach (var instance in instances)
                instance.Configure();
        }

        /// <summary>
        /// 加载顺序
        /// </summary>
        public int Order => 30;
    }
}