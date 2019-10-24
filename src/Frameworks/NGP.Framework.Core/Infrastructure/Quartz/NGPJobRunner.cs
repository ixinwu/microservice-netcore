/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPJobRunner Description:
 * ngp job执行器
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NGP.Framework.Core.Quartz
{
    /// <summary>
    /// ngp job执行器
    /// </summary>
    [DisallowConcurrentExecution]
    public class NGPJobRunner : IJob
    {
        /// <summary>
        /// 服务提供者
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="serviceProvider"></param>
        public NGPJobRunner(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// excute use scope get job
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var jobType = context.JobDetail.JobType;
                var job = scope.ServiceProvider.GetRequiredService(jobType) as INGPJob;
                //任务开始时间
                var stratTime = DateTime.Now;
                // TODO:Write LOG
                await job.Execute(context);
                //任务结束时间
                var endTime = DateTime.Now;
                //数据仓储
                var unitRepository = Singleton<IEngine>.Instance.Resolve<IUnitRepository>();
                var strategy = unitRepository.Database.CreateExecutionStrategy();
                strategy.Execute(() =>
                {
                    var transaction = unitRepository.Database.BeginTransaction();
                    try
                    {
                        WriteTaskLog(stratTime, endTime, job.JobKey, unitRepository);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();

                        throw new NGPException(ex, StatusCode.DBException);
                    }
                });
            }
        }

        /// <summary>
        /// 记录任务启停信息
        /// </summary>
        /// <param name="stratTime"></param>
        /// <param name="endTime"></param>
        /// <param name="jobKey"></param>
        /// <param name="unitRepository"></param>
        private void WriteTaskLog(DateTime stratTime, DateTime endTime, string jobKey, IUnitRepository unitRepository)
        {

            //记录当前时间
            var dtNow = DateTime.Now;
            //获取接口执行时间
            var excuteTime = endTime - stratTime;
            var sqlStr = @"INSERT INTO [Sys_Log_TaskCommand]
           ([Id],[ConfigKey],[TaskStratTime],[TaskEndTime],[IsDelete],[OrderIndex],[CreatedTime],[CreatedBy],[UpdatedTime],[UpdatedBy],[CreatedArea],[UpdatedArea],[ExcuteTimeS])
     VALUES
           (@Id,@ConfigKey,@TaskStratTime,@TaskEndTime,0,0,@CreatedTime,@CreatedBy,@UpdatedTime,@UpdatedBy,@CreatedArea,@UpdatedArea,@ExcuteTimeS)";
            //定义检索参数
            var sqlParameters = new Dictionary<string, object>();
            sqlParameters.Add("@Id", CommonHelper.NewGuid());
            sqlParameters.Add("@ConfigKey", jobKey);
            sqlParameters.Add("@TaskStratTime", stratTime);
            sqlParameters.Add("@TaskEndTime", endTime);
            sqlParameters.Add("@CreatedTime", dtNow);
            sqlParameters.Add("@CreatedBy", "Ststem");
            sqlParameters.Add("@UpdatedTime", dtNow);
            sqlParameters.Add("@UpdatedBy", "Ststem");
            sqlParameters.Add("@CreatedArea", "32");
            sqlParameters.Add("@UpdatedArea", "32");
            sqlParameters.Add("@ExcuteTimeS", Convert.ToInt32(excuteTime.TotalMilliseconds));
            unitRepository.ExecuteNonQuery(sqlStr, sqlParameters);
        }

    }
}
