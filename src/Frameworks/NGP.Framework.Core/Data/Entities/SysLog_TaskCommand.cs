using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 服务启停日志
    /// </summary>
    public class SysLog_TaskCommand : BaseDBEntity
    {
        /// <summary>
        /// 服务Key
        /// </summary>
        public string ConfigKey { get; set; }
        /// <summary>
        /// 服务开始时间
        /// </summary>
        public DateTime TaskStratTime { get; set; }
        /// <summary>
        /// 服务结束时间
        /// </summary>
        public DateTime TaskEndTime { get; set; }
        /// <summary>
        /// 服务执行时间（秒）
        /// </summary>
        public int ExcuteTimeS { get; set; }
    }
}
