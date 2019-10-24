using System;
using System.Collections.Generic;
using System.Text;

namespace NGP.Framework.Core
{
    /// <summary>
    /// 系统接口Api日志表Sys_Log_ApiInterface
    /// </summary>
    public class SysLog_ApiInterface: BaseDBEntity
    {
        /// <summary>
        /// api路径
        /// </summary>
        public string ApiUrl { get; set; }
        /// <summary>
        /// api提交参数
        /// </summary>
        public string ApiPostParameter { get; set; }
        /// <summary>
        /// 业务方法
        /// </summary>
        public string BusinessMethod { get; set; }
        /// <summary>
        /// 人员Id
        /// </summary>
        public string EmpId { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 平台Id
        /// </summary>
        public string CompanyId { get; set; }
        /// <summary>
        /// 登录Ip
        /// </summary>
        public string LoginIP { get; set; }
        /// <summary>
        /// 区域Id
        /// </summary>
        public string AreaId { get; set; }
        /// <summary>
        /// 调用开始时间
        /// </summary>
        public DateTime CallStratTime { get; set; }
        /// <summary>
        /// 调用结束时间
        /// </summary>
        public DateTime CallEndTime { get; set; }
        /// <summary>
        /// 调用时长
        /// </summary>
        public int CallTimeS { get; set; }
        /// <summary>
        /// 返回参数
        /// </summary>
        public string ApiResponseBody { get; set; }
    }
}
