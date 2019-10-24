/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * INGPStartup Description:
 * 应用程序启动时配置服务和中间件接口
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 应用程序启动时配置服务和中间件接口
    /// </summary>
    public interface INGPStartup
    {
        /// <summary>
        /// 添加启动服务配置
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        /// <summary>
        /// 添加请求管道配置
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        void Configure(IApplicationBuilder application);

        /// <summary>
        /// 加载顺序
        /// </summary>
        int Order { get; }
    }
}
