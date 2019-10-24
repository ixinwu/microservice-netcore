/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPMetricsStartup.cs Description:
 * AppMetrics配置启动
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-1   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGP.Framework.Core.Infrastructure;

namespace NGP.Framework.Core.Startup
{
    /// <summary>
    /// AppMetrics配置启动
    /// </summary>
    public class NGPMetricsStartup : INGPStartup
    {
        /// <summary>
        /// 添加AppMetrics配置启动
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("HasAppMetrics"))
            {
                services.RegisterAppMetrics(configuration);
            }
        }

        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="application"></param>
        public void Configure(IApplicationBuilder application)
        {
            var configuration = application.ApplicationServices.GetService<IConfiguration>();
            if (configuration.GetValue<bool>("HasAppMetrics"))
            {
                application.ConfigureAppMetrics();
            }
        }

        /// <summary>
        /// 配置顺序
        /// </summary>
        public int Order
        {
            get { return 1004; }
        }
    }
}