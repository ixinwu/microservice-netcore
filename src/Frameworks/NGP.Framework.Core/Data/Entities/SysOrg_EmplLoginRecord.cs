using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 用户登录履历信息表
    /// </summary>
    public class SysOrg_EmplLoginRecord : BaseDBEntity
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string EmpId { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 人员类别
        /// </summary>
        public string EmployeeType { get; set; }
        /// <summary>
        /// 地区Id
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 是否登录成功
        /// </summary>
        public bool? IsLoginSucess { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIP { get; set; }
    }
}
