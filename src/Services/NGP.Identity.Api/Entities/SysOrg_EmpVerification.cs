using NGP.Framework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Identity.Api.Entities
{
    /// <summary>
    /// 用户验证码信息表
    /// </summary>
    public class SysOrg_EmpVerification : BaseDBEntity
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public string EmpId { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime? SendTime { get; set; }
        /// <summary>
        /// 验证码
        /// </summary>
        public string VerificationCode { get; set; }
    }
}
