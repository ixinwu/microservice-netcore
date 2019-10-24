/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * SystemGroupQueryExtend Description:
 * 系统组查询扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-2-15   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System.Linq;
using System.Linq.Dynamic.Core;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 系统组查询扩展
    /// </summary>
    public static class SystemGroupQueryExtend
    {
        /// <summary>
        /// 通过GroupKey查询
        /// </summary>
        /// <param name="query"></param>
        /// <param name="groupKey"></param>
        /// <returns></returns>
        public static IQueryable<SysConfig_GroupType> QueryByGroupKey(this IQueryable<SysConfig_GroupType> query,
            string groupKey)
            => query.Where(s => !s.IsDelete && s.GroupKey == groupKey)
                 .OrderBy(o => o.OrderIndex)
                 .ThenBy(o => o.TypeValue);
    }
}
