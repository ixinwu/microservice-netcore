/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * IEnumerableExtend Description:
 * 列表接口扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-15   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 列表接口扩展
    /// </summary>
    public static class IEnumerableExtension
    {
        /// <summary>
        /// 判断列表是否为null或者空
        /// </summary>
        /// <typeparam name="T">列表类型</typeparam>
        /// <param name="list">判断源</param>
        /// <returns>判断结果</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list.Count() == 0;
        }

        /// <summary>
        /// 返回列表默认值
        /// </summary>
        /// <typeparam name="TEntity">泛型类型</typeparam>
        /// <param name="source">源列表</param>
        /// <returns>列表的第一个对象</returns>
        public static TEntity FirstOrDefaultWithNullList<TEntity>(this IEnumerable<TEntity> source) where TEntity : new()
        {
            if (!source.IsNullOrEmpty())
            {
                return source.FirstOrDefault();
            }

            return new TEntity();
        }

        /// <summary>
        /// 格式化字符串列表（去除空白和重复项）
        /// </summary>
        /// <param name="source">源</param>
        /// <returns>格式化结果</returns>
        public static IEnumerable<string> RemoveEmptyRepeat(this IEnumerable<string> source)
        {
            return source.Where(s => !string.IsNullOrWhiteSpace(s)).Distinct();
        }

        /// <summary>
        /// 递归查找子集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="criteria"></param>
        /// <param name="selectCurrent"></param>
        /// <param name="parentKey"></param>
        /// <param name="childList"></param>
        public static void RecursionFindChild<T>(this IEnumerable<T> source,
            Func<T, string, bool> criteria,
            Func<T, string> selectCurrent,
            string parentKey,
            List<T> childList)
        {
            var tempList = source.Where(s => criteria(s, parentKey));
            childList.AddRange(tempList);
            foreach (var item in tempList)
            {
                var currentParent = selectCurrent(item);
                RecursionFindChild(source, criteria, selectCurrent, currentParent, childList);
            }
        }

        /// <summary>
        /// 比较两个数组
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="a1">Array 1</param>
        /// <param name="a2">Array 2</param>
        /// <returns>Result</returns>
        public static bool ArraysEqual<T>(this T[] a1, T[] a2)
        {
            //also see Enumerable.SequenceEqual(a1, a2);
            if (ReferenceEquals(a1, a2))
                return true;

            if (a1 == null || a2 == null)
                return false;

            if (a1.Length != a2.Length)
                return false;

            var comparer = EqualityComparer<T>.Default;
            return !a1.Where((t, i) => !comparer.Equals(t, a2[i])).Any();
        }

        /// <summary>
        /// 根据key获取value
        /// </summary>
        /// <typeparam name="TKey">key类型</typeparam>
        /// <typeparam name="TValue">value类型</typeparam>
        /// <param name="dic">字典</param>
        /// <param name="key">key值</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>获取的value值</returns>
        public static TValue GetVlaue<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue defaultValue = default(TValue))
        {
            var resultValue = default(TValue);
            if (dic == null)
            {
                return defaultValue;
            }
            if (dic.TryGetValue(key, out resultValue))
            {
                return resultValue;
            }
            return defaultValue;
        }
    }
}
