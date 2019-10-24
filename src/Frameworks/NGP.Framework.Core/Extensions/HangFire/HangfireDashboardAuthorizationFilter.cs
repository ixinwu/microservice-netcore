/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * HangfireDashboardAuthorizationFilter Description:
 * 任务中间件看板认证过滤器
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Hangfire.Annotations;
using Hangfire.Dashboard;

namespace NGP.Framework.Core.Extensions
{
    /// <summary>
    /// 任务中间件看板认证过滤器
    /// </summary>
    public class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize([NotNull] DashboardContext context)
        {
            return true;
        }
    }
}
