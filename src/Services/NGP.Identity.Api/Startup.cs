/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Startup Description:
 * 系统启动函数
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NGP.Framework.Core;
using NGP.Identity.Api.IdentityExtensions;
using System;
using System.Threading.Tasks;

namespace NGP.Identity.Api
{
    /// <summary>
    /// 系统启动函数
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// host环境变量
        /// </summary>
        public IHostingEnvironment Environment { get; }
        /// <summary>
        /// 获取应用程序的配置
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="environment"></param>
        /// <param name="configuration"></param>
        public Startup(IHostingEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // 配置身份认证服务
            var builder = services.AddNGPIdentityServer(Configuration, Environment);
            var serviceProvider = builder.Services.ConfigureServices(Configuration, Environment);
            return serviceProvider;
        }

        /// <summary>
        /// 配置http管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.InitializeIdentityDatabase();
            
            app.ConfigureRequestPipeline(loggerFactory);           
            
            app.UseIdentityServer();
        }
    }
}
