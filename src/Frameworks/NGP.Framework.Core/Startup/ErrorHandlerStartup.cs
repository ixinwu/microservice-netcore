/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * ErrorHandlerStartup Description:
 * 错误处理启动器
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;

namespace NGP.Framework.Core.Startup
{
    /// <summary>
    /// 错误处理启动器
    /// </summary>
    public class ErrorHandlerStartup : INGPStartup
    {
        /// <summary>
        /// 添加错误启动
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {

        }

        /// <summary>
        /// 配置错误处理
        /// </summary>
        /// <param name="application"></param>
        public void Configure(IApplicationBuilder application)
        {
            application.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {

                        // TODO:WriteLog
                        //logger.LogError($"Something went wrong: {contextFeature.Error}");
                        var errorCode = StatusCode.SystemException;
                        var message = errorCode.Message();
                        var ngpException = contextFeature.Error as NGPException;
                        if (ngpException != null)
                        {
                            errorCode = ngpException.StatusCode;
                            message = ngpException.Message;
                        }
                        else //Add By Nick At 2019/10/09 添加日志
                        {
                            //数据仓储
                            var unitRepository = Singleton<IEngine>.Instance.Resolve<IUnitRepository>();
                            //获取当前登录上下文
                            var currentContext = Singleton<IEngine>.Instance.Resolve<IWorkContext>();
                            //记录当前时间
                            var dtNow = DateTime.Now;
                            var sqlStr = @"INSERT INTO [Sys_Log_Error]
           ([Id],[ApiUrl],[ApiPostParameter],[ExceptionContent],[IsDelete],[OrderIndex],[CreatedTime],[CreatedBy],[UpdatedTime],[UpdatedBy],[CreatedCompany],[UpdatedCompany],[CreatedArea],[UpdatedArea],[LoginIP],[AreaId],[LoginName],[RoleId],[CompanyId],[EmpId])
     VALUES
           (@Id,@ApiUrl,@ApiPostParameter,@ExceptionContent,0,0,@CreatedTime,@CreatedBy,@UpdatedTime,@UpdatedBy,@CreatedCompany,@UpdatedCompany,@CreatedArea,@UpdatedArea,@LoginIP,@AreaId,@LoginName,@RoleId,@CompanyId,@EmpId)";
                            //定义检索参数
                            var sqlParameters = new Dictionary<string, object>();
                            sqlParameters.Add("@Id", CommonHelper.NewGuid());
                            sqlParameters.Add("@ApiUrl", currentContext.CurrentRequest.Url);
                            sqlParameters.Add("@ApiPostParameter", currentContext.CurrentRequest.RequestBody);
                            sqlParameters.Add("@ExceptionContent", contextFeature.Error.Message);
                            sqlParameters.Add("@CreatedTime", dtNow);
                            sqlParameters.Add("@CreatedBy", currentContext.Current.EmplId);
                            sqlParameters.Add("@UpdatedTime", dtNow);
                            sqlParameters.Add("@UpdatedBy", currentContext.Current.EmplId);
                            sqlParameters.Add("@CreatedCompany", currentContext.Current.CompanyId);
                            sqlParameters.Add("@UpdatedCompany", currentContext.Current.CompanyId);
                            sqlParameters.Add("@CreatedArea", currentContext.Current.AreaId);
                            sqlParameters.Add("@UpdatedArea", currentContext.Current.AreaId);
                            sqlParameters.Add("@LoginIP", currentContext.CurrentRequest.IpAddress);
                            sqlParameters.Add("@AreaId", currentContext.Current.AreaId);
                            sqlParameters.Add("@LoginName", currentContext.Current.LoginName);
                            sqlParameters.Add("@RoleId", currentContext.Current.RoleId);
                            sqlParameters.Add("@CompanyId", currentContext.Current.CompanyId);
                            sqlParameters.Add("@EmpId", currentContext.Current.EmplId);
                            unitRepository.ExecuteNonQuery(sqlStr, sqlParameters);
                            //var logInfo = new SysLog_Error
                            //{
                            //    Id = CommonHelper.NewGuid(),
                            //    ApiPostParameter = currentContext.CurrentRequest.RequestBody,
                            //    ApiUrl = currentContext.CurrentRequest.Url,
                            //    ExceptionContent = contextFeature.Error.Message,
                            //    IsDelete = false,
                            //    AreaId = currentContext.Current.AreaId,
                            //    CompanyId = currentContext.Current.CompanyId,
                            //    EmpId = currentContext.Current.EmplId,
                            //    LoginIP = currentContext.CurrentRequest.IpAddress,
                            //    LoginName = currentContext.Current.LoginName,
                            //    RoleId = currentContext.Current.RoleId

                            //};
                            //logInfo.InitAddDefaultFields();
                            //unitRepository.Insert(logInfo);
                            //await unitRepository.SaveChangesAsync();
                        }
                        await context.Response.WriteAsync(new NGPResponse
                        {
                            Message = message,
                            Status = errorCode
                        }.ToString());
                    }
                });
            });
        }

        /// <summary>
        /// 启动顺序
        /// </summary>
        public int Order
        {
            // 应首先加载错误处理程序
            get { return 0; }
        }
    }
}
