/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPSingleRequestValidator Description:
 * ngp单对象请求验证
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using FluentValidation;

namespace NGP.Framework.Core.Models
{
    /// <summary>
    /// ngp单对象请求验证
    /// </summary>
    public class NGPSingleRequestValidator<T> : BaseNGPValidator<NGPSingleRequest<T>>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public NGPSingleRequestValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithStatusCode(StatusCode.EmptyError,"请求数据");

            RuleFor(x => x.RequestData)
                .NotEmpty()
                .WithStatusCode(StatusCode.EmptyError, "请求数据");
        }
    }
}
