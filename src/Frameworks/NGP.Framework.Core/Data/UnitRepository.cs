/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * UnitRepository Description:
 * 工作单元仓储
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace NGP.Framework.Core.Data
{
    /// <summary>
    /// 工作单元仓储
    /// </summary>
    public class UnitRepository :BaseAdoRepository, IUnitRepository
    {
        /// <summary>
        /// ef上下文
        /// </summary>
        protected readonly IDbContext _context;

        #region Ctor
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        public UnitRepository(IDbContext context)
          => _context = context;
        #endregion

        #region Methods
        /// <summary>
        /// 查询满足条件的第一个
        /// </summary>
        /// <typeparam name="TEntity">返回值类型</typeparam>
        /// <param name="criteria">查询条件</param>
        /// <returns>查询结果</returns>
        public TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : BaseEntity
            => _context.Set<TEntity>().FirstOrDefault(criteria);


        /// <summary>
        /// 根据ID查找
        /// </summary>
        /// <typeparam name="TEntity">返回值类型</typeparam>
        /// <param name="id">主键值</param>
        /// <returns>查询结果</returns>
        public TEntity FindById<TEntity>(string id) where TEntity : BaseEntity
            => _context.Set<TEntity>().Find(id);

        /// <summary>
        /// 追加对象
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entity">追加对象</param>
        public void Insert<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                return;
            }
            _context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// 更新对象
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entity">需要更新的实体</param>
        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                return;
            }
            _context.Set<TEntity>().Attach(entity);
        }

        /// <summary>
        /// EF扩展更新
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="updateExpression">更新表达式</param>
        /// <param name="criteria">查询表达式</param>
        /// <returns></returns>      
        public Task<int> BatchUpdateAsync<TEntity>(Expression<Func<TEntity, TEntity>> updateExpression,
            Expression<Func<TEntity, bool>> criteria = null) where TEntity : class
        {
            var source = All<TEntity>(criteria);
            return source.BatchUpdateAsync(updateExpression);
        }

        /// <summary>
        /// EF扩展更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="updateValues"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public Task<int> BatchUpdateAsync<TEntity>(Expression<Func<TEntity, bool>> criteria,
             TEntity updateValues, List<string> updateColumns = null) where TEntity : class, new()
        {
            var source = All<TEntity>(criteria);
            return source.BatchUpdateAsync(updateValues, updateColumns);
        }

        /// <summary>
        /// EF扩展更新
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="updateExpression">更新表达式</param>
        /// <param name="criteria">查询表达式</param>
        /// <returns></returns>        
        public int BatchUpdate<TEntity>(Expression<Func<TEntity, TEntity>> updateExpression,
            Expression<Func<TEntity, bool>> criteria = null) where TEntity : class
        {
            var source = All<TEntity>(criteria);
            return source.BatchUpdate(updateExpression);
        }

        /// <summary>
        /// EF扩展更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="criteria"></param>
        /// <param name="updateValues"></param>
        /// <param name="updateColumns"></param>
        /// <returns></returns>
        public int BatchUpdate<TEntity>(Expression<Func<TEntity, bool>> criteria,
            TEntity updateValues, List<string> updateColumns = null) where TEntity : class, new()
        {
            var source = All<TEntity>(criteria);
            return source.BatchUpdate(updateValues, updateColumns);
        }

        /// <summary>
        /// 删除对象
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entity">需要删除的实体</param>
        public void Delete<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
            {
                return;
            }
            _context.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// EF扩展删除
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="criteria">查询表达式</param>
        /// <returns></returns>
        public Task<int> BatchDeleteAsync<T>(Expression<Func<T, bool>> criteria) where T : class
        {
            var source = _context.Set<T>();
            return source.Where(criteria).BatchDeleteAsync();
        }

        /// <summary>
        /// EF扩展删除
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <param name="criteria">查询表达式</param>
        /// <returns></returns>
        public int BatchDelete<T>(Expression<Func<T, bool>> criteria) where T : class
        {
            var source = _context.Set<T>();
            return source.Where(criteria).BatchDelete();
        }

        /// <summary>
        /// 基于原始SQL查询为查询类型创建LINQ查询
        /// </summary>
        /// <typeparam name="TQuery">Query type</typeparam>
        /// <param name="sql">The raw SQL query</param>
        /// <param name="parameters">The values to be assigned to parameters</param>
        /// <returns>表示原始SQL查询的IQueryable</returns>
        public IQueryable<TQuery> QueryFromSql<TQuery>(string sql, IDictionary<string, object> parameters = null) where TQuery : class
        {
            var dbParameters = (parameters ?? new Dictionary<string, object>()).Select(s => new SqlParameter(s.Key, s.Value)).ToArray();
            return _context.QueryFromSql<TQuery>(sql, dbParameters);
        }


        /// <summary>
        /// 基于原始SQL查询为实体创建LINQ查询
        /// </summary>
        /// <typeparam name="TEntity">Entity type</typeparam>
        /// <param name="sql">原始SQL查询</param>
        /// <param name="parameters">要分配给参数的值</param>
        /// <returns>表示原始SQL查询的IQueryable</returns>
        public IQueryable<TEntity> EntityFromSql<TEntity>(string sql, IDictionary<string, object> parameters = null) where TEntity : BaseEntity
        {
            var dbParameters = (parameters ?? new Dictionary<string, object>()).Select(s => new SqlParameter(s.Key, s.Value)).ToArray();
            return _context.EntityFromSql<TEntity>(sql, dbParameters);
        }

        /// <summary>
        /// </summary>
        /// <typeparam name="TEntity">查询类型</typeparam>
        public IQueryable<TEntity> All<TEntity>(Expression<Func<TEntity, bool>> criteria = null) where TEntity : class
            => criteria == null ? _context.Set<TEntity>() : _context.Set<TEntity>().Where(criteria);


        /// <summary>
        /// 获取一个启用了“无跟踪”的表（EF功能），仅当只为只读操作加载记录时才使用该表
        /// </summary>
        /// <typeparam name="TEntity">查询类型</typeparam>
        ///  <param name="criteria">条件表达式</param>
        /// <returns>当前类型的表接口</returns>
        public IQueryable<TEntity> AllNoTracking<TEntity>(Expression<Func<TEntity, bool>> criteria = null) where TEntity : class
        {
            var entities = _context.Set<TEntity>().AsNoTracking();
            return criteria == null ? entities : entities.Where(criteria);
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <typeparam name="TEntity">查询类型</typeparam>
        ///  <param name="criteria">条件表达式</param>
        /// <returns>当前条件是否存在</returns>
        public bool Any<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : BaseEntity
            => All(criteria).Select(s => s.Id).Distinct().Any();


        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        public void Insert<TEntity>(List<TEntity> entities) where TEntity : BaseEntity
        {
            if (entities.IsNullOrEmpty())
            {
                return;
            }
            foreach (var entitieItem in entities)
            {
                _context.Set<TEntity>().Add(entitieItem);
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        public Task BulkInsertAsync<TEntity>(IList<TEntity> entities) where TEntity : class
        {
            if (entities.IsNullOrEmpty())
            {
                return Task.CompletedTask;
            }

            return (_context as DbContext).BulkInsertAsync(entities);
        }

        /// <summary>
        /// 批量插入或更新
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        public Task BulkInsertOrUpdateAsync<TEntity>(IList<TEntity> entities) where TEntity : class
        {
            if (entities.IsNullOrEmpty())
            {
                return Task.CompletedTask;
            }

            return (_context as DbContext).BulkInsertOrUpdateAsync(entities);
        }

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
        public Task BulkInsertOrUpdateOrDeleteAsync<TEntity>(IList<TEntity> entities) where TEntity : class
        {
            if (entities.IsNullOrEmpty())
            {
                return Task.CompletedTask;
            }

            return (_context as DbContext).BulkInsertOrUpdateOrDeleteAsync(entities);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        public void BulkInsert<TEntity>(IList<TEntity> entities) where TEntity : class
        {
            if (entities.IsNullOrEmpty())
            {
                return;
            }

            (_context as DbContext).BulkInsert(entities);
        }

        /// <summary>
        /// 批量插入或更新
        /// 1、列表数据在DB不存在，则追加
        /// 2、列表数据在DB存在，则更新
        /// </summary>
        /// <typeparam name="TEntity">参数类型</typeparam>
        /// <param name="entities">插入列表</param>
        public void BulkInsertOrUpdate<TEntity>(IList<TEntity> entities) where TEntity : class
        {
            if (entities.IsNullOrEmpty())
            {
                return;
            }

            (_context as DbContext).BulkInsertOrUpdate(entities);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entities"></param>
        public void BulkUpdate<TEntity>(IList<TEntity> entities) where TEntity : class
        {
            if (entities.IsNullOrEmpty())
            {
                return;
            }

            (_context as DbContext).BulkUpdate(entities);
        }

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
        public void BulkInsertOrUpdateOrDelete<TEntity>(IList<TEntity> entities) where TEntity : class
        {
            if (entities.IsNullOrEmpty())
            {
                return;
            }

            (_context as DbContext).BulkInsertOrUpdateOrDelete(entities);
        }

        /// <summary>
        /// data base
        /// </summary>
        public DatabaseFacade Database { get => _context.Database; }

        /// <summary>
        /// 将此上下文中所做的所有更改保存到数据库
        /// </summary>
        /// <returns>写入数据库的状态条目数</returns>
        public Task<int> SaveChangesAsync()
            => _context.SaveChangesAsync();

        /// <summary>
        /// 将此上下文中所做的所有更改保存到数据库
        /// </summary>
        /// <returns>写入数据库的状态条目数</returns>
        public int SaveChanges() => _context.SaveChanges();
        #endregion

        /// <summary>
        /// 创建MySqlCommand执行
        /// </summary>
        /// <typeparam name="T">结果泛型</typeparam>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <param name="excute">执行回调</param>
        /// <returns>执行结果</returns>
        protected override T CreateDbCommondAndExcute<T>(string commandText,
           IDictionary<string, object> parameters, Func<DbCommand, T> excute)
        {
            //var conn = _context.Database.GetDbConnection() as SqlConnection;
            //if (conn.State != ConnectionState.Open)
            //{
            //    conn.Open();
            //}

            //using (var dbCommand = conn.CreateCommand())
            //{
            //    dbCommand.CommandType = CommandType.Text;
            //    SetParameters(dbCommand, parameters);
            //    dbCommand.CommandText = commandText;

            //    return excute(dbCommand);
            //}

            var conn = _context.Database.GetDbConnection();
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            using (var dbCommand = conn.CreateCommand())
            {
                dbCommand.CommandType = CommandType.Text;
                SetParameters(dbCommand, parameters);
                dbCommand.CommandText = commandText;
                if (_context.Database.CurrentTransaction != null)
                    dbCommand.Transaction = _context.Database.CurrentTransaction.GetDbTransaction();

                return excute(dbCommand);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }
    }
}
