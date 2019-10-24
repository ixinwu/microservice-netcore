/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPPageQueryRequestValidator Description:
 * ngp分页对象请求验证
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
    /// ngp分页对象请求验证
    /// </summary>
    public class NGPPageQueryRequestValidator<T> : BaseNGPValidator<NGPPageQueryRequest<T>> where T : INGPModel
    {
        /// <summary>
        /// ctor
        /// </summary>
        public NGPPageQueryRequestValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithStatusCode(StatusCode.EmptyError, "请求数据");

            RuleFor(x => x.RequestData)
                .NotNull()
                .WithStatusCode(StatusCode.EmptyError, "请求数据")
                .NotEmpty()
                .WithStatusCode(StatusCode.EmptyError, "请求数据");

            RuleFor(x => x.PageNumber)
                .NotEmpty()
                .WithStatusCode(StatusCode.EmptyError,"页数");

            RuleFor(x => x.PageSize)
               .NotEmpty()
                .WithStatusCode(StatusCode.EmptyError,"每页条数")
                .GreaterThan(0)
                .WithStatusCode(StatusCode.MinValueError,"每页条数", 0);
        }
    }
}
