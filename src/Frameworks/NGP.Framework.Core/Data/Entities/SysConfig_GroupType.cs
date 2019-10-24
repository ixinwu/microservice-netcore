/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Sys_Config_Group_Type Description:
 * 系统小分类
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-3-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/


namespace NGP.Framework.Core
{
    /// <summary>
    /// 系统小分类
    /// </summary>
    public class SysConfig_GroupType : BaseDBEntity
    {
        /// <summary>
        /// 类别组Key
        /// </summary>
        public string GroupKey { get; set; }

        /// <summary>
        /// 类别Key
        /// </summary>
        public string TypeKey { get; set; }

        /// <summary>
        /// 类别值
        /// </summary>
        public string TypeValue { get; set; }

        /// <summary>
        /// 类别值1
        /// </summary>
        public string TypeValue1 { get; set; }

        /// <summary>
        /// 类别值2
        /// </summary>
        public string TypeValue2 { get; set; }

        /// <summary>
        /// 类别值3
        /// </summary>
        public string TypeValue3 { get; set; }

        /// <summary>
        /// 类别值4
        /// </summary>
        public string TypeValue4 { get; set; }
    }
}
