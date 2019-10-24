/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ConsulServiceDiscoveryExtension Description:
 * Consul服务发现扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;
using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NGP.Framework.Core.Consul;

namespace NGP.Framework.Core
{
    /// <summary>
    /// Consul服务发现扩展
    /// </summary>
    public static class ConsulServiceDiscoveryExtension
    {
        /// <summary>
        /// 注册Consul
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterConsulServices(this IServiceCollection services, IConfiguration configuration)
        {
            
            var key = configuration.GetValue<string>("ApiConfig:Audience");
            var serviceConfig = new ConsulServiceConfig
            {
                ServiceDiscoveryAddress = configuration.GetValue<Uri>($"ConsulServiceConfig:{key}:serviceDiscoveryAddress"),
                ServiceAddress = configuration.GetValue<Uri>($"ConsulServiceConfig:{key}:serviceAddress"),
                ServiceName = configuration.GetValue<string>($"ConsulServiceConfig:{key}:serviceName"),
            };
            
            services.AddSingleton(serviceConfig);
            services.AddHostedService<ConsulServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(p => new ConsulClient(config =>
            {
                config.Address = serviceConfig.ServiceDiscoveryAddress;
            }));
        }
    }
}