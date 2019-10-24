/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * QuartzHostedService Description:
 * Quartz.host服务
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Hosting;
using NGP.Framework.Core;
using Quartz;
using Quartz.Spi;
using Microsoft.Extensions.Configuration;

namespace NGP.Framework.Core.Quartz
{
    /// <summary>
    /// Quartz.host服务
    /// </summary>
    public class NGPJobHostedService : IHostedService
    {
        /// <summary>
        /// Scheduler工厂
        /// </summary>
        private readonly ISchedulerFactory _schedulerFactory;

        /// <summary>
        /// 任务工厂
        /// </summary>
        private readonly IJobFactory _jobFactory;

        /// <summary>
        /// 上下文仓储
        /// </summary>
        private readonly IUnitRepository _repository;

        /// <summary>
        /// 类型查找器
        /// </summary>
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 配置
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="schedulerFactory"></param>
        /// <param name="jobFactory"></param>
        /// <param name="repository"></param>
        /// <param name="typeFinder"></param>
        /// <param name="configuration"></param>
        public NGPJobHostedService(
            ISchedulerFactory schedulerFactory,
            IJobFactory jobFactory,
            IUnitRepository repository,
            ITypeFinder typeFinder,
            IConfiguration configuration)
        {
            _schedulerFactory = schedulerFactory;
            _jobFactory = jobFactory;
            _repository = repository;
            _typeFinder = typeFinder;
            _configuration = configuration;
        }

        /// <summary>
        /// Quartz scheduler
        /// </summary>
        public IScheduler Scheduler { get; set; }

        /// <summary>
        /// 开始任务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Scheduler = await _schedulerFactory.GetScheduler(cancellationToken);
            Scheduler.JobFactory = _jobFactory;

            // db获取任务列表
            var jobSchedules = InitJobSchedules();

            foreach (var jobSchedule in jobSchedules)
            {
                var job = CreateJob(jobSchedule);
                var trigger = CreateTrigger(jobSchedule);

                await Scheduler.ScheduleJob(job, trigger, cancellationToken);
            }

            await Scheduler.Start(cancellationToken);
        }

        /// <summary>
        ///  停止任务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Scheduler?.Shutdown(cancellationToken);
        }

        #region 初始化任务
        /// <summary>
        /// 初始化任务
        /// </summary>
        private List<JobSchedule> InitJobSchedules()
        {
            //配置为SQL查询
            var dbJobs = _repository.AllNoTracking<SysConfig_ServiceRunning>(s => s.IsEnable && !s.IsDelete)
                .OrderBy(o => o.OrderIndex).ToList();

            var runJobs = new List<JobSchedule>();

            var codeJobs = new List<(string jobKey, Type jobType)>();
            var jobTypes = _typeFinder.FindClassesOfType<INGPJob>();

            foreach (var jobType in jobTypes)
            {
                var job = Singleton<IEngine>.Instance.ResolveUnregistered(jobType) as INGPJob;
                if (job == null)
                {
                    continue;
                }
                codeJobs.Add((job.JobKey, jobType));
            }

            foreach (var dbJob in dbJobs)
            {
                var codeJob = codeJobs.FirstOrDefault(s => s.jobKey.ToLower() == dbJob.ConfigKey.ToLower());
                if (codeJob == (null, null))
                {
                    continue;
                }
                runJobs.Add(new JobSchedule
                {
                    CronExpression = dbJob.CronConfig,
                    JobType = codeJob.jobType,
                    OrderIndex = dbJob.OrderIndex,
                    ServiceKey = dbJob.ConfigKey,
                    ServiceName = dbJob.ConfigName,
                    ValidEndTime = dbJob.ValidEndTime,
                    ValidStartTime = dbJob.ValidStartTime
                });
            }
            return runJobs;
        }
        #endregion

        /// <summary>
        /// 创建Job
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private static IJobDetail CreateJob(JobSchedule schedule)
        {
            var jobType = schedule.JobType;
            return JobBuilder
                .Create(jobType)
                .WithIdentity(schedule.ServiceKey)
                .WithDescription(schedule.ServiceName)
                .Build();
        }

        /// <summary>
        /// 创建trigger
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        private static ITrigger CreateTrigger(JobSchedule schedule)
        {
            var tigger = TriggerBuilder
                .Create()
                .WithIdentity($"{schedule.ServiceKey}.trigger")
                .WithDescription(schedule.ServiceName);

            if (schedule.ValidEndTime.HasValue)
            {
                tigger = tigger.EndAt(schedule.ValidEndTime.Value);
            }
            return tigger.WithCronSchedule(schedule.CronExpression).Build();
        }
    }
}
