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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace NGP.Framework.Core.Data
{
    /// <summary>
    /// 工作单元仓储
    /// </summary>
    public abstract class BaseAdoRepository : IAdoRepository
    {
        #region private excute func
        #endregion

        #region excute command
        /// <summary>
        /// 读取列表数据(泛型)
        /// </summary>
        /// <typeparam name="TEntity">返回结果类型</typeparam>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回结果</returns>
        public List<TEntity> QueryListEntity<TEntity>(string commandText,
             IDictionary<string, object> parameters = null)
             where TEntity : class, new()
        {
            Func<DbCommand, List<TEntity>> excute = (dbCommand) =>
            {
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    var result = new List<TEntity>();
                    var converter = new DbReaderConverter<TEntity>(reader);
                    while (reader.Read())
                    {
                        var item = converter.CreateItemFromRow();
                        result.Add(item);
                    }
                    return result;
                }
            };
            return CreateDbCommondAndExcute(commandText, parameters,
                excute);
        }

        /// <summary>
        /// 读取列表数据
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回结果</returns>
        public List<dynamic> QueryListDynamic(string commandText,
             IDictionary<string, object> parameters = null)
        {
            Func<DbCommand, List<dynamic>> excute = dbCommand =>
            {
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    var result = new List<dynamic>();
                    var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                    foreach (IDataRecord record in reader as IEnumerable)
                    {
                        var expando = new ExpandoObject() as IDictionary<string, object>;
                        foreach (var name in names)
                        {
                            expando[name] = record[name];
                        }
                        result.Add(expando);
                    }
                    return result;
                }
            };
            return CreateDbCommondAndExcute(commandText, parameters, excute);
        }

        /// <summary>
        /// 读取列表数据,根据回调设定值,值通过key,value提供
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>        
        /// <returns>返回结果</returns>
        public List<IDictionary<string, object>> QueryListDictionary(string commandText,
            IDictionary<string, object> parameters = null)
        {
            Func<DbCommand, List<IDictionary<string, object>>> excute = dbCommand =>
            {
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    var result = new List<IDictionary<string, object>>();
                    var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                    foreach (IDataRecord record in reader as IEnumerable)
                    {
                        result.Add(names.ToDictionary(n => n, n => record[n]));
                    }
                    return result;
                }
            };
            return CreateDbCommondAndExcute(commandText, parameters, excute);
        }

        /// <summary>
        /// 执行数据行读取
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>        
        /// <returns>返回结果</returns>
        public DataTable QueryDataTable(string commandText,
            IDictionary<string, object> parameters = null)
        {
            Func<DbCommand, DataTable> excute = dbCommand =>
            {
                using (var reader = dbCommand.ExecuteReader())
                {
                    var table = new DataTable();
                    table.BeginLoadData();
                    table.Load(reader);
                    table.EndLoadData();
                    return table;
                }
            };
            return CreateDbCommondAndExcute(commandText, parameters, excute);
        }

        /// <summary>
        /// 读取单条记录（泛型）
        /// </summary>
        /// <typeparam name="TEntity">返回结果类型</typeparam>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回结果</returns>
        public TEntity QuerySingleEntity<TEntity>(string commandText,
            IDictionary<string, object> parameters = null)
            where TEntity : class, new()
        {
            Func<DbCommand, TEntity> excute = (dbCommand) =>
            {
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    var converter = new DbReaderConverter<TEntity>(reader);
                    if (reader.Read())
                    {
                        return converter.CreateItemFromRow();
                    }
                    return null;
                }
            };
            return CreateDbCommondAndExcute(commandText, parameters,
                excute);
        }

        /// <summary>
        /// 读取列表数据
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回结果</returns>
        public dynamic QuerySingleDynamic(string commandText,
             IDictionary<string, object> parameters = null)
        {
            Func<DbCommand, dynamic> excute = (dbCommand) =>
            {
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                    foreach (IDataRecord record in reader as IEnumerable)
                    {
                        var expando = new ExpandoObject() as IDictionary<string, object>;
                        foreach (var name in names)
                        {
                            expando[name] = record[name];
                        }
                        return expando;
                    }
                    return null;
                }
            };

            return CreateDbCommondAndExcute(commandText, parameters, excute);
        }

        /// <summary>
        /// 读取详细记录,根据回调设定值,值通过key,value提供
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>        
        /// <returns>返回结果</returns>
        public IDictionary<string, object> QuerySingleDictionary(string commandText,
            IDictionary<string, object> parameters = null)
        {
            Func<DbCommand, dynamic> excute = (dbCommand) =>
            {
                using (var reader = dbCommand.ExecuteReader())
                {
                    var names = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                    foreach (IDataRecord record in reader as IEnumerable)
                    {
                        return names.ToDictionary(n => n, n => record[n]);
                    }
                    return null;
                }
            };

            return CreateDbCommondAndExcute(commandText, parameters, excute);
        }

        /// <summary>
        /// 读取第一条第一列的结果（泛型）
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回结果</returns>
        public T ExecuteScalar<T>(string commandText, IDictionary<string, object> parameters = null)
        {
            return CreateDbCommondAndExcute(commandText,
                parameters, (dbCommand) => CommonHelper.To<T>(dbCommand.ExecuteScalar()));
        }

        /// <summary>
        /// 执行数据库操作
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响条数</returns>
        public int ExecuteNonQuery(string commandText, IDictionary<string, object> parameters = null)
        {
            return CreateDbCommondAndExcute(commandText,
                parameters, (dbCommand) => dbCommand.ExecuteNonQuery());
        }
        #endregion

        #region abstract methods
        /// <summary>
        /// 创建MySqlCommand执行
        /// </summary>
        /// <typeparam name="T">结果泛型</typeparam>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <param name="excute">执行回调</param>
        /// <returns>执行结果</returns>
        protected abstract T CreateDbCommondAndExcute<T>(string commandText,
           IDictionary<string, object> parameters, Func<DbCommand, T> excute);

        /// <summary>
        /// 设置参数
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <param name="parameters"></param>
        protected virtual void SetParameters(DbCommand dbCommand, IDictionary<string, object> parameters)
        {
            if (parameters == null)
            {
                return;
            }
            foreach (var parameter in parameters)
            {
                if (parameter.Value != null)
                {
                    dbCommand.Parameters.Add(new SqlParameter(parameter.Key, parameter.Value));
                    continue;
                }

                dbCommand.Parameters.Add(new SqlParameter(parameter.Key, DBNull.Value));
            }
        }
        #endregion

        #region excute xml command
        /// <summary>
        /// 通过xml文件--读取列表数据(泛型)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<TEntity> QueryListEntity<TEntity>(string xmlId, string sqlId, params object[] parameters) where TEntity : class, new()
        {
            var resloveResult = ResolveSqlTextExtension.GetResolveXmlResult(xmlId, sqlId, parameters);
            return QueryListEntity<TEntity>(resloveResult.sqlText, resloveResult.parameters);
        }

        /// <summary>
        /// 通过xml文件--读取动态列表数据
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<dynamic> QueryListDynamic(string xmlId, string sqlId, params object[] parameters)
        {
            var resloveResult = ResolveSqlTextExtension.GetResolveXmlResult(xmlId, sqlId, parameters);
            return QueryListDynamic(resloveResult.sqlText, resloveResult.parameters);
        }

        /// <summary>
        /// 通过xml文件--读取列表数据,根据回调设定值,值通过key,value提供
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public List<IDictionary<string, object>> QueryListDictionary(string xmlId, string sqlId, params object[] parameters)
        {
            var resloveResult = ResolveSqlTextExtension.GetResolveXmlResult(xmlId, sqlId, parameters);
            return QueryListDictionary(resloveResult.sqlText, resloveResult.parameters);
        }

        /// <summary>
        /// 通过xml文件--读取datatable
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataTable QueryDataTable(string xmlId, string sqlId, params object[] parameters)
        {
            var resloveResult = ResolveSqlTextExtension.GetResolveXmlResult(xmlId, sqlId, parameters);
            return QueryDataTable(resloveResult.sqlText, resloveResult.parameters);
        }

        /// <summary>
        /// 通过xml文件--读取单条记录（泛型）
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public TEntity QuerySingleEntity<TEntity>(string xmlId, string sqlId, params object[] parameters) where TEntity : class, new()
        {
            var resloveResult = ResolveSqlTextExtension.GetResolveXmlResult(xmlId, sqlId, parameters);
            return QuerySingleEntity<TEntity>(resloveResult.sqlText, resloveResult.parameters);
        }

        /// <summary>
        /// 通过xml文件--读取详细记录,值通过key,value提供
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IDictionary<string, object> QuerySingleDictionary(string xmlId, string sqlId, params object[] parameters)
        {
            var resloveResult = ResolveSqlTextExtension.GetResolveXmlResult(xmlId, sqlId, parameters);
            return QuerySingleDictionary(resloveResult.sqlText, resloveResult.parameters);
        }

        /// <summary>
        /// 通过xml文件--读取单条动态记录
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public dynamic QuerySingleDynamic(string xmlId, string sqlId, params object[] parameters)
        {
            var resloveResult = ResolveSqlTextExtension.GetResolveXmlResult(xmlId, sqlId, parameters);
            return QuerySingleDynamic(resloveResult.sqlText, resloveResult.parameters);
        }

        /// <summary>
        /// 通过xml文件--读取第一条第一列的结果（泛型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public T ExecuteScalar<T>(string xmlId, string sqlId, params object[] parameters)
        {
            var resloveResult = ResolveSqlTextExtension.GetResolveXmlResult(xmlId, sqlId, parameters);
            return ExecuteScalar<T>(resloveResult.sqlText, resloveResult.parameters);
        }

        /// <summary>
        /// 通过xml文件--执行数据库操作
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string xmlId, string sqlId, params object[] parameters)
        {
            var resloveResult = ResolveSqlTextExtension.GetResolveXmlResult(xmlId, sqlId, parameters);
            return ExecuteNonQuery(resloveResult.sqlText, resloveResult.parameters);
        }
        #endregion
    }
}
