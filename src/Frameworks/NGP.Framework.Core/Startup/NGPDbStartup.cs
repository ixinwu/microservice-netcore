/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPDbStartup.cs Description:
 * db配置启动
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGP.Framework.Core.Data;
using NGP.Framework.Core.Infrastructure;
using System;

namespace NGP.Framework.Core.Startup
{
    /// <summary>
    /// 启动时配置数据库
    /// </summary>
    public class NGPDbStartup : INGPStartup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //add object context
            services.AddDbContextPool<UnitObjectContext>(optionsBuilder =>
            {
                optionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(ConfigurationExtensions.GetConnectionString(configuration, "DbConnection"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
                    //sqlOptions.UseRowNumberForPaging();
                });
            });

            //add EF services
            services.AddEntityFrameworkSqlServer();
            services.AddEntityFrameworkProxies();

            //// 配置sqlserver
            //services.AddDbContext<UnitObjectContext>(options =>
            //{
            //    options.UseSqlServer(ConfigurationExtensions.GetConnectionString(configuration, "DbConnection"),
            //    sqlServerOptionsAction: sqlOptions =>
            //    {
            //        sqlOptions.EnableRetryOnFailure(
            //        maxRetryCount: 10,
            //        maxRetryDelay: TimeSpan.FromSeconds(30),
            //        errorNumbersToAdd: null);
            //    });
            //},
            //ServiceLifetime.Scoped);
        }

        /// <summary>
        /// 配置请求管道
        /// </summary>
        /// <param name="application"></param>
        public void Configure(IApplicationBuilder application)
        {
        }

        /// <summary>
        /// 加载顺序
        /// </summary>
        public int Order => 10;
    }
}