/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPException Description:
 * ngp异常
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019/8/20 13:13:18   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;

namespace NGP.Framework.Core
{
    /// <summary>
    /// ngp异常
    /// </summary>
    [Serializable]
    public class NGPException : Exception
    {
        /// <summary>
        /// 状态Code
        /// </summary>
        public StatusCode StatusCode { get; set; }

        /// <summary>
        /// ctor
        /// </summary>
        public NGPException()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="formatParameters"></param>
        public NGPException(StatusCode statusCode = StatusCode.SystemException, params object[] formatParameters)
            : base(statusCode.Message(formatParameters))
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="formatParameters"></param>
        public NGPException(string message)
            : base(message)
        {
            StatusCode = StatusCode.SystemException;
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="innerException"></param>
        /// <param name="statusCode"></param>
        /// <param name="formatParameters"></param>
        public NGPException(Exception innerException, StatusCode statusCode = StatusCode.SystemException,
            params object[] formatParameters)
            : base(statusCode.Message(formatParameters), innerException)
        {
            StatusCode = statusCode;
        }
    }
}
