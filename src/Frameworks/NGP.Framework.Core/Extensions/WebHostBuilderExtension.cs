/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * WebHostBuilderExtension Description:
 * IWebHostBuilder扩展
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/


using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace NGP.Framework.Core
{
    /// <summary>
    /// IWebHostBuilder扩展
    /// </summary>
    public static class WebHostBuilderExtension
    {
        /// <summary>
        /// 配置ngp配置文件
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IWebHostBuilder ConfigureNGPConfiguration(this IWebHostBuilder builder,
            Action<WebHostBuilderContext, IConfigurationBuilder> action = null)
        {
            builder.ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;

                    // 开发环境寻找开发配置
                    if (env.IsDevelopment())
                    {
                        var basePath = File.Exists(env.WebRootPath) ? Path.GetDirectoryName(env.WebRootPath) : env.ContentRootPath;
                        // find the shared folder in the parent folder
                        var shareFile = Path.Combine(basePath, "..", "..", "..",
                            "SharedConfiguration",
                            $"NGPSettiings.{env.EnvironmentName}.json");
                        if (!File.Exists(shareFile))
                        {
                            shareFile = Path.Combine(basePath, "..", "..",
                                "SharedConfiguration",
                                 $"NGPSettiings.{env.EnvironmentName}.json");
                            config.AddJsonFile(shareFile, optional: true);
                        }
                        config.AddJsonFile(shareFile, optional: true);
                        config.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
                    }
                    else
                    {
                        // 发布环境寻找发布配置
                        config.AddJsonFile("NGPSettiings.json", optional: true);
                    }
                    config.AddJsonFile("appsettings.json", optional: true);                    
                    config.AddEnvironmentVariables();

                    action?.Invoke(hostingContext, config);
                });
            return builder;
        }
    }
}
