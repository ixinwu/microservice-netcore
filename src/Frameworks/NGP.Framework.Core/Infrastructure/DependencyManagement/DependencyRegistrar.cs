/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * DependencyRegistrar Description:
 * 依赖注入
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-28   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NGP.Framework.Core.Data;
using NGP.Framework.Core.HealthCheck;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace NGP.Framework.Core.Infrastructure
{
    /// <summary>
    /// 依赖注入
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// 注入顺序
        /// </summary>
        public int Order => 1;

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<AdoRepository>().As<IAdoRepository>().SingleInstance();

            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            builder.RegisterType<NGPFileProvider>().As<INGPFileProvider>().InstancePerLifetimeScope();
            builder.RegisterType<WorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            builder.Register(context => new UnitObjectContext(context.Resolve<DbContextOptions<UnitObjectContext>>()))
                .As<IDbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitRepository>().As<IUnitRepository>().InstancePerLifetimeScope();

            builder.RegisterType<UseMemoryHealthCheck>().As<IHealthCheck>().SingleInstance();
        }
    }
}
