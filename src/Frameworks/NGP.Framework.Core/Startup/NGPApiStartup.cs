/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPApiStartup Description:
 * api配置启动器
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using NGP.Framework.Core.Api;
using NGP.Framework.Core.Filters;
using NGP.Framework.Core.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NGP.Framework.Core.Startup
{
    /// <summary>
    /// api配置启动器
    /// </summary>
    public class NGPApiStartup : INGPStartup
    {
        /// <summary>
        /// 添加api配置启动
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 添加基本的MVC功能
            var mvcBuilder = services.AddMvc(opts =>
            {
                if (configuration.GetValue<string>("ApiConfig:DocName") == "identity")
                {
                    return;
                }
                // 添加新的路由约定
                var convention = new NGPApiRouteConvention(
                    configuration.GetValue<string>("ApiConfig:DocName"),
                    (c) => c.ControllerType.BaseType == typeof(BaseApiController));
                opts.Conventions.Insert(0, convention);
            }).ConfigureApiBehaviorOptions(options=>
            {
                options.InvalidModelStateResponseFactory = c =>
                {
                    var error = c.ModelState.Values.Where(v => v.Errors.Count > 0)
                      .SelectMany(v => v.Errors)
                      .Select(v => v.ErrorMessage)
                      .FirstOrDefault();

                    return new BadRequestObjectResult(JsonConvert.DeserializeObject<NGPResponse>(error));
                };
            });

            // 设置MvcOptions上设置的默认值以匹配asp.net core mvc 2.2的行为
            mvcBuilder.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // 配置json序列化
            if (configuration.GetValue<string>("ApiConfig:DocName") == "ngpanalysis")
            {
                mvcBuilder.AddJsonOptions(options => options.SerializerSettings.ContractResolver =
            new DefaultContractResolver());
            }

            //add fluent validation
            mvcBuilder.AddFluentValidation(config =>
            {
                //register all available validators from Ngp assemblies
                var assemblies = mvcBuilder.PartManager.ApplicationParts
                    .OfType<AssemblyPart>()
                    .Where(part => part.Name.StartsWith("NGP", StringComparison.InvariantCultureIgnoreCase))
                    .Select(part => part.Assembly);
                config.RegisterValidatorsFromAssemblies(assemblies);

                //implicit/automatic validation of child properties
                config.ImplicitlyValidateChildProperties = true;
                config.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            });

            //register controllers as services, it'll allow to override them
            mvcBuilder.AddControllersAsServices();
        }

        /// <summary>
        /// 配置api中间件
        /// </summary>
        /// <param name="application"></param>
        public void Configure(IApplicationBuilder application)
        {
            // 配置请求返回日志
            application.UseMiddleware<RequestResponseLoggingMiddleware>();

            //路由
            application.UseMvc();

            // 全局配置
            application.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
        }

        /// <summary>
        /// 配置顺序
        /// </summary>
        public int Order
        {
            get { return 1000; }
        }
    }
}