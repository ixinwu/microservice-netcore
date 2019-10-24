/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ResilientTransaction Description:
 * 多上下文的事务扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-24   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using NGP.Framework.Core.Data;
using System;
using System.Threading.Tasks;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 多上下文的事务扩展
    /// </summary>
    public class ResilientTransaction
    {
        /// <summary>
        /// db上下文
        /// </summary>
        private readonly IDbContext _context;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="context"></param>
        private ResilientTransaction(IDbContext context) => _context = context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static ResilientTransaction New(IDbContext context) => new ResilientTransaction(context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public async Task ExecuteAsync(Func<Task> action)
        {
            // Use of an EF Core resiliency strategy when using multiple DbContexts 
            // within an explicit BeginTransaction():
            // https://docs.microsoft.com/ef/core/miscellaneous/connection-resiliency
            var strategy = _context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    await action();
                    transaction.Commit();
                }
            });
        }
    }
}
