/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * INGPMapperProfile Description:
 * NGP 配置接口
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

namespace NGP.Framework.Core
{
    /// <summary>
    /// NGP 配置接口
    /// </summary>
    public interface INGPMapperProfile
    {
        /// <summary>
        /// 配置顺序
        /// </summary>
        int Order { get; }
    }
}
