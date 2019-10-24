/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * TokenRequest Description:
 * 认证用户对象
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-2-28   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using NGP.Framework.Core;
using System.Runtime.Serialization;

namespace NGP.Identity.Api.Models
{
    /// <summary>
    /// 认证用户对象
    /// </summary>
    public class CompanyTokenRequest : INGPModel
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyId { get; set; }
    }
}
