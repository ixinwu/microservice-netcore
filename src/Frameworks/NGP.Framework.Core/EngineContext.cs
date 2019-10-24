/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * EngineContext Description:
 * 引擎上下文
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using NGP.Framework.Core.Infrastructure;
using System.Runtime.CompilerServices;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 引擎上下文
    /// </summary>
    public class EngineContext
    {
        #region Methods

        /// <summary>
        /// 创建单例引擎
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        {
            //create NGPEngine as engine
            return Singleton<IEngine>.Instance ?? (Singleton<IEngine>.Instance = new NGPEngine());
        }
        
        #endregion

        #region Properties

        /// <summary>
        /// 获取当前引擎
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
