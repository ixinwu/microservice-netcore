/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Config_Group Description:
 * 系统大分类
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-3-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/


namespace NGP.Framework.Core
{
    /// <summary>
    /// 系统大分类
    /// </summary>
    public class SysConfig_Group : BaseDBEntity
    {
        /// <summary>
        /// 类别组Key
        /// </summary>
        public string GroupKey { get; set; }

        /// <summary>
        /// 类别组值
        /// </summary>
        public string GroupValue { get; set; }

        /// <summary>
        /// 类别组值1
        /// </summary>
        public string GroupValue1 { get; set; }

        /// <summary>
        /// 类别组值2
        /// </summary>
        public string GroupValue2 { get; set; }

        /// <summary>
        /// 类别组值3
        /// </summary>
        public string GroupValue3 { get; set; }

        /// <summary>
        /// 类别组值4
        /// </summary>
        public string GroupValue4 { get; set; }
    }
}
