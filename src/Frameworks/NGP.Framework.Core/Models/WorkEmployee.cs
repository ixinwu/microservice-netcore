/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * WorkEmployee Description:
 * 当前工作的人员信息
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System.Collections.Generic;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 当前工作的人员信息
    /// </summary>
    public class WorkEmployee : INGPModel
    {
        /// <summary>
        /// 人员ID
        /// </summary>
        public string EmplId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 区域id
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 人员名称
        /// </summary>
        public string EmployeeName { get; set; }
        /// <summary>
        /// 是否系统admin
        /// </summary>
        public bool? IsSystemAdmin { get; set; }
        /// <summary>
        /// 所属角色id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 所属角色类型
        /// </summary>
        public string RoleType { get; set; }
        /// <summary>
        /// 部门ID
        /// </summary>
        private string deptId = string.Empty;

        /// <summary>
        /// 部门ID
        /// </summary>
        public string DeptId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(deptId))
                {
                    return string.Empty;
                }
                return deptId;
            }
            set { deptId = value; }
        }

        /// <summary>
        /// 平台Id
        /// </summary>
        private string companyId = string.Empty;
        /// <summary>
        /// 平台Id
        /// </summary>
        public string CompanyId
        {
            get
            {
                if (string.IsNullOrWhiteSpace(companyId))
                {
                    return string.Empty;
                }
                return companyId;
            }
            set
            {
                companyId = value;
            }
        }

        /// <summary>
        /// 平台id列表
        /// </summary>
        private List<string> companyIds;
        /// <summary>
        /// 平台IdList
        /// </summary>
        public List<string> CompanyIds
        {
            get
            {
                if (companyIds.IsNullOrEmpty())
                {
                    var employee = EngineContext.Current.Resolve<IUnitRepository>().FindById<SysOrg_Employee>(this.EmplId);
                    companyIds = string.IsNullOrEmpty(employee.CompanyIds) ? new List<string>() : new List<string>(employee.CompanyIds.Split(',').RemoveEmptyRepeat());
                }
                return companyIds;
            }
            set
            {
                companyIds = value;
            }
        }
        /// <summary>
        /// 人员类别
        /// </summary>
        public List<string> EmployeeTypes { get; set; }
        /// <summary>
        /// 身份类别
        /// </summary>
        public string IdentityType { get; set; }

        /// <summary>
        /// 系统默认人员
        /// </summary>
        public static WorkEmployee SysDefaultEmployee()
        {
            return new WorkEmployee
            {
                AreaId = "32",
                IsSystemAdmin = true,
                CompanyId = "System",
                EmplId = "System",
                DeptId = "System"
            };
        }
    }
}
