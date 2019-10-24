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

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace NGP.Framework.Core.Data
{
    /// <summary>
    /// 工作单元仓储
    /// </summary>
    public class AdoRepository : BaseAdoRepository
    {
        /// <summary>
        /// 连接串
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        public AdoRepository(IConfiguration configuration)
        {
            _connectionString = ConfigurationExtensions.GetConnectionString(configuration, "DbConnection");
        }

        /// <summary>
        /// 创建SqlCommand执行
        /// </summary>
        /// <typeparam name="T">结果泛型</typeparam>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <param name="excute">执行回调</param>
        /// <returns>执行结果</returns>
        protected override T CreateDbCommondAndExcute<T>(string commandText,
           IDictionary<string, object> parameters, Func<DbCommand, T> excute)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var dbCommand = conn.CreateCommand())
                {
                    dbCommand.CommandType = CommandType.Text;
                    SetParameters(dbCommand, parameters);
                    dbCommand.CommandText = commandText;

                    return excute(dbCommand);
                }
            }
        }
    }
}
