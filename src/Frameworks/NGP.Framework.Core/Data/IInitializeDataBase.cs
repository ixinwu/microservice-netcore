/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * IInitializeDataBase Description:
 * 初始化数据库接口
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-4   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.Extensions.Configuration;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 初始化数据库接口
    /// </summary>
    public interface IInitializeDataBase
    {
        /// <summary>
        /// 执行数据库初始化
        /// </summary>
        /// <param name="configuration"></param>
        void Excute(IConfiguration configuration);
    }
}
