/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ConsulServiceDiscoveryHostedService Description:
 * Consul服务发现
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Consul;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NGP.Framework.Core.Consul
{
    /// <summary>
    /// Consul服务发现
    /// </summary>
    public class ConsulServiceDiscoveryHostedService : IHostedService
    {
        /// <summary>
        /// consul client
        /// </summary>
        private readonly IConsulClient _client;

        /// <summary>
        /// 当前配置
        /// </summary>
        private readonly ConsulServiceConfig _config;

        /// <summary>
        /// 注册id
        /// </summary>
        private string _registrationId;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="client"></param>
        /// <param name="config"></param>
        public ConsulServiceDiscoveryHostedService(IConsulClient client, ConsulServiceConfig config)
        {
            _client = client;
            _config = config;
        }

        /// <summary>
        /// start
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _registrationId = $"{_config.ServiceName}-{DateTime.Now.ToString("yyyyMMddHHmmss")}";

            var registration = new AgentServiceRegistration
            {
                ID = _registrationId,
                Name = _config.ServiceName,
                Address = _config.ServiceAddress.Host,
                Port = _config.ServiceAddress.Port,
                Check = new AgentServiceCheck()
                {
                    // 服务启动多久后注册
                    DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(15),
                    // 健康检查时间间隔
                    Interval = TimeSpan.FromSeconds(15),
                    // 健康检查地址
                    HTTP = $"http://{_config.ServiceAddress.Host}:{_config.ServiceAddress.Port}/hc",
                    // 超时
                    Timeout = TimeSpan.FromSeconds(10)
                }
            };
            
            await _client.Agent.ServiceDeregister(registration.ID, cancellationToken);
            await _client.Agent.ServiceRegister(registration, cancellationToken);
        }

        /// <summary>
        /// stop
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.Agent.ServiceDeregister(_registrationId, cancellationToken);
        }
    }
}