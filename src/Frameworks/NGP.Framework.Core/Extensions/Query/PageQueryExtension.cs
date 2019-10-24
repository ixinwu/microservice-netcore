/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * PageQueryExtend Description:
 * 分页扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-15   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using NGP.Framework.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 分页扩展
    /// </summary>
    public static class PageQueryExtension
    {
        /// <summary>
        /// 重置分页对象
        /// </summary>
        /// <param name="query">查询对象</param>
        public static void ResetPageQuery<T>(this NGPPageQueryRequest<T> query) where T : INGPModel
        {
            //充值当前页码
            if (query.PageNumber == 0)
            {
                query.PageNumber = 1;
            }
            //分页大小默认为10
            if (query.PageSize == 0)
            {
                query.PageSize = 10;
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TReponse"></typeparam>
        /// <param name="source"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static (int totalCount, List<TReponse> data) ParsePageQuery<TRequest, TReponse>(this IQueryable<TReponse> source,
            NGPPageQueryRequest<TRequest> query)
            where TRequest : INGPModel
        {
            query.ResetPageQuery();
            //起始页
            var startIndex = (query.PageNumber - 1) * query.PageSize;
            //默认排序字段
            if (string.IsNullOrWhiteSpace(query.SortField))
            {
                query.SortField = "UpdatedTime";
            }
            //默认排序
            if (string.IsNullOrWhiteSpace(query.SortDirection))
            {
                query.SortDirection = "DESC";
            }

            // 循环排序
            var fields = query.SortField.Split(",");
            var sordirections = query.SortDirection.Split(",");

            for (int i = 0; i < fields.Length; i++)
            {
                var field = fields[i];
                var sortDirection = string.Empty;
                if (i > sordirections.Length - 1)
                {
                    sortDirection = "ASC";
                }
                else
                {
                    sortDirection = sordirections[i];
                }

                if (i == 0)
                {
                    source = source.OrderBy(field + " " + sortDirection);
                }
                else
                {
                    source = ((IOrderedQueryable<TReponse>)source).ThenBy(field + " " + sortDirection);
                }
            }

            // 执行分页
            var pageSource = source
                    .Skip(startIndex.Value)
                    .Take(query.PageSize.Value);

            // 总条数
            var count = source.Select(s => 1).Count();

            // 行数据
            var data = pageSource.ToList();

            return (count, data);
        }
    }
}
