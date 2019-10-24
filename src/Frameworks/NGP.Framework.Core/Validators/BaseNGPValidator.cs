/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * BaseNGPValidator Description:
 * NGP验证基类
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using FluentValidation;
using NGP.Framework.Core.Data;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace NGP.Framework.Core
{
    /// <summary>
    /// NGP验证基类
    /// </summary>
    /// <typeparam name="TModel">Model type</typeparam>
    public abstract class BaseNGPValidator<TModel> : AbstractValidator<TModel> where TModel : class
    {
        #region Ctor
        /// <summary>
        /// ctor
        /// </summary>
        protected BaseNGPValidator()
        {
            Initialize();
        }

        #endregion

        

        #region Utilities

        /// <summary>
        /// Initialize
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// 将验证规则设置为数据库模型
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="dbContext">Database context</param>
        /// <param name="filterStringPropertyNames">过滤的属性</param>
        protected virtual void SetDatabaseValidationRules<TEntity>(IDbContext dbContext, params string[] filterStringPropertyNames)
            where TEntity : BaseEntity
        {
            SetStringPropertiesMaxLength<TEntity>(dbContext, filterStringPropertyNames);
            SetDecimalMaxValue<TEntity>(dbContext);
        }

        /// <summary>
        /// 设定字符串属性的最大长度
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="dbContext">Database context</param>
        /// <param name="filterPropertyNames">过滤的属性</param>
        protected virtual void SetStringPropertiesMaxLength<TEntity>(IDbContext dbContext, params string[] filterPropertyNames)
            where TEntity : BaseEntity
        {
            if (dbContext == null)
                return;

            // 获取需要设定长度的属性名称
            var modelPropertyNames = typeof(TModel).GetProperties()
                .Where(property => property.PropertyType == typeof(string) && !filterPropertyNames.Contains(property.Name))
                .Select(property => property.Name).ToList();

            // 根据ef获取最大长度
            var propertyMaxLengths = dbContext.GetColumnsMaxLength<TEntity>()
                .Where(property => modelPropertyNames.Contains(property.Name) && property.MaxLength.HasValue);

            // 创建验证规则表达式
            var maxLengthExpressions = propertyMaxLengths.Select(property => new
            {
                MaxLength = property.MaxLength.Value,
                Expression = DynamicExpressionParser.ParseLambda<TModel, string>(null, false, property.Name)
            }).ToList();

            // 定义验证规则
            foreach (var expression in maxLengthExpressions)
            {
                RuleFor(expression.Expression).Length(0, expression.MaxLength);
            }
        }

        /// <summary>
        /// 设定decimal的最大值
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="dbContext">Database context</param>
        protected virtual void SetDecimalMaxValue<TEntity>(IDbContext dbContext) where TEntity : BaseEntity
        {
            if (dbContext == null)
                return;

            // 获取decimal的属性
            var modelPropertyNames = typeof(TModel).GetProperties()
                .Where(property => property.PropertyType == typeof(decimal))
                .Select(property => property.Name).ToList();

            // 获取属性的最大值
            var decimalPropertyMaxValues = dbContext.GetDecimalColumnsMaxValue<TEntity>()
                .Where(property => modelPropertyNames.Contains(property.Name) && property.MaxValue.HasValue);

            // 创建验证表达式
            var maxValueExpressions = decimalPropertyMaxValues.Select(property => new
            {
                PropertyName = property.Name,
                MaxValue = property.MaxValue.Value,
                Expression = DynamicExpressionParser.ParseLambda<TModel, decimal>(null, false, property.Name)
            }).ToList();

            foreach (var expression in maxValueExpressions)
            {
                RuleFor(expression.Expression).IsDecimal(expression.MaxValue)
                    .WithStatusCode(StatusCode.MaxValueError,expression.PropertyName, expression.MaxValue - 1);
            }
        }

        #endregion
    }
}