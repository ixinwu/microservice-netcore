/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ExpressionExtension Description:
 * 表达式树扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-28   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 表达式树扩展
    /// </summary>
    public static class ExpressionExtension
    {
        /// <summary>
        /// 扩展获取值format
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static object Eval(this Expression expr)
        {
            return expr.Eval<object>();
        }

        /// <summary>
        /// 扩展获取值format(泛型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static T Eval<T>(this Expression expr)
        {
            var constantExpr = expr as ConstantExpression;
            if (constantExpr != null)
            {
                return (T)constantExpr.Value;
            }
            var fun = Expression.Lambda<Func<object>>(Expression.Convert(expr, typeof(object))).Compile();

            return (T)(fun());
        }

        /// <summary>
        /// 整合两个表达式
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="left">左表达式</param>
        /// <param name="right">右表达式</param>
        /// <param name="method">合并方法</param>
        /// <returns>合并结果</returns>
        public static Expression<T> Splice<T>(this Expression<T> left, Expression<T> right, Func<Expression, Expression, Expression> method)
        {
            var dictionary = left.Parameters.Select((f, i) => new { f, s = right.Parameters[i] }).ToDictionary(p => p.s, p => p.f);
            //替换参数
            var secondBody = ParameterReset.ReplaceParameterList(dictionary, right.Body);
            return Expression.Lambda<T>(method(left.Body, secondBody), left.Parameters);
        }

        /// <summary>
        /// 表达式参数Format
        /// </summary>
        private class ParameterReset : ExpressionVisitor
        {
            /// <summary>
            /// 字典
            /// </summary>
            private Dictionary<ParameterExpression, ParameterExpression> Dictionary { get; set; }

            /// <summary>
            /// 参数重设
            /// </summary>
            /// <param name="dictionary"></param>
            public ParameterReset(Dictionary<ParameterExpression, ParameterExpression> dictionary)
            {
                this.Dictionary = dictionary ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            /// <summary>
            /// 替换参数列表
            /// </summary>
            /// <param name="dictionary"></param>
            /// <param name="expression"></param>
            /// <returns></returns>
            public static Expression ReplaceParameterList(Dictionary<ParameterExpression, ParameterExpression> dictionary, Expression expression)
            {
                return new ParameterReset(dictionary).Visit(expression);
            }

            /// <summary>
            /// 重置参数值
            /// </summary>
            /// <param name="param"></param>
            /// <returns></returns>
            protected override Expression VisitParameter(ParameterExpression param)
            {
                ParameterExpression replaceParam;
                if (Dictionary.TryGetValue(param, out replaceParam))
                {
                    param = replaceParam;
                }
                return base.VisitParameter(param);
            }
        }
    }
}
