/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ApplicationBuilderExtensions Description:
 * IApplicationBuilder的扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace NGP.Framework.Core
{
    /// <summary>
    /// IApplicationBuilder的扩展
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 配置应用程序HTTP请求管道
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的生成器</param>
        /// <param name="loggerFactory"></param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application,ILoggerFactory loggerFactory)
        {
            loggerFactory.AddLog4Net();

            EngineContext.Current.ConfigureRequestPipeline(application);
        }
    }
}