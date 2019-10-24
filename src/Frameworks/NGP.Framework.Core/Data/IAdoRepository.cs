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

using System.Collections.Generic;
using System.Data;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    public interface IAdoRepository
    {
        /// <summary>
        /// 读取列表数据(泛型)
        /// </summary>
        /// <typeparam name="TEntity">返回结果类型</typeparam>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回结果</returns>
        List<TEntity> QueryListEntity<TEntity>(string commandText,
            IDictionary<string, object> parameters = null)
            where TEntity : class, new();

        /// <summary>
        /// 读取列表数据
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回结果</returns>
        List<dynamic> QueryListDynamic(string commandText,
             IDictionary<string, object> parameters = null);

        /// <summary>
        /// 读取列表数据,根据回调设定值,值通过key,value提供
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>        
        /// <returns>返回结果</returns>
        List<IDictionary<string, object>> QueryListDictionary(string commandText,
            IDictionary<string, object> parameters = null);

        /// <summary>
        /// 读取datatable
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>        
        /// <returns>返回结果</returns>
        DataTable QueryDataTable(string commandText,
            IDictionary<string, object> parameters = null);

        /// <summary>
        /// 通过xml文件--读取列表数据(泛型)
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<TEntity> QueryListEntity<TEntity>(string xmlId,
            string sqlId,
            params object[] parameters)
            where TEntity : class, new();

        /// <summary>
        /// 通过xml文件--读取动态列表数据
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<dynamic> QueryListDynamic(string xmlId,
            string sqlId,
            params object[] parameters);

        /// <summary>
        /// 通过xml文件--读取列表数据,根据回调设定值,值通过key,value提供
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        List<IDictionary<string, object>> QueryListDictionary(string xmlId,
            string sqlId,
            params object[] parameters);

        /// <summary>
        /// 通过xml文件--读取datatable
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>        
        /// <returns>返回结果</returns>
        DataTable QueryDataTable(string xmlId,
            string sqlId,
            params object[] parameters);

        /// <summary>
        /// 读取单条记录（泛型）
        /// </summary>
        /// <typeparam name="TEntity">返回结果类型</typeparam>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回结果</returns>
        TEntity QuerySingleEntity<TEntity>(string commandText,
            IDictionary<string, object> parameters = null)
            where TEntity : class, new();

        /// <summary>
        /// 读取详细记录,根据回调设定值,值通过key,value提供
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>        
        /// <returns>返回结果</returns>
        IDictionary<string, object> QuerySingleDictionary(string commandText,
            IDictionary<string, object> parameters = null);

        /// <summary>
        /// 读取详细记录,根据回调设定值,值通过key,value提供
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param> 
        /// <returns>返回结果</returns>
        dynamic QuerySingleDynamic(string commandText,
             IDictionary<string, object> parameters = null);

        /// <summary>
        /// 通过xml文件--读取单条记录（泛型）
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        TEntity QuerySingleEntity<TEntity>(string xmlId,
            string sqlId,
            params object[] parameters)
            where TEntity : class, new();

        /// <summary>
        /// 通过xml文件--读取详细记录,值通过key,value提供
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IDictionary<string, object> QuerySingleDictionary(string xmlId,
            string sqlId,
            params object[] parameters);

        /// <summary>
        /// 通过xml文件--读取单条动态记录
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        dynamic QuerySingleDynamic(string xmlId,
            string sqlId,
            params object[] parameters);

        /// <summary>
        /// 读取第一条第一列的结果（泛型）
        /// </summary>
        /// <typeparam name="T">结果类型</typeparam>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>返回结果</returns>
        T ExecuteScalar<T>(string commandText, IDictionary<string, object> parameters = null);

        /// <summary>
        /// 执行数据库操作
        /// </summary>
        /// <param name="commandText">执行语句</param>
        /// <param name="parameters">参数列表</param>
        /// <returns>影响条数</returns>
        int ExecuteNonQuery(string commandText, IDictionary<string, object> parameters = null);

        /// <summary>
        /// 通过xml文件--读取第一条第一列的结果（泛型）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        T ExecuteScalar<T>(string xmlId,
            string sqlId,
            params object[] parameters);

        /// <summary>
        /// 通过xml文件--执行数据库操作
        /// </summary>
        /// <param name="xmlId"></param>
        /// <param name="sqlId"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string xmlId,
            string sqlId,
            params object[] parameters);
    }
}
