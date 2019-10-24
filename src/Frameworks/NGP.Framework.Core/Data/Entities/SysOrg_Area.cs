/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Org_Area Description:
 * 地区信息表
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-3-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/


namespace NGP.Framework.Core
{
    /// <summary>
    /// 地区信息表
    /// </summary>
    public class SysOrg_Area : BaseDBEntity
    {
        /// <summary>
        /// 地区名
        /// </summary>
        public string AreaName { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public string LevelType { get; set; }

        /// <summary>
        /// 是否为本级区域
        /// </summary>
        public bool? IsLevelArea { get; set; }
        /// <summary>
        /// 是否功能园区
        /// </summary>
        public bool? IsFunctionPark { get; set; }
        /// <summary>
        /// 是否县级市
        /// </summary>
        public bool? IsCountyCity { get; set; }
    }
}
