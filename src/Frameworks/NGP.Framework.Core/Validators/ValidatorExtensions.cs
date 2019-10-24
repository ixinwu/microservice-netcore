/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ValidatorExtensions Description:
 * 验证扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using FluentValidation;
using NGP.Framework.Core.Validators;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 验证扩展
    /// </summary>
    public static class ValidatorExtensions
    {
        /// <summary>
        /// 添加错误码
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="rule"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> WithStatusCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule,
            StatusCode statusCode,params object[] formatParameters)
        {
            var response = NGPResponse.Create(statusCode, formatParameters);

            return rule.WithMessage(response.ToString());            
        }

        /// <summary>
        /// Set decimal validator
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="ruleBuilder">RuleBuilder</param>
        /// <param name="maxValue">Maximum value</param>
        /// <returns>Result</returns>
        public static IRuleBuilderOptions<T, decimal> IsDecimal<T>(this IRuleBuilder<T, decimal> ruleBuilder, decimal maxValue)
        {
            return ruleBuilder.SetValidator(new DecimalPropertyValidator(maxValue));
        }
    }
}
