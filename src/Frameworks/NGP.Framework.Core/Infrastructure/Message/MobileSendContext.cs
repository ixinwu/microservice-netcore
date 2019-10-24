/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * MobileSendContext Description:
 * 手机发送上下文
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 手机发送上下文
    /// </summary>
    public class MobileSendContext : INGPModel
    {
        /// <summary>
        /// 发送号码列表
        /// </summary>
        public IList<string> Mobiles { get; set; }

        /// <summary>
        /// 发送内容
        /// </summary>
        public string Content { get; set; }
    }
}
