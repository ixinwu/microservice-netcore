/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * GlobalAnalysisDependencyRegistrar Description:
 * 全局解析依赖注入
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-28   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Autofac;
using NGP.Framework.Core;
using NGP.Framework.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Identity.Api
{
    /// <summary>
    /// 全局解析依赖注入
    /// </summary>
    public class IdentityDependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// 加载顺序
        /// </summary>
        public int Order => 2;

        /// <summary>
        /// 全局解析注入
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="typeFinder"></param>
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            builder.RegisterType<MobileMessageSend>().As<IMobileMessageSend>().InstancePerLifetimeScope();
        }
    }
}
