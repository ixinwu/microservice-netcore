/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * IUnitRepository Description:
 * 仓储接口
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-15   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IUnitRepository : IAdoRepository, IDisposable
    {
        /// <summary>
        /// 查询满足条件的第一个
        /// </summary>
        /// <typeparam name="TEntity">返回值类型</typeparam>
        /// <param name="criteria">查询条件</param>
        /// <returns>查询结果</returns>
        TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : BaseEntity;

        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <typeparam name="TEntity">返回值类型</typeparam>
        /// <param name="id">主键值</param>
        /// <returns>查询结果</returns>
        TEntity FindById<TEntity>(string id) where TEntity : BaseEntity;

        /// <summary>
        /// 追加对象
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entity">追加对象</param>
        void Insert<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        void Insert<TEntity>(List<TEntity> entities) where TEntity : BaseEntity;

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entity">需要更新的实体</param>
        void Update<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// EF扩展更新
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="updateExpression">更新表达式</param>
        /// <param name="criteria">查询表达式</param>
        /// <returns></returns>        
        Task<int> BatchUpdateAsync<TEntity>(Expression<Func<TEntity, TEntity>> updateExpression,
            Expression<Func<TEntity, bool>> criteria = null) where TEntity : class;

        /// <summary>
        /// EF扩展更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="updateValues"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        Task<int> BatchUpdateAsync<TEntity>(Expression<Func<TEntity, bool>> criteria,
            TEntity updateValues, List<string> updateColumns = null) where TEntity : class, new();

        /// <summary>
        /// EF扩展更新
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="updateExpression">更新表达式</param>
        /// <param name="criteria">查询表达式</param>
        /// <returns></returns>        
        int BatchUpdate<TEntity>(Expression<Func<TEntity, TEntity>> updateExpression,
            Expression<Func<TEntity, bool>> criteria = null) where TEntity : class;

        /// <summary>
        /// EF扩展更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="updateValues"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        int BatchUpdate<TEntity>(Expression<Func<TEntity, bool>> criteria,
            TEntity updateValues, List<string> updateColumns = null) where TEntity : class, new();

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entity">需要删除的实体</param>
        void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// EF扩展删除
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="criteria">查询表达式</param>
        /// <returns></returns>
        Task<int> BatchDeleteAsync<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// EF扩展删除
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="criteria">查询表达式</param>
        /// <returns></returns>
        int BatchDelete<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// 获取表（EF功能）
        /// </summary>
        /// <typeparam name="TEntity">查询类型</typeparam>
        ///  <param name="criteria">条件表达式</param>
        /// <returns>当前类型的表接口</returns>
        IQueryable<TEntity> All<TEntity>(Expression<Func<TEntity, bool>> criteria = null) where TEntity : class;

        /// <summary>
        /// 获取一个启用了“无跟踪”的表（EF功能），仅当只为只读操作加载记录时才使用该表
        /// </summary>
        /// <typeparam name="TEntity">查询类型</typeparam>
        ///  <param name="criteria">条件表达式</param>
        /// <returns>当前类型的表接口</returns>
        IQueryable<TEntity> AllNoTracking<TEntity>(Expression<Func<TEntity, bool>> criteria = null) where TEntity : class;

        /// <summary>
        /// 基于原始SQL查询为查询类型创建LINQ查询
        /// </summary>
        /// <typeparam name="TQuery">Query type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>表示原始SQL查询的IQueryable</returns>
        IQueryable<TQuery> QueryFromSql<TQuery>(string sql, IDictionary<string, object> parameters = null) where TQuery : class;

        /// <summary>
        /// 基于原始SQL查询为实体创建LINQ查询
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="sql">原始SQL查询</param>
        /// <param name="parameters">要分配给参数的值</param>
        /// <returns>表示原始SQL查询的IQueryable</returns>
        IQueryable<TEntity> EntityFromSql<TEntity>(string sql, IDictionary<string, object> parameters = null) where TEntity : BaseEntity;

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="TEntity">查询类型</typeparam>
        ///  <param name="criteria">条件表达式</param>
        /// <returns>当前条件是否存在</returns>
        bool Any<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : BaseEntity;

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        Task BulkInsertAsync<TEntity>(IList<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 批量插入或更新
        /// 1、列表数据在DB不存在，则追加
        /// 2、列表数据在DB存在，则更新
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        Task BulkInsertOrUpdateAsync<TEntity>(IList<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 列表数据和DB数据同步
        /// 1、列表数据在DB不存在，则追加
        /// 2、列表数据在DB存在，则更新
        /// 3、DB数据存在列表数据不存在，则删除
        /// 判断条件：主键
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task BulkInsertOrUpdateOrDeleteAsync<TEntity>(IList<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 将此上下文中所做的所有更改保存到数据库
        /// </summary>
        /// <returns>写入数据库的状态条目数</returns>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// 将此上下文中所做的所有更改保存到数据库
        /// </summary>
        /// <returns>写入数据库的状态条目数</returns>
        int SaveChanges();

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        void BulkInsert<TEntity>(IList<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 批量插入或更新
        /// 1、列表数据在DB不存在，则追加
        /// 2、列表数据在DB存在，则更新
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        void BulkInsertOrUpdate<TEntity>(IList<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        void BulkUpdate<TEntity>(IList<TEntity> entities) where TEntity : class;

        /// <summary>
        /// 列表数据和DB数据同步
        /// 1、列表数据在DB不存在，则追加
        /// 2、列表数据在DB存在，则更新
        /// 3、DB数据存在列表数据不存在，则删除
        /// 判断条件：主键
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        void BulkInsertOrUpdateOrDelete<TEntity>(IList<TEntity> entities) where TEntity : class;

        /// <summary>
        /// data base
        /// </summary>
        DatabaseFacade Database { get; }
    }
}
