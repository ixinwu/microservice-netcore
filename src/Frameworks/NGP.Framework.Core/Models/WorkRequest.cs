/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * CurrentRequestInfo Description:
 * 当前请求信息
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

namespace NGP.Framework.Core
{
    /// <summary>
    /// 当前请求信息
    /// </summary>
    public class WorkRequest: INGPModel
    {
        /// <summary>
        /// api路径
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// api提交参数
        /// </summary>
        public string RequestBody { get; set; }

        /// <summary>
        /// api返回参数
        /// </summary>
        public string ResponseBody { get; set; }

        /// <summary>
        /// 绝对路径
        /// </summary>
        public string IpAddress { get; set; }
    }
}
