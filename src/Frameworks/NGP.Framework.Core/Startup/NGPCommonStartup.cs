/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPCommonStartup.cs Description:
 * 启动时通用配置
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGP.Framework.Core.Data;
using NGP.Framework.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace NGP.Framework.Core.Startup
{
    /// <summary>
    /// 启动时通用配置
    /// </summary>
    public class NGPCommonStartup : INGPStartup
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //compression
            services.AddResponseCompression();

            //add options feature
            services.AddOptions();

            //add distributed memory cache
            //services.AddDistributedMemoryCache();

            //add localization
            services.AddLocalization();
        }

        /// <summary>
        /// 配置请求管道
        /// </summary>
        /// <param name="application"></param>
        public void Configure(IApplicationBuilder application)
        {
            application.UseResponseCompression();

            //use static files feature
            application.UseStaticFiles();

            application.UseRequestLocalization(options =>
            {
                //prepare supported cultures
                var defaultCultrue = new CultureInfo("zh-cn");
                options.SupportedCultures = new List<CultureInfo> { defaultCultrue };
                options.DefaultRequestCulture = new RequestCulture(defaultCultrue);
            });
        }

        /// <summary>
        /// 加载顺序
        /// </summary>
        public int Order => 100;
    }
}