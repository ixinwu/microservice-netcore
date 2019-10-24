/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Org_Role_Empl Description:
 * 雇员角色关联
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-1-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

namespace NGP.Framework.Core
{
    /// <summary>
    /// 雇员角色关联
    /// </summary>
    public class SysOrg_RoleEmpl : BaseDBEntity
    {
        /// <summary>
        /// 雇员id
        /// </summary>
        public string EmplId { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }
    }
}
