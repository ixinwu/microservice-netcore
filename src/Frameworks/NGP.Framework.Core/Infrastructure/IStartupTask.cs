/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * IStartupTask Description:
 * 启动时运行的任务接口
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

namespace NGP.Framework.Core.Infrastructure
{
    /// <summary>
    /// 启动时运行的任务接口
    /// </summary>
    public interface IStartupTask 
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        void Execute();

        /// <summary>
        /// 执行顺序
        /// </summary>
        int Order { get; }
    }
}
