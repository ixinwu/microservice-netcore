/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPConsulStartup.cs Description:
 * consul配置启动
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGP.Framework.Core.Consul;
using NGP.Framework.Core.Infrastructure;

namespace NGP.Framework.Core.Startup
{
    /// <summary>
    /// consul配置启动
    /// </summary>
    public class NGPConsulStartup : INGPStartup
    {
        /// <summary>
        /// 添加consul配置启动
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("GlobalConfiguration:UseServiceDiscovery"))
            {
                services.RegisterConsulServices(configuration);
            }            
        }

        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="application"></param>
        public void Configure(IApplicationBuilder application)
        {

        }

        /// <summary>
        /// 配置顺序
        /// </summary>
        public int Order
        {
            get { return 1001; }
        }
    }
}