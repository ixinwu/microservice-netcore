/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * RequestResponseLoggingMiddleware Description:
 * 请求返回日志中间件
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-20   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 请求返回日志中间件
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        /// <summary>
        /// 需要排除的路由
        /// </summary>
        private static readonly List<string> EXPECTPATHS = new List<string>
        {
            "/hc".ToUpper(),
            "UploadFiles".ToUpper(),
            "ngpjob".ToUpper(),
            "UploadAuthFile".ToUpper(),
            "/.well-known".ToUpper(),
            "swagger".ToUpper()
        };
        private readonly RequestDelegate _next;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="next"></param>
        public RequestResponseLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// 执行器
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value;
            if (EXPECTPATHS.Any(s => path.ToUpper().Contains(s)))
            {
                await _next(context);
                return;
            }
            //接口调用开始时间
            var stratTime = DateTime.Now;
            //First, get the incoming request
            var request = await FormatRequest(context.Request);
            //接口调用结束时间
            var endTime = DateTime.Now;

            //Copy a pointer to the original response body stream
            var originalBodyStream = context.Response.Body;

            //Create a new memory stream...
            using (var responseBody = new MemoryStream())
            {
                //...and use that for the temporary response body
                context.Response.Body = responseBody;

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                //Format the response from the server
                var response = await FormatResponse(context.Response);

                var workContext = EngineContext.Current.Resolve<IWorkContext>();
                workContext.CurrentRequest.ResponseBody = response;

                //TODO: Save log to chosen datastore
                await WriteBusinessLog(stratTime, endTime, workContext);
                //Copy the contents of the new memory stream (which contains the response) to the original stream, which is then returned to the client.
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        /// <summary>
        /// 格式化参数
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableRewind();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            //request.Body = body;

            request.Body.Position = 0;

            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            // 设定上下文
            workContext.CurrentRequest = new WorkRequest
            {
                IpAddress = webHelper.GetCurrentIpAddress(),
                RequestBody = bodyAsText,
                Url = webHelper.GetThisRequestUrl(true)
            };

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        /// <summary>
        /// 格式化返回参数
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private async Task<string> FormatResponse(HttpResponse response)
        {
            //We need to read the response stream from the beginning...
            response.Body.Seek(0, SeekOrigin.Begin);

            //...and copy it into a string
            string text = await new StreamReader(response.Body).ReadToEndAsync();

            //We need to reset the reader for the response so that the client can read it.
            response.Body.Seek(0, SeekOrigin.Begin);

            //Return the string for the response, including the status code (e.g. 200, 404, 401, etc.)
            return $"{response.StatusCode}: {text}";
        }

        #region Add By Nick At 2019/10/9

        #region 添加业务日志
        /// <summary>
        /// 添加业务日志
        /// </summary>
        /// <param name="businessStratTime">业务开始时间</param>
        /// <param name="businessEndTime">业务结束时间</param>
        /// <param name="workContext">当前上下文</param>
        /// <returns></returns>
        private async Task WriteBusinessLog(DateTime businessStratTime, DateTime businessEndTime, IWorkContext workContext)
        {
            //数据仓储
            var unitRepository = Singleton<IEngine>.Instance.Resolve<IUnitRepository>();
            //记录当前时间
            var dtNow = DateTime.Now;
            //获取接口执行时间
            var excuteTime = businessEndTime - businessStratTime;
            var sqlStr = @"INSERT INTO [dbo].[Sys_Log_ApiInterface]
           ([Id],[ApiUrl],[EmpId],[IsDelete],[OrderIndex],[CreatedTime],[CreatedBy],[UpdatedTime],[UpdatedBy],[CreatedCompany],[UpdatedCompany],[CreatedArea],[UpdatedArea],[ApiPostParameter],[LoginName],[RoleId],[CompanyId],[LoginIP],[AreaId],[CallStratTime],[CallEndTime],[CallTimeS],[ApiResponseBody])
     VALUES
           (@Id,@ApiUrl,@EmpId,0,0,@CreatedTime,@CreatedBy,@UpdatedTime,@UpdatedBy,@CreatedCompany,@UpdatedCompany,@CreatedArea,@UpdatedArea,@ApiPostParameter,@LoginName,@RoleId,@CompanyId,@LoginIP,@AreaId,@CallStratTime,@CallEndTime,@CallTimeS,@ApiResponseBody)";
            //定义检索参数
            var sqlParameters = new Dictionary<string, object>();
            sqlParameters.Add("@Id", CommonHelper.NewGuid());
            sqlParameters.Add("@ApiUrl", workContext.CurrentRequest.Url);
            sqlParameters.Add("@EmpId", workContext.Current.EmplId);
            sqlParameters.Add("@CreatedTime", dtNow);
            sqlParameters.Add("@CreatedBy", workContext.Current.EmplId);
            sqlParameters.Add("@UpdatedTime", dtNow);
            sqlParameters.Add("@UpdatedBy", workContext.Current.EmplId);
            sqlParameters.Add("@CreatedCompany", workContext.Current.CompanyId);
            sqlParameters.Add("@UpdatedCompany", workContext.Current.CompanyId);
            sqlParameters.Add("@CreatedArea", workContext.Current.AreaId);
            sqlParameters.Add("@UpdatedArea", workContext.Current.AreaId);
            sqlParameters.Add("@ApiPostParameter", workContext.CurrentRequest.RequestBody);
            sqlParameters.Add("@LoginName", workContext.Current.LoginName);
            sqlParameters.Add("@RoleId", workContext.Current.RoleId);
            sqlParameters.Add("@CompanyId", workContext.Current.CompanyId);
            sqlParameters.Add("@LoginIP", workContext.CurrentRequest.IpAddress);
            sqlParameters.Add("@AreaId", workContext.Current.AreaId);
            sqlParameters.Add("@CallStratTime", businessStratTime);
            sqlParameters.Add("@CallEndTime", businessEndTime);
            sqlParameters.Add("@CallTimeS", Convert.ToInt32(excuteTime.TotalMilliseconds));
            sqlParameters.Add("@ApiResponseBody", workContext.CurrentRequest.ResponseBody);
            unitRepository.ExecuteNonQuery(sqlStr, sqlParameters);
            await Task.CompletedTask;
            //var businessLogInfo = new SysLog_ApiInterface
            //{
            //    Id = CommonHelper.NewGuid(),
            //    ApiPostParameter = workContext.CurrentRequest.RequestBody,
            //    ApiUrl = workContext.CurrentRequest.Url,
            //    AreaId = workContext.Current.AreaId,
            //    CallEndTime = businessEndTime,
            //    CallStratTime = businessStratTime,
            //    CallTimeS = Convert.ToInt32(excuteTime.TotalMilliseconds),
            //    CompanyId = workContext.Current.CompanyId,
            //    EmpId = workContext.Current.EmplId,
            //    IsDelete = false,
            //    LoginIP = workContext.CurrentRequest.IpAddress,
            //    LoginName = workContext.Current.LoginName,
            //    RoleId = workContext.Current.RoleId,
            //    ApiResponseBody = workContext.CurrentRequest.ResponseBody
            //};
            //businessLogInfo.InitAddDefaultFields();
            //unitRepository.Insert(businessLogInfo);
            //await unitRepository.SaveChangesAsync();
        }
        #endregion

        #endregion

    }
}