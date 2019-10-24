/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPAppMetricsExtension Description:
 * 应用监控扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using App.Metrics;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Framework.Core
{
    /// <summary>
    /// NGPAppMetricsExtension
    /// </summary>
    public static class NGPAppMetricsExtension
    {
        /// <summary>
        /// 注册AppMetrics
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterAppMetrics(this IServiceCollection services, IConfiguration configuration)
        {
            var metrics = AppMetrics.CreateDefaultBuilder()
            .Configuration.Configure(
            options =>
            {
                options.AddAppTag(configuration.GetValue<string>("AppMetrics:App"));
                options.AddEnvTag(configuration.GetValue<string>("AppMetrics:Env"));
                options.Enabled = true;
                options.ReportingEnabled = true;
            })
            .Report.ToInfluxDb(
            options =>
            {
                // 配置InfluxDb
                options.InfluxDb.BaseUri = new Uri(configuration.GetValue<string>("InfluxDb:ConnectionString"));
                options.InfluxDb.Database = configuration.GetValue<string>("AppMetrics:App");
                options.InfluxDb.UserName = configuration.GetValue<string>("InfluxDb:UserName");
                options.InfluxDb.Password = configuration.GetValue<string>("InfluxDb:Password");
                options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                options.HttpPolicy.FailuresBeforeBackoff = 5;
                options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                options.FlushInterval = TimeSpan.FromSeconds(5);
            })
            .Build();

            services.AddMetrics(metrics);
            services.AddMetricsEndpoints(options =>
            {
                // 允许启用/禁用/metrics端点
                options.MetricsEndpointEnabled = true;

                //允许启用 / 禁用 / metrics - text端点
                options.MetricsTextEndpointEnabled = true;

                // 允许启用/禁用/env端点
                options.EnvironmentInfoEndpointEnabled = true;
            });
            services.AddMetricsReportingHostedService();
            services.AddMetricsTrackingMiddleware();
        }

        /// <summary>
        /// 配置AppMetrics
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureAppMetrics(this IApplicationBuilder app)
        {
            app.UseMetricsAllMiddleware();
            // Or to cherry-pick the tracking of interest
            app.UseMetricsActiveRequestMiddleware();
            app.UseMetricsErrorTrackingMiddleware();
            app.UseMetricsPostAndPutSizeTrackingMiddleware();
            app.UseMetricsRequestTrackingMiddleware();
            app.UseMetricsOAuth2TrackingMiddleware();
            app.UseMetricsApdexTrackingMiddleware();

            app.UseMetricsAllEndpoints();
            // Or to cherry-pick endpoint of interest
            app.UseMetricsEndpoint();
            app.UseMetricsTextEndpoint();
            app.UseEnvInfoEndpoint();
        }

    }
}
