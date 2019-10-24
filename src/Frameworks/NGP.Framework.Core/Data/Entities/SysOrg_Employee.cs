/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Org_Employee Description:
 * 雇员实体
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-3-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 雇员实体
    /// </summary>
    public class SysOrg_Employee : BaseDBEntity
    {
        /// <summary>
        /// 部门Id
        /// </summary>
        public string DeptId { get; set; }

        /// <summary>
        /// 地区Id
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 平台ids
        /// </summary>
        public string CompanyIds { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 雇员名
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 是否系统管理员
        /// </summary>
        public bool? IsSystemAdmin { get; set; }

        /// <summary>
        /// 人员类型
        /// </summary>
        public string EmployeeTypes { get; set; }

        /// <summary>
        /// 级别类型
        /// </summary>
        public string LevelType { get; set; }

        /// <summary>
        /// 身份类别
        /// </summary>
        public string IdentityType { get; set; }

        /// <summary>
        /// 最新登录时间
        /// </summary>
        public DateTime? LoginTime { get; set; }

        /// <summary>
        /// 登录失败次数
        /// </summary>
        public int? FailureTimes { get; set; }

        /// <summary>
        /// 登录成功次数
        /// </summary>
        public int? SuccessTimes { get; set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool? IsLocking { get; set; }

        /// <summary>
        /// 是否白名单
        /// </summary>
        public bool? IsWhiteList { get; set; }
        /// <summary>
        /// 是否显示
        /// </summary>
        public bool? IsShow { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool? AccountStatus { get; set; }

        /// <summary>
        /// 用户账号类型
        /// </summary>
        public string EmplAccountType { get; set; }


    }
}
