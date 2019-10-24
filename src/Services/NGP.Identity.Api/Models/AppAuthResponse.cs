using NGP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGP.Identity.Api.Models
{
    /// <summary>
    /// 应用认证返回
    /// </summary>
    public class AppAuthResponse : INGPModel
    {
        /// <summary>
        /// 地址标识
        /// </summary>
        public string AreaTag { get; set; }

        /// <summary>
        /// 是否过期
        /// </summary>
        public bool IsExpired { get; set; }
    }
}
