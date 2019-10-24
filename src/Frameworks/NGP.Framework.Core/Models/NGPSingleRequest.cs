/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPSingleRequest Description:
 * ngp单对象请求
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System.Linq;
using NGP.Framework.Core.Models;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using System;

namespace NGP.Framework.Core
{
    /// <summary>
    /// ngp单对象请求
    /// </summary>
    public class NGPSingleRequest<T> : INGPModel
    {
        /// <summary>
        /// 请求值
        /// </summary>
        public T RequestData { get; set; }


        public NGPResponse Validat()
        {
            var valudation = new NGPSingleRequestValidator<T>();
            var result = valudation.Validate(this);
            if (!result.IsValid)
            {
                var e = result.Errors.FirstOrDefault().ErrorMessage;

                return JsonConvert.DeserializeObject<NGPResponse>(e);
            }

            return null;
        }
    }

    /// <summary>
    /// ngp单对象请求
    /// </summary>
    public class NGPSingleRequest : NGPSingleRequest<string>
    {
    }
}
