/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ServiceCollectionExtensions Description:
 * IServiceCollection扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGP.Framework.Core.Infrastructure;
using System;
using System.Linq;
using System.Net;

namespace NGP.Framework.Core
{
    /// <summary>
    /// IServiceCollection扩展
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 向应用程序添加服务并配置服务提供程序
        /// </summary>
        /// <param name="services">服务描述符的集合</param>
        /// <param name="configuration">配置应用程序</param>
        /// <returns>配置的服务提供商</returns>
        public static IServiceProvider ConfigureServices(this IServiceCollection services, IConfiguration configuration,
            IHostingEnvironment hostingEnvironment)
        {
            // 添加TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //add accessor to HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // 文件提供者
            CommonHelper.DefaultFileProvider = new NGPFileProvider(hostingEnvironment);

            services.AddMvcCore();

            // 初始化引擎
            var engine =  EngineContext.Create();

            var serviceProvider = engine.ConfigureServices(services, configuration);

            return serviceProvider;
        }
    }
}