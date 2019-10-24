/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Org_Department Description:
 * 部门实体
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-3-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

namespace NGP.Framework.Core
{
    /// <summary>
    /// 部门实体
    /// </summary>
    public class SysOrg_Department : BaseDBEntity
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 地区id
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        public string ParentId { get; set; }
    }
}
