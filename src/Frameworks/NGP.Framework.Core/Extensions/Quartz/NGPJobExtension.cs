/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPJobExtension Description:
 * ngp服务扩展
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
using NGP.Framework.Core.Quartz;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace NGP.Framework.Core
{
    /// <summary>
    /// ngp服务扩展
    /// </summary>
    public static class NGPJobExtension
    {
        /// <summary>
        /// 注册job
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterNGPJob(this IServiceCollection services)
        {
            // register job scheduler
            services.AddSingleton<IJobFactory, NGPJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            services.AddSingleton<NGPJobRunner>();
            services.AddHostedService<NGPJobHostedService>();
        }
    }
}