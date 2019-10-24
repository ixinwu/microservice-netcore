/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * Program Description:
 * 程序启动入口
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using App.Metrics.AspNetCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NGP.Framework.Core;

namespace NGP.ApiGateway.Base
{
    /// <summary>
    /// 程序启动入口
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口函数
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// 创建webhost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureNGPConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    if (env.IsDevelopment())
                    {
                        config.AddJsonFile($"ocelot.{env.EnvironmentName}.json", optional: true);
                    }
                    else
                    {
                        config.AddJsonFile("ocelot.json", optional: true);
                    }
                })
                .UseStartup<Startup>();
    }
}
