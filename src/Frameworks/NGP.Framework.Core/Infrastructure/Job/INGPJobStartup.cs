/* ------------------------------------------------------------------------------
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * INGPJobStartup Description:
 * 任务startup
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
    /// 任务startup
    /// </summary>
    public interface INGPJobStartup
    {
        /// <summary>
        /// 任务startup
        /// </summary>
        void Configure();

        /// <summary>
        /// 顺序
        /// </summary>
        int Order { get; }
    }
}
