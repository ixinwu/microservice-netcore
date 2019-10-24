/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * UseMemoryHealthCheck Description:
 * 使用内存健康检查
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NGP.Framework.Core.HealthCheck
{
    /// <summary>
    /// 使用内存健康检查
    /// </summary>
    public class UseMemoryHealthCheck : IHealthCheck
    {
        /// <summary>
        /// config
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="config"></param>
        public UseMemoryHealthCheck(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// chack name
        /// </summary>
        public string Name => "ngp_machine_check";

        /// <summary>
        /// 健康检查判断
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var memoryThreshold = _config.GetValue<long>("MachineHealthCheck:MemoryThreshold");

            // Include GC information in the reported diagnostics.
            var allocated = GC.GetTotalMemory(forceFullCollection: false);
            var data = new Dictionary<string, object>()
            {
                { "AllocatedBytes", allocated },
                { "Gen0Collections", GC.CollectionCount(0) },
                { "Gen1Collections", GC.CollectionCount(1) },
                { "Gen2Collections", GC.CollectionCount(2) },
            };

            var status = (allocated < memoryThreshold) ?
                HealthStatus.Healthy : HealthStatus.Unhealthy;

            return Task.FromResult(new HealthCheckResult(
                status,
                description: "Reports degraded status if allocated bytes " +
                    $">= {memoryThreshold} bytes.",
                exception: null,
                data: data));
        }
    }
}
