/* ------------------------------------------------------------------------------
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * IMobileMessageSend Description:
 * 短信发送服务接口
 *
 * Comment 					        Revision	Date                  Author
 * -----------------------------    --------    ------------------    ----------------
 * Created							1.0		    2019/9/7 19:53:09    hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 短信发送服务接口
    /// </summary>
    public interface IMobileMessageSend
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="context"></param>
        void SendMessage(MobileSendContext context);
    }
}
