/* ------------------------------------------------------------------------------
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * INGPJob Description:
 * ngp任务接口
 *
 * Comment 					        Revision	Date                  Author
 * -----------------------------    --------    ------------------    ----------------
 * Created							1.0		    2019/9/2 10:35:52    hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Hangfire;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGP.Framework.Core
{
    /// <summary>
    /// ngp任务接口
    /// </summary>
    public interface INGPJob
    {
        /// <summary>
        /// 服务key
        /// </summary>
        string JobKey { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task Execute(IJobCancellationToken token);
    }
}
