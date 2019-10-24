/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * BaseNGPValidator Description:
 * NGP��֤����
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
    /// NGP��֤����
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
        /// ����֤��������Ϊ���ݿ�ģ��
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="dbContext">Database context</param>
        /// <param name="filterStringPropertyNames">���˵�����</param>
        protected virtual void SetDatabaseValidationRules<TEntity>(IDbContext dbContext, params string[] filterStringPropertyNames)
            where TEntity : BaseEntity
        {
            SetStringPropertiesMaxLength<TEntity>(dbContext, filterStringPropertyNames);
            SetDecimalMaxValue<TEntity>(dbContext);
        }

        /// <summary>
        /// �趨�ַ������Ե���󳤶�
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="dbContext">Database context</param>
        /// <param name="filterPropertyNames">���˵�����</param>
        protected virtual void SetStringPropertiesMaxLength<TEntity>(IDbContext dbContext, params string[] filterPropertyNames)
            where TEntity : BaseEntity
        {
            if (dbContext == null)
                return;

            // ��ȡ��Ҫ�趨���ȵ���������
            var modelPropertyNames = typeof(TModel).GetProperties()
                .Where(property => property.PropertyType == typeof(string) && !filterPropertyNames.Contains(property.Name))
                .Select(property => property.Name).ToList();

            // ����ef��ȡ��󳤶�
            var propertyMaxLengths = dbContext.GetColumnsMaxLength<TEntity>()
                .Where(property => modelPropertyNames.Contains(property.Name) && property.MaxLength.HasValue);

            // ������֤������ʽ
            var maxLengthExpressions = propertyMaxLengths.Select(property => new
            {
                MaxLength = property.MaxLength.Value,
                Expression = DynamicExpressionParser.ParseLambda<TModel, string>(null, false, property.Name)
            }).ToList();

            // ������֤����
            foreach (var expression in maxLengthExpressions)
            {
                RuleFor(expression.Expression).Length(0, expression.MaxLength);
            }
        }

        /// <summary>
        /// �趨decimal�����ֵ
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="dbContext">Database context</param>
        protected virtual void SetDecimalMaxValue<TEntity>(IDbContext dbContext) where TEntity : BaseEntity
        {
            if (dbContext == null)
                return;

            // ��ȡdecimal������
            var modelPropertyNames = typeof(TModel).GetProperties()
                .Where(property => property.PropertyType == typeof(decimal))
                .Select(property => property.Name).ToList();

            // ��ȡ���Ե����ֵ
            var decimalPropertyMaxValues = dbContext.GetDecimalColumnsMaxValue<TEntity>()
                .Where(property => modelPropertyNames.Contains(property.Name) && property.MaxValue.HasValue);

            // ������֤���ʽ
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