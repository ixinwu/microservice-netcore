/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * PageQueryExtend Description:
 * 分页扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-2-15   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using NGP.Framework.Core.Infrastructure;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 分页扩展
    /// </summary>
    public static class BaseDbEntityExtend
    {
        /// <summary>
        /// 初始化追加时默认字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void InitAddDefaultFields<T>(this T entity)
            where T : BaseDBEntity
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            entity.CreatedBy = workContext.Current.EmplId;            
            entity.CreatedTime = DateTime.Now;
            entity.CreatedArea = workContext.Current.AreaId;
            entity.CreatedCompany = workContext.Current.CompanyId;
            entity.OrderIndex = 0;
            entity.InitUpdateDefaultFields();
        }

        /// <summary>
        /// 初始化更新时默认字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        public static void InitUpdateDefaultFields<T>(this T entity)
            where T : BaseDBEntity
        {
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            entity.UpdatedBy = workContext.Current.EmplId;
            entity.UpdatedTime = DateTime.Now;
            entity.UpdatedArea = workContext.Current.AreaId;
            entity.UpdatedCompany = workContext.Current.CompanyId;
        }
    }
}
