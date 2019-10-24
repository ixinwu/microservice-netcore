/* ------------------------------------------------------------------------------
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * JobSchedule Description:
 * 任务计划
 *
 * Comment 					        Revision	Date                  Author
 * -----------------------------    --------    ------------------    ----------------
 * Created							1.0		    2019/2/26 10:56:02    hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 任务计划
    /// </summary>
    public class JobSchedule : INGPModel
    {   
        /// <summary>
        /// jobKey
        /// </summary>
        public string ServiceKey { get; set; }

        /// <summary>
        /// job名称
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string CronExpression { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? OrderIndex { get; set; }

        /// <summary>
        /// 有效起始时间
        /// </summary>
        public DateTime? ValidStartTime { get; set; }

        /// <summary>
        /// 有效结束时间
        /// </summary>
        public DateTime? ValidEndTime { get; set; }

        /// <summary>
        /// 策略执行方法
        /// </summary>
        public Type JobType { get; set; }     
    }
}
